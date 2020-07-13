using Runner.Core;
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
        //Esto solo lo he puesto x si se nos olvida setearlo desde el editor de Unity, q nunca explote
        if (scoreMultiplier <= 0)
		{
            scoreMultiplier = 1;
		}

        tmpPosition.text = "10";
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsPlayerAlive)
		{
            IncreaseScore();
        }
              
    }

    void IncreaseScore()
	{
        score = (int)Time.fixedTime * scoreMultiplier;
        tmpScore.text = "Score: " + score;
    }

    public void EnemyDied (){
        enemiesAlive--;
        tmpPosition.text = (enemiesAlive+1).ToString();
    }
}
