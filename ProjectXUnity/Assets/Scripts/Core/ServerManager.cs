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
        StartCoroutine(RankingCall());
	}

    IEnumerator RankingCall()
	{
        Debug.Log("SM: llamando al ranking");
        string finalUri = "ranking.php";
        UnityWebRequest request = UnityWebRequest.Get(serverUri + finalUri);
        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            Debug.Log("SM Error llamando al ranking: " + request.error);
            RankingCall();
        }
        else
        {

            try
            {
                Debug.Log("SM: Éxito recuperando ranking: " + JsonHelper.fixJson(request.downloadHandler.text));
                // TipoUnidad[] lista = JsonHelper.FromJson<TipoUnidad>(jsonColeccion);
                RankingPosition[] ranking = JsonHelper.FromJson<RankingPosition>(JsonHelper.fixJson(request.downloadHandler.text));
                FindObjectOfType<Ranking>().RankingList = ranking.ToList();
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
    public void SignUser(WWWForm formu)
	{
        StartCoroutine(SignUserCall(formu));
	}
    IEnumerator SignUserCall(WWWForm formu)
	{
		SignLoginManager signLoginManager = FindObjectOfType<SignLoginManager>();
		Debug.Log("SM: registrando usuario");
        string finalUri = "nuser.php";
        UnityWebRequest request = UnityWebRequest.Post(serverUri + finalUri, formu);
        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            Debug.Log("SM Error intentando registrar usuario: " + request.error);
            SignUserCall(formu);
        }
        else
        {

            try
            {
                Debug.Log("jumanjiii");
                string response = request.downloadHandler.text;

                try
				{
                    User newUser = JsonUtility.FromJson<User>(response);
                    signLoginManager.txtInfo.text = "USUARIO REGISTRADO CON ÉXITO";
                    //tenemos q llevar al tio al menú de login (o directamente meterlo en el juego, lo q veamos)
                    Debug.Log("exito registrando");
				}
				catch (System.Exception e)
				{
                    signLoginManager.txtInfo.text = "ALGO HA IDO MAL CON EL REGISTRO";
                    Debug.Log("SM: Error registrando usuario -> " + e);
				}
                
            }
            catch (System.Exception e)
            {
                Debug.Log("SM: ERROR registrando usuario: " + e);
            }
        }
    }


}
