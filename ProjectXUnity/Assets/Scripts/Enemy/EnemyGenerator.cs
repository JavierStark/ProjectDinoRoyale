using System.Collections;
using UnityEngine;

namespace Runner.Enemy {
    public class EnemyGenerator : MonoBehaviour {

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

        private void SpawnEnemy() {
            Instantiate(enemyToSpawn, transform.position, Quaternion.identity, enemyParent);
        }
    }


}