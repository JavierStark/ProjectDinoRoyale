using System.Collections;
using UnityEngine;

namespace Runner.Enemy {
    public class Generator : MonoBehaviour {

        [SerializeField] GameObject enemyToSpawn;
        [SerializeField] Transform enemyParent;

        [SerializeField] float minTimeBetweenSpawns;
        [SerializeField] float maxTimeBetweenSpawns;
        float timeToWait;


        public IEnumerator Spawn() {            
            float timeToWait = UnityEngine.Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
            yield return new WaitForSeconds(timeToWait);
            SpawnEnemy();
        }

        protected void SpawnEnemy() {
            Instantiate(enemyToSpawn, transform.position, Quaternion.identity, enemyParent);
        }
    }


}