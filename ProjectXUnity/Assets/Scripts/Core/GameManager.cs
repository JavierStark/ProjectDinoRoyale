using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runner.Enemy;

namespace Runner.Core {
    public class GameManager : MonoBehaviour {

		[SerializeField] bool isPlayerAlive = true;
		[SerializeField] DinosScriptableObject dinosScriptableObject;
		[SerializeField] Canvas gameOverCanvas;

		EnemyGenerator[] generators;

		public static GameManager instance;

		private void Awake(){
			instance = this;
			
			dinosScriptableObject.Reset();
		}

        private void Start() {
            if(IsPlayerAlive){
				generators = FindObjectsOfType<EnemyGenerator>();
				Time.timeScale = 1;
				gameOverCanvas.gameObject.SetActive(false);
				StartCoroutine(Generate());
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

		private IEnumerator Generate() {
			if (IsPlayerAlive) {
				print("alive");
				int randomGenerator = Random.Range(0, generators.Length);
				yield return generators[randomGenerator].Spawn();
				StartCoroutine(Generate());
			}			
        }	

		public void GameOver() {
			print("gameOver");
			gameOverCanvas.gameObject.SetActive(true);
		}
	}

}