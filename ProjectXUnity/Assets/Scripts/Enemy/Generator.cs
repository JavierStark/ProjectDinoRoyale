using System.Collections;
using UnityEngine;
using Runner.Core;

namespace Runner.Enemy {
    public class Generator : MonoBehaviour {

        [SerializeField] GameObject[] enemyToSpawn;
 
        [SerializeField] Transform enemyParent;

        [SerializeField] float minTimeBetweenSpawns;
        [SerializeField] float maxTimeBetweenSpawns;
        float timeToWait;


        public IEnumerator Spawn() {
            if (GameManager.instance.IsPlayerAlive) {
                float timeToWait = UnityEngine.Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
                yield return new WaitForSeconds(timeToWait);
                SpawnEnemy();
            }            
        }

        protected void SpawnEnemy() {
            Instantiate(enemyToSpawn[Random.Range(0, enemyToSpawn.Length)], transform.position, Quaternion.identity, enemyParent);
        }
    }


}