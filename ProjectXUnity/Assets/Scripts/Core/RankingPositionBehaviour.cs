using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankingPositionBehaviour : MonoBehaviour
{
    //solo los pongo serializables para comprobarlos en debug
    [SerializeField]
    public TMP_Text txtPosition, txtNickname, txtScore;
    void Start()
    {
		TMP_Text[] txts = GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text txt in txts)
		{
            if (txt.name == "TxtPosition")
			{
                txtPosition = txt;
			}
            else if (txt.name == "TxtNick")
			{
                txtNickname = txt;
			}
            else if(txt.name == "TxtScore")
			{
                txtScore = txt;
			}
		}
    }
}
