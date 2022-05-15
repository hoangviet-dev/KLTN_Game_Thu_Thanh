using Assets.Scripts.Controllers;
using Assets.Scripts.Models.Enemy;
using Assets.Scripts.Models.Map;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    private static GameControl instance;

    [SerializeField] private Transform mapTranform;
    [SerializeField] private GameObject mapMiniMap;
    [SerializeField] private GameObject questPointObject;
    [SerializeField] private Transform questPointPanel;
    [SerializeField] private Camera miniMapCamera;
    [SerializeField] private Transform enemyInfoPanelTransform;
    [SerializeField] private UIEnemyInfoItem enemyInfoItemPrefab;
    [SerializeField] private AudioSource countDownSound;
    [SerializeField] private AudioSource startSound;
    [SerializeField] private AudioSource finishSound;
    [SerializeField] private AudioSource failureSound;

    private int timeDown;
    private int waveNumber;
    private List<GameObject> listQuestPointObject;
    private LevelCTLs levelCTLs;
    private MapDataCTLs mapDataCTLs;
    private bool isEnemyRun;

    private Vector3 vectorDirectionCameraMain;

    private void Awake()
    {
        BaseGameCTLs.Instance.Health = 10;
        BaseGameCTLs.Instance.State = EGameState.PLAYING;
        BaseGameCTLs.Instance.Money = 100;
        //BaseGameCTLs.Instance.SpeedGame = 2;

        instance = this;
        waveNumber = 0;

        listQuestPointObject = new List<GameObject>();

        mapDataCTLs = new MapDataCTLs(BaseGameCTLs.Instance.MapId);
        LoadMap();

        //Camera minimap hien thi bao tron map
        Quaternion rotationCamera = miniMapCamera.transform.rotation;
        HelperCTLs.Instance.TakeObject(mapTranform.gameObject, miniMapCamera, -miniMapCamera.transform.forward);
        miniMapCamera.transform.rotation = rotationCamera;

        //Camera main hien thi bao tron map
        vectorDirectionCameraMain = -Camera.main.transform.forward;
        HelperCTLs.Instance.TakeObject(mapTranform.gameObject, Camera.main, vectorDirectionCameraMain);

        levelCTLs = new LevelCTLs(BaseGameCTLs.Instance.LevelId);

        isEnemyRun = true;
    }

    private void Start()
    {
        BeginWave();
    }

    private void Update()
    {
        CheckInput();
    }

    private void LateUpdate()
    {
        CheckWaveFinish();
    }

    void CheckInput()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            HelperCTLs.Instance.TakeObject(mapTranform.gameObject, Camera.main, vectorDirectionCameraMain);
        }
    }

    private void CheckWaveFinish()
    {
        if (BaseGameCTLs.Instance.State == EGameState.PLAYING)
        {
            if (BaseGameCTLs.Instance.Health <= 0)
            {
                GameOver();
            }

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length == 0 && isEnemyRun)
            {
                waveNumber++;

                if (waveNumber >= levelCTLs.WaveCount)
                {
                    GameSuccess();
                }
                else
                {
                    BeginWave();
                }
            }

        }
    }

    private void GameOver()
    {
        UIGameStatus.StopBackgroundSound();
        if (BaseGameCTLs.Instance.IsEffectSound && failureSound != null)
        {
            failureSound.Play();
        }
        BaseGameCTLs.Instance.SpeedGame = 1;
        BaseGameCTLs.Instance.State = EGameState.GAME_OVER;
        ClearQuestPoint();
        UIGameStatus.ShowGameFailure();
    }

    private void GameSuccess()
    {
        UIGameStatus.StopBackgroundSound();
        if (BaseGameCTLs.Instance.IsEffectSound && finishSound != null)
        {
            finishSound.Play();
        }
        if (DataCTLs.Instance.Level == BaseGameCTLs.Instance.LevelId)
        {
            DataCTLs.Instance.Level++;
        }
        BaseGameCTLs.Instance.SpeedGame = 1;
        BaseGameCTLs.Instance.State = EGameState.GAME_OVER;
        ClearQuestPoint();
        UIGameStatus.ShowGameFinish();
    }

    private void BeginWave()
    {
        isEnemyRun = false;
        UINotification.Title = string.Format("Vòng {0}", waveNumber + 1);
        ClearQuestPoint();
        ShowQuestPoint();

        List<WaveDetail> waves = levelCTLs.GetWave(waveNumber);
        bool isBoss = false;
        foreach (WaveDetail wave in waves)
        {
            isBoss |= wave.waveInfo.IsBoss;
        }
        ShowEnemyInfo(waves);
        UINotification.IsBoss = isBoss;
        if (waveNumber == 0)
        {
            StartCountDown(10);
        }
        else
        {
            StartCountDown(20);
        }
    }

    private void ShowEnemyInfo(List<WaveDetail> waves)
    {
        foreach (Transform child in enemyInfoPanelTransform)
        {
            Destroy(child.gameObject);
        }
        int indexEnemy = 0;
        foreach (WaveDetail wave in waves)
        {
            indexEnemy++;
            UIEnemyInfoItem itemEnemyInfo = Instantiate(enemyInfoItemPrefab, enemyInfoPanelTransform);
            if (wave.waveInfo.IsBoss)
            {
                itemEnemyInfo.SetTitle(string.Format("Quái {0}-{1} - <#f1c40f>BOSS</color>", waveNumber + 1, indexEnemy));
            }
            else
            {
                itemEnemyInfo.SetTitle(string.Format("Quái {0}-{1}", waveNumber + 1, indexEnemy));
            }
            itemEnemyInfo.SetSpeed(wave.waveInfo.Speed.ToString());
            itemEnemyInfo.SetHealth(wave.waveInfo.Health.ToString());
            itemEnemyInfo.SetQuantity(wave.waveInfo.Quantity.ToString());
        }
    }

    private void LoadMap()
    {
        mapDataCTLs.DrawMap(Instantiate, mapMiniMap, mapTranform);
    }

    /// <summary>
    /// Ham bat dau dem nguoc
    /// </summary>
    /// <param name="second"></param>
    private void StartCountDown(int second)
    {
        timeDown = second;
        StartCoroutine(CountDown());
    }

    /// <summary>
    /// Ham dem nguoc
    /// </summary>
    /// <returns></returns>
    IEnumerator CountDown()
    {
        UINotification.TimeDown = timeDown;
        while (timeDown > 0 && BaseGameCTLs.Instance.State == EGameState.PLAYING)
        {
            yield return new WaitForSeconds(1.0f);
            timeDown--;
            if (BaseGameCTLs.Instance.IsEffectSound && timeDown <= 5)
            {
                if (timeDown > 0 && countDownSound != null)
                {
                    countDownSound.PlayOneShot(countDownSound.clip);
                }
                else if (startSound != null)
                {
                    startSound.PlayOneShot(startSound.clip);
                }
            }
            UINotification.TimeDown = timeDown;
        }
        WaveStart();
    }

    /// <summary>
    /// Hien thi duong di cua quan dich
    /// </summary>
    private void ShowQuestPoint()
    {
        List<WaveDetail> waves = levelCTLs.GetWave(waveNumber);
        foreach (WaveDetail wave in waves)
        {
            List<Vector3> listWaypoints = mapDataCTLs.GetWayPoint(wave.waveInfo.WayIndex);
            int indexWayPoint = 0;
            while (indexWayPoint < listWaypoints.Count - 1)
            {
                Vector3 dir = listWaypoints[indexWayPoint + 1] - listWaypoints[indexWayPoint];
                float distance = dir.magnitude;
                for (float i = 0; i < distance; i += 1)
                {
                    Vector3 position = listWaypoints[indexWayPoint] + dir.normalized * i;
                    position.y += .01f;
                    GameObject _questPointObject = Instantiate(questPointObject, position, Quaternion.identity, questPointPanel);
                    _questPointObject.transform.LookAt(new Vector3(listWaypoints[indexWayPoint + 1].x, position.y, listWaypoints[indexWayPoint + 1].z));
                    listQuestPointObject.Add(_questPointObject);
                }
                indexWayPoint++;
            }
        }
    }

    /// <summary>
    /// An duong di cua quan dich
    /// </summary>
    private void ClearQuestPoint()
    {
        foreach (GameObject cquestPointObject in listQuestPointObject)
        {
            Destroy(cquestPointObject);
        }
        listQuestPointObject.Clear();
    }

    private void WaveStart()
    {
        List<WaveDetail> waves = levelCTLs.GetWave(waveNumber);
        foreach (WaveDetail wave in waves)
        {
            StartCoroutine(EnemyStart(wave, mapDataCTLs.GetWayPoint(wave.waveInfo.WayIndex)));
        }
        isEnemyRun = true;
    }

    IEnumerator EnemyStart(WaveDetail waveDetail, List<Vector3> wayPoints)
    {
        if (wayPoints != null || wayPoints.Count != 0)
        {
            int count = 0;
            while (count < waveDetail.waveInfo.Quantity && BaseGameCTLs.Instance.State == EGameState.PLAYING)
            {
                GameObject enemyObject = Instantiate(waveDetail.prefab, wayPoints[0], Quaternion.identity);
                Enemy enemy = enemyObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.SetInfo(waveDetail.waveInfo.Speed, waveDetail.waveInfo.Health, wayPoints);
                    enemy.Run();
                }
                count++;
                yield return new WaitForSeconds(waveDetail.waveInfo.Duration);
            }
        }
    }
}
