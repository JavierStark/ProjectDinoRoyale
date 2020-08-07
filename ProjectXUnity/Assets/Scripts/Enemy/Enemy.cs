using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runner.Core;

public class Enemy : MonoBehaviour
{
    [Range(0,20)][SerializeField] float velocity;

    void Update(){
        if (GameManager.instance.IsPlayerAlive) {
            if(gameObject.tag == "BackMeteorite") {
                transform.Translate(new Vector3(-1, -1, 0)*velocity*Time.deltaTime*GameManager.instance.GlobalMultiplier());
            }
            else {
                transform.Translate(Vector3.left*velocity*Time.deltaTime*GameManager.instance.GlobalMultiplier());
            }            
        }
        else {
            try {
                GetComponent<Animator>().SetTrigger("IsGameOver");
            }
            catch {
               
            }
        }
    }
}
