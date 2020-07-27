using Runner.Core;
using UnityEngine;
using System.Collections;

namespace Runner.Player {
    public class PlayerController : MonoBehaviour {

        private float jump;
        private float vertical;
        [Range(1, 30)] [SerializeField] float jumpForce;
        bool invincible = false;
        
        //References to components
        Rigidbody2D playerRB;
        Animator playerAnim;
        AudioSource playerAudioSource;
        [SerializeField] BoxCollider2D colliderRun;
        [SerializeField] BoxCollider2D colliderCrouch;

        //Ground Checker
        [Header("Ground Checker")]
        [SerializeField] Vector2 checkerSize;
        [SerializeField] Transform checkerPosition;
        [SerializeField] LayerMask groundMask;

        //Sound FX
        [Header("Sound FX")]        
        [SerializeField] AudioClip[] jumpSoundFXs = new AudioClip[3];


        private void Start() {
            playerRB = GetComponent<Rigidbody2D>();
            playerAnim = GetComponent<Animator>();
            playerAudioSource = GetComponent<AudioSource>();
        }

        private void Update() {
            jump = SimpleInput.GetAxis("Jump");
            vertical = SimpleInput.GetAxisRaw("Vertical");


            if (IsGrounded()) {
                playerAnim.SetBool("IsJumping", false);
                if (jump > 0) Jump();
            }

            Crouch();
        }

        private void Crouch() {
            if (vertical < 0) {
                playerRB.gravityScale = 3;
                playerAnim.SetBool("IsCrouching", true);
                colliderRun.enabled = false;
                colliderCrouch.enabled = true;
            }
            else {
                playerRB.gravityScale = 1.5f;
                playerAnim.SetBool("IsCrouching", false);
                colliderRun.enabled = true;
                colliderCrouch.enabled = false;
            }
        }
        private void Jump() {
            PlayJumpSFX();      

            playerAnim.SetBool("IsJumping", true);
            playerRB.velocity = new Vector2(0, jumpForce);
        }

        private bool IsGrounded() {
            //return Physics2D.Raycast(checkerPosition.position, new Vector2(0, -1), 0.01f);
            return Physics2D.OverlapBox(checkerPosition.position, checkerSize, 0, groundMask); //cpn esto siempre me devolvía false
        }


        //Draws the ground checker
        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.grey;
            Gizmos.DrawWireCube(checkerPosition.position, checkerSize);
        }

		private void OnTriggerEnter2D(Collider2D c)
		{
            if (!invincible) {
                if (c.CompareTag("Enemy"))
                {
                    GameManager.instance.IsPlayerAlive = false;
                    GameManager.instance.GameOver();
                }
                else
                {
                    Debug.Log(c.name);
                }
            }
        }

        private void PlayJumpSFX() {
            AudioClip clip = jumpSoundFXs[Random.Range(0, jumpSoundFXs.Length)];
            playerAudioSource.clip = clip;
            playerAudioSource.Play();
        }

        public IEnumerator Invincible(int s) {
            playerAnim.SetBool("IsRainbow", true);
            invincible = true;
            yield return new WaitForSeconds(s);
            invincible = false;
            playerAnim.SetBool("IsRainbow", false);
        }
	
	}
}
