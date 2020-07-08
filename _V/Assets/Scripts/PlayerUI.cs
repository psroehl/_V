using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

namespace TowerDefence
{
    public class PlayerUI : MonoBehaviour
    {
        public float maxHealth = 100f;
        public float currentHealth;
        public bool isDead = false;
        public Slider healthbar;
        public Turret Turret;
        public Image tagHealth;
        public float health;
        // public static float remainingShot = 10f;
        // public Transform shotText;

        public static PlayerUI instance;

        void Update()
        {
            CompHealthBar();
            Debug.Log("o shit");
        }

        void Awake()
        {

            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
        
        }
        public void CompHealthBar()
            {
                // health = computerInfo.TakeDamage(0);
                tagHealth.fillAmount = Turret.health / Turret.startHealth;           
            }

        void Start()
        {
            currentHealth = maxHealth;
        }

        public void PlayerDamage(float damage)
        {
            if (isDead)
                return;
            if (damage >= currentHealth)
            {
                Dead();
            }
            else
            {
                currentHealth -= damage;
            }
            healthbar.value = currentHealth/100;
        }
        void Dead()
        {
            currentHealth = 0f;
            isDead = true;
            Debug.Log("Player Is Dead");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("LoseScreen");
        }
    }
}
