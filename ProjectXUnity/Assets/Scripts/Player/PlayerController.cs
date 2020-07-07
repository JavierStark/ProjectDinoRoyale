using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Numerics;
using UnityEditor;
using UnityEditor.U2D.Path;
using UnityEngine;

namespace Runner.Player {
    public class PlayerController : MonoBehaviour {

        private float jump;
        private float vertical;
        [Range(1, 30)] [SerializeField] float jumpForce;
        
        //References to components
        Rigidbody2D playerRB;
        Animator playerAnim;
        [SerializeField] BoxCollider2D colliderRun;
        [SerializeField] BoxCollider2D colliderCrouch;

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
            vertical = Input.GetAxisRaw("Vertical");

            if (IsGrounded()) {
                playerAnim.SetBool("IsJumping", false);
                if(jump > 0){ Jump(); }                
            }            

            if(vertical < 0) {
                playerAnim.SetBool("IsCrouching",true);
                colliderRun.enabled = false;
                colliderCrouch.enabled = true;
            }
            else {
                playerAnim.SetBool("IsCrouching", false);
                colliderRun.enabled = true;
                colliderCrouch.enabled = false;
            }
        }

        private bool IsGrounded() {
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
