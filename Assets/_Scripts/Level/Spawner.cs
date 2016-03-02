using UnityEngine;
using System.Collections;

namespace Assets._Scripts
{
    public class Spawner : MonoBehaviour
    {

        public Vector3 size;
        public float spawnFrequency;
        public GameObject spawnedObject;
        public GameObject ParentGameObject;
        public bool randomRotation;

        private float spawnTime;
        private Transform _transform;

        void Start()
        {
            _transform = GetComponent<Transform>();
        }

        // Update is called once per frame
        void Update()
        {
            spawnTime += Time.deltaTime;
            if (spawnTime < spawnFrequency) return;

            Vector3 position = _transform.position;

            float randomX = Random.Range(0, size.x);
            float randomY = Random.Range(0, size.y);
            float randomZ = Random.Range(0, size.z);

            Vector3 randomPos = new Vector3(position.x + randomX, position.y + randomY, position.z + randomZ);
            Object clone = Instantiate(spawnedObject, randomPos, spawnedObject.GetComponent<Transform>().rotation);

            if (ParentGameObject != null && clone is GameObject)
            {
                Transform cloneTransform = ((GameObject)clone).GetComponent<Transform>();
                cloneTransform.parent = ParentGameObject.transform;

                if (randomRotation)
                    cloneTransform.rotation = Random.rotation;
            }
            spawnTime = 0;
        }
    }
}
