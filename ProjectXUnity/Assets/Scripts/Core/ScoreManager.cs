using Runner.Core;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmpScore;
    [SerializeField] TMP_Text gameOverScore;
    [SerializeField] TMP_Text scoreByPosition;
    [SerializeField] public TextMeshProUGUI tmpPosition;
    [SerializeField] float scoreDelay;
    [SerializeField] int enemiesAlive = 9;
    public int bonus = 2;

    [SerializeField] int score;

    public static ScoreManager instance;

    private IEnumerator increaseScoreCoroutine;

	private void Awake()
	{
	    if (instance == null)
		{
            instance = this;
		}
		else
		{
            Destroy(this);
		}
	}

	void Start()
    {
        GameManager.instance.IsPlayerAlive = true;
        //Esto solo lo he puesto x si se nos olvida setearlo desde el editor de Unity, q nunca explote
        if (scoreDelay <= 0)
		{
            scoreDelay = 1;
		}

        tmpPosition.text = "10";

        increaseScoreCoroutine = IncreaseScore();
        StartCoroutine(increaseScoreCoroutine);
    }


    private IEnumerator IncreaseScore()
	{
        do {
            if (GameManager.instance.IsPlayerAlive) yield return 0;
            yield return new WaitForSeconds(scoreDelay);
            if (!GameManager.instance.IsPlayerAlive) yield return 0;
            score++;
            if (!GameManager.instance.IsPlayerAlive) yield return 0;
            tmpScore.text = score.ToString();
        } while (GameManager.instance.IsPlayerAlive);
    }

    public void EnemyDied (){
        enemiesAlive--;
        tmpPosition.text = (enemiesAlive+1).ToString();
    }

    public int GetScore()
	{
        return score;
	}

    public void PayScore(int valueToPay) {
        if(valueToPay <= score) {
            score -= valueToPay;
        }
        else {
            return;
        }
    }

    public void GameOver() {        
        int finalScore = ((10 - Int32.Parse(tmpPosition.text)) * bonus) + score;
        int bonusScore = (10 - Int32.Parse(tmpPosition.text))*bonus;
        gameOverScore.text = (score).ToString() + " + " + bonusScore + " = " + finalScore;        
    }

    public void StopScoring() {
        StopCoroutine(increaseScoreCoroutine);
        increaseScoreCoroutine = null;
    }
}
