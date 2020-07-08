using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace TowerDefence
{
    public class WaveSpawner : MonoBehaviour
    {

        public static int EnemiesAlive = 0;

        public Wave[] waves;

        private float s = 1;

        public Transform spawnPoint;
        public Transform spawn2;
        public Transform spawn3;
        public Transform spawn4;

        public float timeBetweenWaves = 5f;
        private float countdown = 2f;

        private int waveIndex = 0;
        private void Start()
        {
            EnemiesAlive = 0;
        }
        void Update()
        {

            if (EnemiesAlive > 0)
            {
                return;
            }
            else if (waveIndex == waves.Length && EnemiesAlive <= 0)
            {

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                SceneManager.LoadScene("WinScreen");
            }

            //if (waveIndex == waves.Length)
            //{
            //    this.enabled = false;
            //}
           
            if (countdown <= 0f)
            {
              
                if (s < 3)
                {
                    s++;
                }
                else
                {
                    s = 1;
                }
                StartCoroutine(SpawnWave());
                countdown = timeBetweenWaves;
                return;
            }

            countdown -= Time.deltaTime;

            countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);


           // waveCountdownText.text = string.Format("{0:00.00}", countdown);
        }

        IEnumerator SpawnWave()
        {
            PlayerStats.Rounds++;

            Wave wave = waves[waveIndex];

            EnemiesAlive = wave.count;

            for (int i = 0; i < wave.count; i++)
            {
                if (s == 1)
                {
                    s++;
                    SpawnEnemy(wave.enemy);
                    yield return new WaitForSeconds(1f / wave.rate);
                }
                else if (s == 2)
                {
                    s++;
                    SpawnEnemy2(wave.enemy);
                    yield return new WaitForSeconds(1f / wave.rate);
                    
                }
                else if (s == 3)
                {
                    s++;
                    SpawnEnemy3(wave.enemy);
                    yield return new WaitForSeconds(1f / wave.rate);
                    
                }
                else
                {
                    s = 1;
                    SpawnEnemy4(wave.enemy);
                    yield return new WaitForSeconds(1f / wave.rate);
                    
                }
            }

            waveIndex++;
        }

        void SpawnEnemy(GameObject enemy)
        {
            Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        }
        void SpawnEnemy2(GameObject enemy)
        {
            Instantiate(enemy, spawn2.position, spawn2.rotation);
        }
        void SpawnEnemy3(GameObject enemy)
        {
            Instantiate(enemy, spawn3.position, spawn3.rotation);
        }
        void SpawnEnemy4(GameObject enemy)
        {
            Instantiate(enemy, spawn4.position, spawn4.rotation);
        }

    }
}
