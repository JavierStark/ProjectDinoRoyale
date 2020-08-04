using Runner.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ServerManager : MonoBehaviour
{
    [SerializeField]
    string serverUri = "https://dinoroyale.000webhostapp.com/"; //https://dinoroyale.000webhostapp.com/


    //no estoy seguro si necesitamos que esta clase sea singleton....
    public static ServerManager instance;
    SceneFlow sceneFlow;
    [SerializeField] public User user;

    [SerializeField] GameObject loadingPanel;

    List<string> nicknames = new List<string>();
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

        sceneFlow = FindObjectOfType<SceneFlow>();
        if(sceneFlow == null)
		{
            sceneFlow = gameObject.AddComponent<SceneFlow>();
		}
        StartCoroutine(GetNicknamesCall());
        RecoverPlayerPrefs();
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
        Loading(true);
        yield return request.SendWebRequest();
        Loading(false);
        if (request.isNetworkError)
        {
            Debug.Log("SM Error llamando al ranking: " + request.error);
            RankingCall();
        }
        else
        {

            try
            {
                Debug.Log("SM: Éxito recuperando ranking");
                RankingPosition[] ranking = JsonHelper.FromJson<RankingPosition>(JsonHelper.fixJson(request.downloadHandler.text));
                FindObjectOfType<Ranking>().RankingList = ranking.ToList();
               
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
        Loading(true);
        yield return request.SendWebRequest();
        Loading(false);
        if (request.isNetworkError)
        {
            Debug.Log("SM Error intentando registrar usuario: " + request.error);
            SignUserCall(formu);
        }
        else
        {

            try
            {
                string response = request.downloadHandler.text;

                try
				{
                    SignLoginManager slm = FindObjectOfType<SignLoginManager>();
                    Debug.Log(request.downloadHandler.text);
                    if (request.downloadHandler.text.Contains("Error nick"))
					{
                        signLoginManager.txtInfoSign.text = "ESE NICKNAME YA EXISTE";
                        slm.ResetPasswordInputs();
					}
					else
					{
                        User newUser = JsonUtility.FromJson<User>(response);
                        signLoginManager.txtInfoSign.text = "USUARIO REGISTRADO CON ÉXITO";
                        Debug.Log("SM: exito registrando usuario");
                       
                        slm.ToLogIn();
                        slm.ResetInputs();
                        
                    }
					
				}
				catch (System.Exception e)
				{
                    signLoginManager.txtInfoSign.text = "NUESTROS SERVIDORES NO FUNCIONAN \nINTÉNTALO MÁS TARDE";
                    Debug.Log("SM: Error registrando usuario -> " + e);
				}
                
            }
            catch (System.Exception e)
            {
                Debug.Log("SM: ERROR registrando usuario: " + e);
            }
        }
    }

    public void LoginUser(string nickname, string pass, bool keep)
	{
        WWWForm formu = new WWWForm();
        formu.AddField("nickname", nickname);
        formu.AddField("password", pass);
        StartCoroutine(LoginCall(formu, keep));

	}
    IEnumerator LoginCall(WWWForm formu, bool keep)
	{
        SignLoginManager signLoginManager = FindObjectOfType<SignLoginManager>();
        Debug.Log("SM: Login usuario");
        string finalUri = "login.php";
        UnityWebRequest request = UnityWebRequest.Post(serverUri + finalUri, formu);
        Loading(true);
        yield return request.SendWebRequest();
        Loading(false);
        if (request.isNetworkError)
        {
            Debug.Log("SM: Error intentando logear usuario: " + request.error);
            LoginCall(formu, keep);
        }
        else
        {

            try
            {
                string response = request.downloadHandler.text;

                try
                {
                    User newUser = JsonUtility.FromJson<User>(response);
                    user = newUser;
                    signLoginManager.txtInfoLogin.text = "¡BIENVENIDO!";
                    Debug.Log("se ha logeado: "+ newUser.nickname);

                    if (keep)
					{
                        Debug.Log("salvando player prefs");
                        PlayerPrefs.SetString("nickname", newUser.nickname);
                        PlayerPrefs.Save();
                    }
                    sceneFlow.ChangeScene("MainMenuScene");
                }
                catch (System.Exception e)
                {
                    signLoginManager.txtInfoLogin.text = "USUARIO O PASSWORD INCORRECTO";
                    Debug.Log("SM: Error logeando -> " + e);
                }

            }
            catch (System.Exception e)
            {
                Debug.Log("SM: ERROR logeando: " + e);
            }
			
        }
    }

    public void NewScore()
	{
        int finalScore = ((10 - Int32.Parse(ScoreManager.instance.tmpPosition.text)) * ScoreManager.instance.bonus) + ScoreManager.instance.GetScore();

        WWWForm formu = new WWWForm();
        formu.AddField("nickname", user.nickname);
        formu.AddField("score", finalScore);
        Debug.Log("SM: intentando salvar " + finalScore +" puntos, para: " + user.nickname);
        StartCoroutine(NewScoreCall(formu));
	}

    IEnumerator NewScoreCall(WWWForm formu)
	{
        string finalUri = "nuser.php";
        UnityWebRequest request = UnityWebRequest.Post(serverUri + finalUri, formu);
        Loading(true);
        yield return request.SendWebRequest();
        Loading(false);
    }

    public List<string> GetNicknames()
	{
        return nicknames;
	}

    IEnumerator GetNicknamesCall()
	{
        Debug.Log("SM: recuperando nicknames de la BD");
        string finalUri = "nicknames.php";
        UnityWebRequest request = UnityWebRequest.Get(serverUri + finalUri);
        Loading(true);
        yield return request.SendWebRequest();
        Loading(false);
        if (request.isNetworkError)
        {
            Debug.Log("SM: Error recuperando nicks: " + request.error);
            Debug.Log("SM: Reintentando recuperar nicks...");
            GetNicknames();
		}
		else
		{
            User[] users = JsonHelper.FromJson<User>(JsonHelper.fixJson(request.downloadHandler.text));
            foreach (User u in users)
            {
                if (u.nickname != user.nickname)
                {
                    nicknames.Add(u.nickname);
                }

            }
        }
       
    }

    public void RecoverPlayerPrefs()
	{
        string nicknamePref = "";
        nicknamePref = PlayerPrefs.GetString("nickname");
        if (nicknamePref != "")
		{
            Debug.Log("tengo un nick -> " + nicknamePref);
            user.nickname = nicknamePref;
            sceneFlow.ChangeScene("MainMenuScene");
		}
	}

    public void Logout()
	{
        Debug.Log("Desconectando usuario");
        PlayerPrefs.DeleteKey("nickname");
        PlayerPrefs.Save();
        user = null;

        sceneFlow.ChangeScene("LoginScene");

	}

    private void Loading(bool loading) {
        if (loading) loadingPanel.SetActive(true);
        else loadingPanel.SetActive(false);
    }

}
