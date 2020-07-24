using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runner.Enemy;
using Runner.Core;

public class PropGenerator : Generator
{

    private void Start() {
		StartCoroutine(GenerateProp());
    }

    private IEnumerator GenerateProp() {
		if (GameManager.instance.IsPlayerAlive) {			
			yield return this.Spawn();
			StartCoroutine(GenerateProp());
		}
	}
}
