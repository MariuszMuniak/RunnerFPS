using FPS.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Environment
{
    public class InstantiatePrefabs : MonoBehaviour
    {
        [SerializeField] bool instantiateOnStart = false;
        [SerializeField] bool instantiateOnDeath = false;
        [SerializeField] Transform spawnPoint = null;
        [SerializeField] List<ObjectToSpawn> objects = new List<ObjectToSpawn>();

        [System.Serializable]
        private class ObjectToSpawn
        {
            public GameObject prefab;
            [Tooltip("Set value between 0 and 1. 1 always spawn, 0 never spawn.")]
            [Range(0, 1)] public float spawnChance = 1f;
        }

        private void Awake()
        {
            if (spawnPoint == null)
            {
                spawnPoint = gameObject.transform;
            }
        }

        void Start()
        {
            if (instantiateOnStart)
            {
                SpawnObjects();
            }

            if (instantiateOnDeath)
            {
                Health health = GetComponent<Health>();

                if (health == null) { return; }

                health.onDeath.AddListener(SpawnObjects);
            }
        }

        public void SpawnObjects()
        {
            if (CanSpawn())
            {
                objects.ForEach(x => SpawnObject(x.prefab, x.spawnChance));
            }
        }

        private bool CanSpawn()
        {
            if (objects.Count > 0)
            {
                return true;
            }
            else
            {
                Debug.Log($"GameObject {gameObject.name} nie ma przypisanych objektow ktore ma wytworzyc.");
                return false;
            }
        }

        private void SpawnObject(GameObject prefab, float spawnChance)
        {
            if (spawnChance == 0) { return; }
            if (prefab == null) { return; }

            float chance = Random.Range(0f, 1f);

            if (chance > spawnChance) { return; }

            Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
