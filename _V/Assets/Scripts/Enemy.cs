using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class Enemy : MonoBehaviour
    {

        public float startSpeed = 10f;

        [HideInInspector]
        public float speed;
        public float range = 15f;

        public float startHealth = 100;
        private float health;
        private float Sheild;
        public float MaxSheild = 50;

        public int worth = 50;

        public GameObject deathEffect;

        [Header("Unity Stuff")]
        public Image healthBar;

        [Header("Use Laser")]
        public bool useLaser = false;

        public int damageOverTime = 30;
        public float slowAmount = .5f;

        public LineRenderer lineRenderer;
        public ParticleSystem impactEffect;
        public Light impactLight;

        private Transform target;
        private Enemy targetEnemy;

        [Header("Use Bullets (default)")]
        public GameObject bulletPrefab;
        public float fireRate = 1f;
        private float fireCountdown = 0f;

        [Header("Unity Setup Fields")]

        public string enemyTag = "Enemy";

        public Transform partToRotate;
        public float turnSpeed = 10f;

        public Transform firePoint;

        private bool isDead = false;

        void Start()
        {
            speed = startSpeed;
            health = startHealth;
        }

        public void TakeDamage(float amount)
        {
            health -= amount;

            healthBar.fillAmount = health / startHealth;

            if (health <= 0 && !isDead)
            {
                Die();
            }
        }

        public void Slow(float pct)
        {
            speed = startSpeed * (1f - pct);
        }

        void Die()
        {
            isDead = true;

            PlayerStats.Money += worth;

            GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 5f);

            WaveSpawner.EnemiesAlive--;

            Destroy(gameObject);
        }

        void Update()
        {
            if (MaxSheild > Sheild)
            {
                Sheild = MaxSheild;
            }
            if (target == null)
            {
                if (useLaser)
                {
                    if (lineRenderer.enabled)
                    {
                        lineRenderer.enabled = false;
                        impactEffect.Stop();
                        impactLight.enabled = false;
                    }
                }

                return;
            }

            LockOnTarget();

            if (useLaser)
            {
                Laser();
            }
            else
            {
                if (fireCountdown <= 0f)
                {
                    Shoot();
                    fireCountdown = 1f / fireRate;
                }

                fireCountdown -= Time.deltaTime;
            }
            if (health <= 0)
            {
                Destroy(gameObject);
            }

        }

        void LockOnTarget()
        {
            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }

        void Laser()
        {
            targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
            targetEnemy.Slow(slowAmount);

            if (!lineRenderer.enabled)
            {
                lineRenderer.enabled = true;
                impactEffect.Play();
                impactLight.enabled = true;
            }

            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, target.position);

            Vector3 dir = firePoint.position - target.position;

            impactEffect.transform.position = target.position + dir.normalized;

            impactEffect.transform.rotation = Quaternion.LookRotation(dir);
        }

        void Shoot()
        {
            GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bullet = bulletGO.GetComponent<Bullet>();

            if (bullet != null)
                bullet.Seek(target);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }

    }
}
