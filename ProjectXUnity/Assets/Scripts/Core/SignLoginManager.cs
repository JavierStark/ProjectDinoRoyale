using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Linq;

public class SignLoginManager : MonoBehaviour
{
    GameObject panelLogin, panelSign;
    public TMP_Text txtInfo;

    bool error = false;

    string emptyNicknameMessage = "El nickname no puede estar en blanco";
    string emptyPasswordMessage = "La contraseña no puede estar en blanco";
    string failConfirmMessage = "La contraseña y la confirmación deben ser iguales";   
    string correctFormMessage = "¡Todo Ok!";

    string nickname = null;
    string pass = null;

    void Start()
    {
       panelLogin = GameObject.Find("PanelLogin");
       panelSign = GameObject.Find("PanelSign");
       txtInfo = GameObject.Find("TxtInfo").GetComponent<TMP_Text>();
    }

    public void CheckCredentials()
	{
        error = false;
        //string nickname = null;
        //string pass = null;
        string confirmPass = null;
        TMP_InputField[] inputs = panelSign.GetComponentsInChildren<TMP_InputField>();
        //txtInfo = GameObject.Find("TxtInfo").GetComponent<TMP_Text>();
        foreach (TMP_InputField i in inputs)
        {
            if (i.name == "InputPassword")
            {
                pass = i.text;

            }
            else if (i.name == "InputConfirmPassword")
            {
                confirmPass = i.text;
            }else if (i.name == "InputNickname")
			{
                nickname = i.text;
			}
        }

        if (nickname == String.Empty || nickname.Trim() == String.Empty)
		{
            error = true;
            txtInfo.text = emptyNicknameMessage.ToUpper();
		}
        else if (pass != String.Empty)
        {
            if (pass != confirmPass)
            {
                error = true;
                txtInfo.text = failConfirmMessage.ToUpper();
            }
          
		}
		else if (pass == String.Empty)
		{
            error = true;
            txtInfo.text = emptyPasswordMessage.ToUpper();
		}

		if (error == false) {
            txtInfo.text = correctFormMessage.ToUpper();
        }
    }

    public void SignUser()
	{
        if (!error)
		{
            Debug.Log("voy a llamar al registro");
            WWWForm formu = new WWWForm();
			formu.AddField("nickname", nickname);
			formu.AddField("password", pass);
			ServerManager.instance.SignUser(formu);
        }
		else
		{
            Debug.Log("hay algun error en el formulario y no estoy llamando");
		}
    }

   
}
