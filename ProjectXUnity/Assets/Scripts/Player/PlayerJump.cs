using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Numerics;
using UnityEditor;
using UnityEngine;

namespace Runner.Player {
    public class PlayerJump : MonoBehaviour {

        private float jump;
        [Range(1, 30)] [SerializeField] float jumpForce;
        
        //References to components
        Rigidbody2D playerRB;
        Animator playerAnim;

        //Ground Checker
        [Header("Ground Checker")]
        [SerializeField] UnityEngine.Vector2 checkerSize;
        [SerializeField] Transform checkerPosition;
        [SerializeField] LayerMask groundMask;


        private void Start() {
            playerRB = GetComponent<Rigidbody2D>();
            playerAnim = GetComponent<Animator>();
        }

        private void Update() {
            jump = Input.GetAxis("Jump");
            if (IsGrounded()) {
                playerAnim.SetBool("IsJumping", false);
                if(jump > 0){ Jump(); }                
            }            
        }

        private bool IsGrounded() {
            Debug.Log("Ground");
            bool isGrounded = Physics2D.OverlapBox(checkerPosition.position, checkerSize, 0, groundMask);
            return isGrounded;
        }

        private void Jump() {
            playerAnim.SetBool("IsJumping", true);
            playerRB.velocity = new UnityEngine.Vector2(0, jumpForce);
        }

        //Draws the ground checker
        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.grey;
            Gizmos.DrawWireCube(checkerPosition.position, checkerSize);
        }
    }
}
