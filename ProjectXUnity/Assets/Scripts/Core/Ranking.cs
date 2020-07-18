using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranking : MonoBehaviour
{

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.T))
		{
			Debug.Log("Ranking...llamando al SM....");
			ServerManager.instance.GetRanking();
		}
	}
}
