using UnityEngine;
using System;

namespace TowerDefence
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyMovement : MonoBehaviour
    {

        private Transform target;

        private Enemy enemy;

        public float checkRadius;
        public LayerMask checkLayers;

        public float AttackRadius;

        public float Damage = 1f;

        [Header("Use Laser")]
        public bool useLaser = true;

        public int damageOverTime = 30;
        public float slowAmount = .5f;

        public LineRenderer lineRenderer;
        public ParticleSystem impactEffect;
        public Light impactLight;

        public Transform firePoint;

        public Animator walking;

        //private bool turretInRange;

        public Transform partToRotate;
        public float turnSpeed = 10f;

        void Start()
        {
            enemy = GetComponent<Enemy>();
            Damage *= Time.deltaTime;
            target = GameObject.Find("END").transform;
            walking = GetComponent<Animator>();
        }



        void Update()
        {

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
                
                walking.enabled = true;
                target = GameObject.Find("END").transform;
                LockOnTarget();
            }
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, target.position) <= 0.4f)
            {
                EndPath();
            }
            LockOnTarget();
            enemy.speed = enemy.startSpeed;

            Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, checkLayers);
            Collider[] attackColliders = Physics.OverlapSphere(transform.position, AttackRadius, checkLayers);
            Array.Sort(attackColliders, new Distance_Enemies(transform));
            Array.Sort(colliders, new Distance_Enemies(transform));


            if (colliders.Length > 0)
            {
                walking.enabled = false;
                if (attackColliders.Length == 0)
                {
                    target = colliders[0].transform;
                    LockOnTarget();

                }
                else
                {
                    //if (Vector3.Distance(transform.position, target.position) == AttackRadius)
                    //{

                    walking.enabled = false;
                        LockOnTarget();
                        damage(colliders[0].transform);
                    //}
                    //else
                    //{
                        //walking.enabled = true;
                        //Vector3 dirNow = transform.position;
                        transform.Translate(-dir.normalized * enemy.speed * Time.deltaTime, Space.World);
                    //}

                }
            }
            else
            {
                walking.enabled = true;
                target = GameObject.Find("END").transform;
                LockOnTarget();
            }
        }

        void damage(Transform TargetOfAttack)
        {
           
            Turret T = TargetOfAttack.GetComponent<Turret>();


            if (T != null)
            {
                T.TakeDamage(damageOverTime * Time.deltaTime);
                if (!lineRenderer.enabled)
                {
                    lineRenderer.enabled = true;
                    impactEffect.Play();
                    impactLight.enabled = true;
                }
                lineRenderer.SetPosition(0, firePoint.position);
                lineRenderer.SetPosition(1, T.transform.position);

                Vector3 fireDistance = firePoint.position - target.position;

                impactEffect.transform.position = target.position + fireDistance.normalized;

                impactEffect.transform.rotation = Quaternion.LookRotation(fireDistance);
            }

            else if(target.tag=="Player")
            {

                //  T.TakeDamage(damageOverTime * Time.deltaTime);
                PlayerUI.instance.PlayerDamage(damageOverTime * Time.deltaTime);
                if (!lineRenderer.enabled)
                {
                    lineRenderer.enabled = true;
                    impactEffect.Play();
                    impactLight.enabled = true;
                }
                lineRenderer.SetPosition(0, firePoint.position);
                lineRenderer.SetPosition(1, target.transform.position);

                Vector3 fireDistance = firePoint.position - target.position;

                impactEffect.transform.position = target.position + fireDistance.normalized;

                impactEffect.transform.rotation = Quaternion.LookRotation(fireDistance);
                return;
            }
        }

        //void Laser()
        //{
        //    targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        //    targetEnemy.Slow(slowAmount);

        //    if (!lineRenderer.enabled)
        //    {
        //        lineRenderer.enabled = true;
        //        impactEffect.Play();
        //        impactLight.enabled = true;
        //    }

        //    lineRenderer.SetPosition(0, firePoint.position);
        //    lineRenderer.SetPosition(1, target.position);

        //    Vector3 dir = firePoint.position - target.position;

        //    impactEffect.transform.position = target.position + dir.normalized;

        //    impactEffect.transform.rotation = Quaternion.LookRotation(dir);
        //}

        void LockOnTarget()
        {
            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }


        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, checkRadius);
            Gizmos.DrawWireSphere(transform.position, AttackRadius);
        }



        void EndPath()
        {
            PlayerStats.Lives--;
            WaveSpawner.EnemiesAlive--;
            Destroy(gameObject);
        }

    }
}