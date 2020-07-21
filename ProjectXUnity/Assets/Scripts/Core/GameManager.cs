using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runner.Enemy;

namespace Runner.Core {
    public class GameManager : MonoBehaviour {

		[SerializeField] bool isPlayerAlive = true;
		[SerializeField] DinosScriptableObject dinosScriptableObject;
		[SerializeField] Canvas gameOverCanvas;

		EnemyGenerator[] enemiesGenerators;
		PropGenerator[] propGenerators;

		public static GameManager instance;

		private void Awake(){
			instance = this;
			
			dinosScriptableObject.Reset();
		}

        private void Start() {
            if(IsPlayerAlive){
				enemiesGenerators = FindObjectsOfType<EnemyGenerator>();
				propGenerators = FindObjectsOfType<PropGenerator>();
				Time.timeScale = 1;
				gameOverCanvas.gameObject.SetActive(false);
				StartCoroutine(GenerateEnemy());
				StartCoroutine(GenerateProp());
            }
		}


        public bool IsPlayerAlive{
			get{
                return isPlayerAlive;
			}
			set{
				isPlayerAlive = value;
			}
        }

		private IEnumerator GenerateEnemy() {
			print("Generate");
			if (IsPlayerAlive) {
				int randomGenerator = Random.Range(0, enemiesGenerators.Length);
				yield return enemiesGenerators[randomGenerator].Spawn();				
				StartCoroutine(GenerateEnemy());
			}			
        }
		private IEnumerator GenerateProp() {
			print("Generate");
			if (IsPlayerAlive) {
				int randomGenerator = Random.Range(0, propGenerators.Length);
				yield return propGenerators[randomGenerator].Spawn();
				StartCoroutine(GenerateProp());
			}
		}

		public void GameOver() {
			gameOverCanvas.gameObject.SetActive(true);
		}
	}

}