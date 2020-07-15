using Runner.Core;
using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmpScore;
    [SerializeField] TextMeshProUGUI tmpPosition;
    [SerializeField] int scoreMultiplier;
    [SerializeField] int enemiesAlive = 9;

    int score;

    void Start()
    {
        GameManager.instance.IsPlayerAlive = true;
        //Esto solo lo he puesto x si se nos olvida setearlo desde el editor de Unity, q nunca explote
        if (scoreMultiplier <= 0)
		{
            scoreMultiplier = 1;
		}

        tmpPosition.text = "10";

        StartCoroutine(IncreaseScore());
    }

    // Update is called once per frame
    void Update()
    {

              
    }

    private IEnumerator IncreaseScore()
	{
        if (GameManager.instance.IsPlayerAlive) {
            yield return new WaitForSeconds(1f);
            score++;
            tmpScore.text = "Score: " + score; 
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
}
