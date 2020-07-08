using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float velocity;

    
    // Update is called once per frame
    void Update(){
        Vector3 movement = new Vector3(-velocity, 0,0);
        transform.Translate(movement*Time.deltaTime);
    }
}
