using Runner.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI tmpScore;
    [SerializeField] int scoreMultiplier;

    int score;
    void Start()
    {
        //Esto solo lo he puesto x si se nos olvida setearlo desde el editor de Unity, q nunca explote
        if (scoreMultiplier <= 0)
		{
            scoreMultiplier = 1;
		}
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsPlayerAlive)
		{
            IncreaseScore();
        }
       
        tmpScore.text = "Score: " + score;
    }

    void IncreaseScore()
	{
        score = (int)Time.fixedTime * scoreMultiplier;
	}
}
