using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class ServerManager : MonoBehaviour
{
    [SerializeField]
    string serverUri = "https://dinoroyale.000webhostapp.com/"; //https://dinoroyale.000webhostapp.com/


    //no estoy seguro si necesitamos que esta clase sea singleton....
    public static ServerManager instance;
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad(this);

	}

	public void GetRanking()
	{
        StartCoroutine(CallRanking());
	}

    IEnumerator CallRanking()
	{
        Debug.Log("SM: llamando al ranking");
        string finalUri = "ranking.php";
        UnityWebRequest request = UnityWebRequest.Get(serverUri + finalUri);
        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            Debug.Log("SM Error llamando al ranking: " + request.error);
            CallRanking();
        }
        else
        {

            try
            {
                Debug.Log("SM: Éxito recuperando ranking: " + JsonHelper.fixJson(request.downloadHandler.text));
                // TipoUnidad[] lista = JsonHelper.FromJson<TipoUnidad>(jsonColeccion);
                RankingPosition[] ranking = JsonHelper.FromJson<RankingPosition>(JsonHelper.fixJson(request.downloadHandler.text));
                foreach (RankingPosition r in ranking)
				{
                    Debug.Log("he recuperado: " + r.nickname + " con una puntuación de "+ r.score);
				}
            }
            catch (System.Exception e)
            {
                Debug.Log("SM: ERROR recuperando ranking: " + e);
            }

        }
    }


}
