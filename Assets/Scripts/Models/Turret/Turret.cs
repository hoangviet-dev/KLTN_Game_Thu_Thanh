using Assets.Scripts.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Models.Turret
{
    public class Turret : MonoBehaviour
    {
        [Header("Attributes")]
        public string enemyTag = "Enemy";
        [SerializeField] private string turretName;
        [SerializeField] private string description;
        [SerializeField] protected int cost;
        [SerializeField] private bool isSale = true;
        [SerializeField] protected float range;
        [SerializeField] protected float slowPercent = 0;
        [SerializeField] protected float damageValue = 30;
        [SerializeField] protected float timeBuild = 2;
        [SerializeField] protected bool isLook = true;
        [SerializeField] private Transform customPivot;
        [SerializeField] private List<TurretComponent> ListTurretComponent;
        [SerializeField] protected int attackQuantity = 1;
        [SerializeField] protected AudioSource attackSound;

        [Header("Object Component")]
        [SerializeField] protected GameObject baseTurret;
        [SerializeField] protected GameObject headTurret;
        [SerializeField] protected GameObject footTurret;
        [SerializeField] protected Transform barrelTransform;
        [SerializeField] protected GameObject startEffect;
        [SerializeField] private Slider sliderBuilding;


        protected Transform target = null;
        protected List<Enemy.Enemy> targetEnemies = null;
        protected bool ready = false;
        protected TurretInfo info = new TurretInfo();
        protected List<Shop.ShopItem> listShop = new List<Shop.ShopItem>();
        public string TurretName
        {
            get { return turretName; }
        }

        public string TurretDescription
        {
            get { return description; }
        }

        public int TurretCost
        {
            get { return cost; }
        }

        public float TurretRange
        {
            get { return range; }
        }

        public int Sale
        {
            get { return (int)Math.Round(cost * .75f); }
        }


        private void Start()
        {
            sliderBuilding.gameObject.SetActive(true);
            headTurret.SetActive(false);
            InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
            SetPositionWithPivot();
            Init();
        }

        private void Update()
        {
            if (BaseGameCTLs.Instance.State == EGameState.PLAYING)
            {
                if (!ready)
                {
                    return;
                }
                FollowTarget();
                Action();
            } else
            {
                Finish();
                CancelInvoke(nameof(UpdateTarget));
            }
        }

        protected virtual void Init()
        {

        }

        protected virtual void Action()
        {

        }

        protected virtual void Finish()
        {
            targetEnemies = null;
            target = null;
        }

        public EBuildTurretState Upgrade(Shop.ShopItem item)
        {
            if (item.Value == "Sale")
            {
                //BaseGameCTLs.Instance.Money += Sale;
                Destroy(gameObject);
                return EBuildTurretState.Sale;
            }
            else
            {
                TurretComponent currentTurretComponent = Instantiate(ListTurretComponent[item.Index], transform);
                if (currentTurretComponent != null)
                {
                    if (currentTurretComponent.headTurret != null)
                    {
                        Destroy(headTurret.gameObject);
                        headTurret = currentTurretComponent.headTurret;
                        headTurret.transform.parent = baseTurret.transform;
                        //headTurret.transform.localPosition = currentTurretComponent.headTurret.transform.position;
                        barrelTransform = currentTurretComponent.barrelTransform ?? barrelTransform;
                    }
                    if (currentTurretComponent.footTurret != null)
                    {
                        Destroy(footTurret.gameObject);
                        footTurret = currentTurretComponent.footTurret;
                        footTurret.transform.parent = baseTurret.transform;
                    }
                    customPivot = currentTurretComponent.customPivot ?? customPivot;
                    SetUpgrade(currentTurretComponent);
                    Destroy(currentTurretComponent.gameObject);
                    ListTurretComponent = currentTurretComponent.Childent;
                }
            }
            return EBuildTurretState.Success;
        }

        void FollowTarget()
        {
            if (target != null && isLook && headTurret != null)
            {
                Vector3 dir = target.position - transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(dir);
                Vector3 rotation = lookRotation.eulerAngles;
                headTurret.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            }
        }

        void UpdateTarget()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

            List<Enemy.Enemy> nearestEnemies = new List<Enemy.Enemy>();
            int count = 0;
            foreach (GameObject enemy in enemies)
            {

                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy <= range)
                {
                    count++;
                    if (count > attackQuantity)
                    {
                        break;
                    }
                    nearestEnemies.Add(enemy.GetComponent<Enemy.Enemy>());
                }
            }

            if (nearestEnemies.Count>0)
            {
                target = nearestEnemies[0].transform;
                targetEnemies = nearestEnemies;
            }
            else
            {
                target = null;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }

        /// <summary>
        /// Ham cai gan gia tri cho cac cap nhap
        /// </summary>
        /// <param name="turretComponent"></param>
        protected virtual void SetUpgrade(TurretComponent turretComponent)
        {
            if (turretComponent.description != null)
            {
                description = turretComponent.description;
            }
            damageValue = AttributeTurretFloat.ConvertValue(damageValue, turretComponent.damage);
            range = AttributeTurretFloat.ConvertValue(range, turretComponent.range);
            slowPercent = AttributeTurretFloat.ConvertValue(slowPercent, turretComponent.slowPercent);
            attackQuantity = AttributeTurretInt.ConvertValue(attackQuantity, turretComponent.attackQuantity);
            cost += turretComponent.cost;
        }

        public virtual List<Shop.ShopItem> GetListShop()
        {
            listShop.Clear();
            for (int i = 0; i < ListTurretComponent.Count; i++)
            {
                TurretComponent turretComponent = ListTurretComponent[i];
                listShop.Add(new Shop.ShopItem(turretComponent.nameTurret, turretComponent.nameTurret, turretComponent.imageView, turretComponent.cost, i, turretComponent.description, turretComponent.range));
            }
            if (isSale)
            {
                listShop.Add(new Shop.ShopItem("Bán", "Sale", ResourcesCTL.Instance.IconSale, -Sale, -1, String.Format("Nhận lại <b><#f39c12>{0}$</color></b>", Sale)));
            }
            return listShop;
        }

        private delegate void CallBack();

        public void Build()
        {
            StartCoroutine(Building(timeBuild, BuiltStart));
        }

        private void BuiltStart()
        {
            headTurret.SetActive(true);
            sliderBuilding.gameObject.SetActive(false);
            ready = true;
        }

        private IEnumerator Building(float timeBuild, CallBack callBack)
        {
            float _time = 0;
            sliderBuilding.value = 0;
            while (_time < timeBuild)
            {
                _time += Time.deltaTime;
                sliderBuilding.value = _time / timeBuild;
                yield return null;
            }

            callBack();
        }

        public void SetPositionWithPivot()
        {
            Vector3 dir = customPivot.position - transform.position;
            transform.position -= dir;
        }

        public GameObject GetBaseObject()
        {
            return baseTurret.gameObject;
        }

        public virtual TurretInfo GetInfo()
        {
            info.damage = damageValue;
            info.slowPercent = slowPercent;
            info.range = range;
            info.attackQuantity = attackQuantity;
            return info;
        }
    }
}
