using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    Animator animator;


    IEnumerator Start()
    {        
        InitialSetup();

        while (!dead) {
            yield return new WaitForSeconds(ThrowDice(MAX_TIME));

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
        animator.SetBool("isDead", true);
        crossImage.enabled = true;
        dead = true;
    }
}