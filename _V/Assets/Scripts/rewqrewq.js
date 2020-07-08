
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
        public computerInfo computerInfo;
        public Image tagHealth;
        public float health;
        // public float newnew;

        // public static float remainingShot = 10f;
        // public Transform shotText;
        void Update()
        {
            CompHealthBar();
            Debug.Log("o shit");
        }

        void Start()
        {
            currentHealth = maxHealth;
        }

        
        public void CompHealthBar()
        {
            // health = computerInfo.TakeDamage(0);
            tagHealth.fillAmount = computerInfo.health / computerInfo.startHealth;           
        }
        

        public void PlayerDamage(float damage)
        {
            if (damage >= currentHealth)
            {
                Dead();
            }
            else
            {
                currentHealth -= damage;
            }
        }
        void Dead()
        {
            currentHealth = 0f;
            isDead = true;
            Debug.Log("Player Is Dead");
            SceneManager.LoadScene("StartMenuA");
        }

        
    }   

}
