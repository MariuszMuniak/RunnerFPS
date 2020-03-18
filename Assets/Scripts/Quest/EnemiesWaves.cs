using FPS.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FPS.Quest
{
    public class EnemiesWaves : MonoBehaviour
    {
        [Range(1, 10)]
        [SerializeField] int wavesNumbers = 5;
        [SerializeField] int minEnemiesNumberInWave = 1;
        [SerializeField] int maxEnemiesNumerInWave = 5;
        [Range(1, 60)]
        [SerializeField] float timeBetweenWaves = 30f;
        [SerializeField] Enemie[] enemies;
        [SerializeField] Transform[] spawnPoints;

        Transform player;

        int[] maxNumbersInWave;
        int[] limitedNumbers;
        bool isActive = false;
        float timeSinceLastWaveWasSpawn = Mathf.Infinity;
        int wavesCounter = 0;

        [System.Serializable]
        public class Enemie
        {
            public GameObject gameObject = null;
            public bool limitedNumberInWave = false;
            [Range(1, 5)]
            public int maxNumberInWave = 1;
        }

        private void Awake()
        {
            player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }

        private void Start()
        {
            InitialMaxNumbersInWave();
        }

        void Update()
        {
            if (!isActive) { return; }
            if (wavesCounter == wavesNumbers) { isActive = false; return; }

            if (timeBetweenWaves < timeSinceLastWaveWasSpawn)
            {
                SpawnWave();
            }

            timeSinceLastWaveWasSpawn += Time.deltaTime;
        }

        public void Activate()
        {
            isActive = true;
        }

        public void Deactivate()
        {
            isActive = false;
        }

        private void InitialMaxNumbersInWave()
        {
            maxNumbersInWave = new int[enemies.Length];

            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].limitedNumberInWave)
                {
                    maxNumbersInWave[i] = enemies[i].maxNumberInWave;
                }
                else
                {
                    maxNumbersInWave[i] = Mathf.RoundToInt(Mathf.Infinity);
                }
            }
        }

        private void SpawnWave()
        {
            timeSinceLastWaveWasSpawn = 0;
            wavesCounter++;

            limitedNumbers = maxNumbersInWave;

            for (int i = 0; i < Random.Range(minEnemiesNumberInWave, maxEnemiesNumerInWave); i++)
            {
                GameObject enemy = Instantiate(RandomEnemy(), RandomPosition(), Quaternion.identity);
                enemy.GetComponent<AIMover>().MoveTo(player.position, 1.25f);
            }
        }

        private GameObject RandomEnemy()
        {
            int index;

            do
            {
                index = Random.Range(0, enemies.Length);
            }
            while (limitedNumbers[index] == 0);

            limitedNumbers[index]--;

            return enemies[index].gameObject;
        }

        private Vector3 RandomPosition()
        {
            int index = Random.Range(0, spawnPoints.Length);

            return spawnPoints[index].position;
        }
    }
}