using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runner.Enemy;

namespace Runner.Core {
    public class GameManager : MonoBehaviour {

		[SerializeField] bool isPlayerAlive = true;
		[SerializeField] DinosScriptableObject dinosScriptableObject;

		EnemyGenerator[] generators;

		public static GameManager instance;

		private void Awake(){ 
			if (instance == null){ 
				instance = this;
			}
            else{ 
				Destroy(this);
			}
			
			DontDestroyOnLoad(gameObject);
			dinosScriptableObject.Reset();
		}

        private void Start() {
			generators = FindObjectsOfType<EnemyGenerator>();
			StartCoroutine(Generate());
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
			int randomGenerator = Random.Range(0, generators.Length);
			yield return generators[randomGenerator].Spawn();

            if (IsPlayerAlive) {
				StartCoroutine(Generate());
            }
        }	

    }

}