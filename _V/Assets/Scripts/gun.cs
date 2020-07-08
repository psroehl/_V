using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class gun : MonoBehaviour {
        public float damage = 10f;
        public float fireRate = 15f;
        public float range = 100f;
        public float impactForce = 30f;
        
        public int maxAmmo = 10;
        public static float currentAmmo;
        public float reloadTime = 1f;
        private bool isReloading = false;

        public Transform shotText;
        
        public Camera fpsCam;
        public ParticleSystem muzzleFlash;
        public GameObject impactEffect;

        private float nextTimeToFire = .05f;

        public Animator animator;

        void Start()
        {
                currentAmmo = maxAmmo;
        }

        void OnEnable ()
        {
            isReloading = false;
            animator.SetBool("Reloading", false);
        }

        void Update() { 
            shotText.GetComponent<Text> ().text = "Ammo: " + currentAmmo.ToString() + "/" + maxAmmo;
            if (isReloading)
                return;

            if (currentAmmo <= 0)
            {
                StartCoroutine(Reload());
                return;
            }
            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f/fireRate;
                Shoot();
                shotText.GetComponent<Text> ().text = currentAmmo.ToString();
            }
            if(Input.GetKeyDown(KeyCode.R)){
                StartCoroutine(Reload());
                return;
            }
        }

        IEnumerator Reload()
        {
            isReloading = true;
            Debug.Log("Reloading...");

            animator.SetBool("Reloading", true);

            yield return new WaitForSeconds(reloadTime - .25f);
            animator.SetBool("Reloading", false);
            yield return new WaitForSeconds(.25f);

            currentAmmo = maxAmmo;
            isReloading = false;
        }



        void Shoot ()
        {
            muzzleFlash.Play();

            currentAmmo--;

            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);

                Enemy target = hit.transform.GetComponent<Enemy>();
                if (target!= null)
                {
                    target.TakeDamage(damage);
                }
                
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }

                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy( impactGO, 2f);
            }
        }

    }
}