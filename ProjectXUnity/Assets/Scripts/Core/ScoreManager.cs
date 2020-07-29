using Runner.Core;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmpScore;
    [SerializeField] TextMeshProUGUI tmpCoins;
    [SerializeField] public TextMeshProUGUI tmpPosition;
    [SerializeField] float scoreDelay;
    [SerializeField] int enemiesAlive = 9;
    public int bonus = 2;

    int score;

    public static ScoreManager instance;

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

        StartCoroutine(IncreaseScore());
    }


    private IEnumerator IncreaseScore()
	{
        if (GameManager.instance.IsPlayerAlive) {
            yield return new WaitForSeconds(scoreDelay);
            score++;
            tmpScore.text = score.ToString();
            if (GameManager.instance.IsPlayerAlive)
		    {
                StartCoroutine(IncreaseScore());
            }
        }
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
}
