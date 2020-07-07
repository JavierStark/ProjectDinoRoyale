using Runner.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
        [SerializeField] Vector2 checkerSize;
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
			          

            if(vertical < 0 && IsGrounded()) {
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
            return Physics2D.Raycast(checkerPosition.position, new Vector2(0, -1), 0.01f);
            //return Physics2D.OverlapBox(checkerPosition.position, checkerSize, 0, groundMask); //cpn esto siempre me devolvía false
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

		private void OnTriggerEnter2D(Collider2D c)
		{
            if (c.CompareTag("Enemy"))
            {
                Debug.Log(gameObject + ": Me muero...");
                GameManager.instance.IsPlayerAlive = false;
            }
            else
            {
                Debug.Log(c.name);
            }
        }
	
	}
}
