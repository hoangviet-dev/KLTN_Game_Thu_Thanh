using Assets.Scripts.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Models.Enemy
{
    public class Enemy : MonoBehaviour
    {
        private List<Vector3> wayPoints;

        public Slider SliderHealth;
        protected float speed;
        [SerializeField] protected GameObject enemyModel;
        protected float health;
        public int value = 0;

        private float currenthealth;

        private bool isRun = false;

        private float currentSpeed;

        private int currentWaypoint = 0;

        void Start()
        {
            currentSpeed = speed;
            currenthealth = health;
        }

        void Update()
        {
            if (BaseGameCTLs.Instance.State == EGameState.PLAYING && isRun)
            {
                Move();
            }
        }

        public void SetInfo(float speed, float health, List<Vector3> wayPoints)
        {
            this.speed = speed;
            this.health = health;
            this.wayPoints = wayPoints;
            currentSpeed = speed;
            currenthealth = health;
        }

        private void Move()
        {
            Vector3 dir = wayPoints[currentWaypoint] - transform.position;
            transform.Translate(dir.normalized * currentSpeed * Time.deltaTime, Space.World);
            if (Vector3.Distance(transform.position, wayPoints[currentWaypoint]) <= .4f)
            {
                GetNextWaypoint();
            }
            currentSpeed = speed;
        }

        void GetNextWaypoint()
        {
            if (currentWaypoint >= wayPoints.Count - 1)
            {
                EndPath();
                return;
            }
            currentWaypoint++;
            if (enemyModel)
            {
                enemyModel.transform.LookAt(new Vector3(wayPoints[currentWaypoint].x, transform.position.y, wayPoints[currentWaypoint].z));
            }
        }

        void EndPath()
        {
            BaseGameCTLs.Instance.Health--;
            Destroy(gameObject);
        }

        /// <summary>
        /// Cho phep Enemy chay den vi tri muc tieu
        /// </summary>
        public void Run()
        {
            if (wayPoints != null)
            {
                isRun = true;
                transform.position = wayPoints[0];
                currentWaypoint = 0;
            }
        }

        /// <summary>
        /// Ham lam cham
        /// </summary>
        /// <param name="slowPercent"></param>
        public void Slow(float slowPercent)
        {
            currentSpeed = Math.Min(speed, speed * (1f - slowPercent));
        }

        /// <summary>
        /// Ham nhan xac thuong
        /// </summary>
        /// <param name="damage"></param>
        public void TakeDamage(float damage)
        {
            currenthealth -= damage;
            SliderHealth.value = currenthealth / health;
            if (currenthealth < 0)
            {
                Die();
            }
        }

        /// <summary>
        /// Ham khi tu huy khi luong mau ve 0
        /// </summary>
        void Die()
        {
            BaseGameCTLs.Instance.Money += value;
            Destroy(gameObject);
        }
    }
}
