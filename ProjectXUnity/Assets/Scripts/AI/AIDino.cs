using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Runner.Core;

public class AIDino : MonoBehaviour
{
    const int MAX_DIFFICULTY = 15;
    const int MAX_TIME = 10;


    int difficulty;
    int minJumps;
    bool dead = false;
       

    [SerializeField] TMP_Text dinoName;
    [SerializeField] Image faceImage;
    [SerializeField] Image crossImage;
    [SerializeField] DinosScriptableObject dinoInfo;
    [SerializeField] GameObject explosionParticles;
    Animator animator;



    IEnumerator Start()
    {        
        InitialSetup();

        while (!dead) {
            if (!GameManager.instance.IsPlayerAlive) break;
		    yield return new WaitForSeconds(ThrowDice(MAX_TIME));
            if (!GameManager.instance.IsPlayerAlive) break;
            TryJump();
		}
	}

    private void InitialSetup() {
        crossImage.enabled = false;
        animator = GetComponent<Animator>();

        dinoName.text = dinoInfo.GetName();
        faceImage.sprite = dinoInfo.GetFace();       

        difficulty = ThrowDice(MAX_DIFFICULTY);
        minJumps = ThrowDice(4);
    }

    private void TryJump() {
        if(minJumps > 0) {
            minJumps--;
            Jump();            
        }
        else if(ThrowDice(MAX_DIFFICULTY) >= difficulty) {
            Jump();
        }
        else Death();
    }

    private void Jump() {
        animator.SetTrigger("isJumping");
    }

    private int ThrowDice(int max) {
        int randomNumber = Random.Range(1, max);
        return randomNumber;
    }

    private void Death() {
        dinoName.color = new Color32(0, 0, 0, 0);
        crossImage.enabled = true;
        dead = true;

        FindObjectOfType<ScoreManager>().EnemyDied();
    }

    public bool IsAlive() {
        return !dead;
    }

    public void Attacked(int diceFaces) {
        GameObject currentParticles = Instantiate(explosionParticles, new Vector3(crossImage.transform.position.x ,crossImage.transform.position.y,-5) , explosionParticles.transform.rotation);
        Destroy(currentParticles, 0.5f);
        Death();        
    }
}