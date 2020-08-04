using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Linq;

public class SignLoginManager : MonoBehaviour
{
    GameObject panelLogin, panelSign;
    public TMP_Text txtInfoSign, txtInfoLogin;
    Toggle checkBoxKeep;

    bool error = false;

    string emptyNicknameMessage = "El nickname no puede estar en blanco";
    string nicknameTooLongMessage = "El nickname no puede tener más de 14 caracteres";
    string passwordTooLongMessage = "La contraseña no puede tener más de 20 caracteres";
    string emptyPasswordMessage = "La contraseña no puede estar en blanco";
    string failConfirmMessage = "La contraseña y la confirmación deben ser iguales";   
    string correctFormMessage = "¡Todo Ok!";

    string nickname = null;
    string pass = null;

    void Start()
    {
       panelLogin = GameObject.Find("PanelLogin");
       panelSign = GameObject.Find("PanelSign");
       txtInfoSign = panelSign.GetComponentsInChildren<TMP_Text>().Where(d => d.gameObject.name == "TxtInfo").First();
       txtInfoLogin = panelLogin.GetComponentsInChildren<TMP_Text>().Where(d => d.gameObject.name == "TxtInfo").First();
       checkBoxKeep = panelLogin.GetComponentsInChildren<Toggle>().Where(d => d.gameObject.name == "CheckBoxKeep").First();
    }

    public void CheckCredentials()
	{
        error = false;
        string confirmPass = null;
        TMP_InputField[] inputs = panelSign.GetComponentsInChildren<TMP_InputField>();
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

        if (nickname == string.Empty )
		{
            error = true;
            txtInfoSign.text = emptyNicknameMessage.ToUpper();
		}else if (nickname.Length >= 15)
		{
            error = true;
            txtInfoSign.text = nicknameTooLongMessage.ToUpper();

        }
        else if (pass == string.Empty)
        {
            error = true;
            txtInfoSign.text = emptyPasswordMessage.ToUpper();
        }
        else if (pass != string.Empty  )
        {
            if (pass.Length >= 21)
			{
                error = true;
                txtInfoSign.text = passwordTooLongMessage.ToUpper();
			}
            else if (pass != confirmPass)
            {
                error = true;
                txtInfoSign.text = failConfirmMessage.ToUpper();
            }
          
		}
		

		if (error == false) {
            txtInfoSign.text = correctFormMessage.ToUpper();
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

    public void Login()
	{
        TMP_InputField[] inputs = panelLogin.GetComponentsInChildren<TMP_InputField>();
        foreach (TMP_InputField i in inputs)
        {
            if (i.name == "InputPassword")
            {
                pass = i.text;

            }
            else if (i.name == "InputNickname")
            {
                nickname = i.text;
            }
        }
   
        ServerManager.instance.LoginUser(nickname, pass, checkBoxKeep.isOn);
    }

    public void ToSignIn() {
        GetComponent<Animator>().SetTrigger("SwapToSignIn");
    }

    public void ToLogIn()
    {
        GetComponent<Animator>().SetTrigger("SwapToLogIn");
    }

    public void ResetInputs()
	{
        foreach (TMP_InputField inputs in panelSign.GetComponentsInChildren<TMP_InputField>())
		{
            inputs.text = "";
		}
    }
    public void ResetPasswordInputs()
	{
        foreach (TMP_InputField inputs in panelSign.GetComponentsInChildren<TMP_InputField>())
        {
            if (inputs.name != "InputNickname")
			{
                inputs.text = "";
            }
        }
    }


}
