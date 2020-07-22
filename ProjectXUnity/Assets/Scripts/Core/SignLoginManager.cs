using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SignLoginManager : MonoBehaviour
{
    GameObject panelLogin, panelSign;
    TMP_Text txtInfo;
    string emptyNicknameMessage = "El nickname no puede estar en blanco";
    string emptyPasswordMessage = "La contraseña no puede estar en blanco";
    string failConfirmMessage = "La contraseña y la confirmación deben ser iguales";
    
    string correctFormMessage = "¡Todo Ok!";

    void Start()
    {
       panelLogin = GameObject.Find("PanelLogin");
       panelSign = GameObject.Find("PanelSign");
    }

    public void CheckCredentials()
	{
        bool error = false;
        string nickname = null;
        string pass = null;
        string confirmPass = null;
        TMP_InputField[] inputs = panelSign.GetComponentsInChildren<TMP_InputField>();
        txtInfo = GameObject.Find("TxtInfo").GetComponent<TMP_Text>();
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


        if (nickname == String.Empty)
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

   
}
