using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner.Core {
    public class GameManager : MonoBehaviour {

		[SerializeField] bool isPlayerAlive = true;
		[SerializeField] DinosScriptableObject dinosScriptableObject;


		public static GameManager instance;
		private void Awake()
		{
			if (instance == null)
			{
				instance = this;
			}
			else
			{
				Destroy(this);
			}
			DontDestroyOnLoad(gameObject);

			dinosScriptableObject.Reset();
		}

		public bool IsPlayerAlive {
			get
			{
                return isPlayerAlive;
			}
			set
			{
				isPlayerAlive = value;
			}
        }
    }

}