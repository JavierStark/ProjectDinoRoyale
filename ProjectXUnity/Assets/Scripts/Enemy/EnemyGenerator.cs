using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runner.Core;
using System;

namespace Runner.Enemy {
    public class EnemyGenerator : MonoBehaviour {

        [SerializeField] GameObject enemyToSpawn;
        [SerializeField] Transform enemyParent;
        GameManager gameManager;

        [SerializeField] float minTimeBetweenSpawns;
        [SerializeField] float maxTimeBetweenSpawns;
        float timeToWait;

        private IEnumerator Start() {
            gameManager = GameManager.instance;
            while (gameManager.IsPlayerAlive) {
                timeToWait = UnityEngine.Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
                yield return new WaitForSeconds(timeToWait);
                SpawnEnemy();
            }
        }

        private void SpawnEnemy() {
            Instantiate(enemyToSpawn, transform.position, Quaternion.identity, enemyParent);
        }
    }

}