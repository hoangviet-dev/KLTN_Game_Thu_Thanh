using Assets.Scripts.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class UIGameStatus : MonoBehaviour
{
    private static UIGameStatus instance;

    [SerializeField] private GameObject panelObject;
    [SerializeField] private GameObject settingObject;
    [SerializeField] private GameObject gameFinishObject;
    [SerializeField] private GameObject gameFailureObject;
    [SerializeField] private SceneFader sceneFader;
    [SerializeField] private Toggle toggleOnOffBackgroundSound;
    [SerializeField] private Toggle toggleOnOffEffectSound;
    [SerializeField] private AudioSource backgroundSound;

    private void Awake()
    {
        instance = this;
        panelObject.SetActive(false);
        toggleOnOffBackgroundSound.isOn = BaseGameCTLs.Instance.IsBackgroundSound;
        toggleOnOffEffectSound.isOn = BaseGameCTLs.Instance.IsEffectSound;
    }

    public static void ShowSetting()
    {
        instance.HideAll();
        instance.panelObject.SetActive(true);
        instance.settingObject.SetActive(true);
    }

    public static void ShowGameFailure()
    {
        instance.HideAll();
        instance.panelObject.SetActive(true);
        instance.gameFailureObject.SetActive(true);
    }

    public static void ShowGameFinish()
    {
        instance.HideAll();
        instance.panelObject.SetActive(true);
        instance.gameFinishObject.SetActive(true);
    }

    public static void StopBackgroundSound()
    {
        instance.backgroundSound.Stop();
    }

    private void HideAll()
    {
        panelObject.SetActive(false);
        settingObject.SetActive(false);
        gameFailureObject.SetActive(false);
        gameFinishObject.SetActive(false);
    }

    public void Retry()
    {
        Time.timeScale = 1;
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Time.timeScale = 1;
        BaseGameCTLs.Instance.ResumeGame();
        sceneFader.FadeTo(BaseGameCTLs.HOME_SCENE);
    }

    public void Resume()
    {
        BaseGameCTLs.Instance.ResumeGame();
        HideAll();
    }

    public void OnOffBackgroundSound()
    {
        BaseGameCTLs.Instance.IsBackgroundSound = toggleOnOffBackgroundSound.isOn;
        if (toggleOnOffBackgroundSound.isOn)
        {
            backgroundSound.Play();
        } else
        {
            backgroundSound.Stop();
        }
    }

    public void OnOffEffectSound()
    {
        BaseGameCTLs.Instance.IsEffectSound = toggleOnOffEffectSound.isOn;
    }
}
