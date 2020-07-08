using UnityEngine;
using System.Collections;
using System;

namespace TowerDefence
{
    [RequireComponent(typeof(Turret))]
    public class ShieldTurret : MonoBehaviour
    {

        private Transform target;

        private Turret turret;

        public float checkRadius;
        public LayerMask checkLayers;

        //public float ShieldRadius;

        public float Shield = 30f;

        private Collider[] colliders = new Collider[]{};
        private Turret[] turrets = new Turret[]{};

    void Start()
        {
            turret = GetComponent<Turret>();
            //Damage *= Time.deltaTime;
            target = GameObject.Find("END").transform;
        }



        void Update()
        {
            //if (target == null)
            //{
            //target = GameObject.Find("END").transform;
            //}
            //Vector3 dir = target.position - transform.position;
            //transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

            //if (Vector3.Distance(transform.position, target.position) <= 0.4f)
            //{
            //EndPath();
            //}

            //enemy.speed = enemy.startSpeed;

            Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, checkLayers);
            //Collider[] shieldColliders = Physics.OverlapSphere(transform.position, ShieldRadius, checkLayers);
            //Array.Sort(shieldColliders, new Distance_Enemies(transform));
            //Array.Sort(colliders, new Distance_Enemies(transform));

        }


        //void damage(Transform TargetOfShield)
        //{
          //  foreach (Collider i in colliders)
           // {
             //   Turret T = TargetOfShield.GetComponent<Turret>();
             //   if (T != null)
             //   {
           //         T.TakeDamage(Shield);
          //      }
          //  }



      // }



        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, checkRadius);
        }
    }
}
