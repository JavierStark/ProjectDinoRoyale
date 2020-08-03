using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runner.Core;

public class Enemy : MonoBehaviour
{
    [Range(0,20)][SerializeField] float velocity;

    void Update(){
        if (GameManager.instance.IsPlayerAlive) {
            transform.Translate(new Vector3(-velocity, 0, 0)*Time.deltaTime*GameManager.instance.GlobalMultiplier());
        }
        else {
            if(this.gameObject.tag == "Enemy") {
                GetComponent<Animator>().SetTrigger("IsGameOver");
            }            
        }
    }
}
