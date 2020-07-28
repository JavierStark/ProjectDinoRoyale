using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
	List<RankingPosition> rankingList;
	List<RankingPositionBehaviour> positionList;
	bool complete = false;

	public List<RankingPosition> RankingList
	{
		get
		{
			return rankingList;
		}
		set
		{
			rankingList = value;
		}
	}
	void Start()
	{
		ServerManager.instance.GetRanking();
		gameObject.GetComponentsInChildren<Text>();
		positionList = GetComponentsInChildren<RankingPositionBehaviour>().ToList();
	}

	private void Update()
	{
		if(rankingList != null)
		{
			if (!complete)
			{
				ShowRanking();
			}
		}
	}

	void ShowRanking()
	{
		int i = 0;
		Debug.Log("mostrando ranking");
		foreach (RankingPosition position in rankingList)
		{
			RankingPositionBehaviour rankingPositionBehaviour = positionList.ElementAt(i);
			int nPosition = i + 1;
			rankingPositionBehaviour.txtPosition.text = "" + nPosition;
			rankingPositionBehaviour.txtNickname.text = position.nickname;
			rankingPositionBehaviour.txtScore.text = position.score;
			i++;
		}
		complete = true;
	}
}
