using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Range(0,10)][SerializeField] float velocity;

    void Update(){
        transform.Translate(new Vector3(-velocity, 0, 0)*Time.deltaTime);
    }
}
