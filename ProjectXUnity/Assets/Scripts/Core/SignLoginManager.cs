using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

public class SignLoginManager : MonoBehaviour
{
    GameObject panelLogin, panelSign;
    public TMP_Text txtInfoSign, txtInfoLogin;
    Toggle checkBoxKeep;

    bool error = false;

    string emptyNicknameMessage = "Nickname may not be empty";
    string nicknameTooLongMessage = "Nickname may not be longer than 14 characters ";
    string passwordTooLongMessage = "Password may not be longer than 20 characters";
    string emptyPasswordMessage = "Password may not be empty";
    string failConfirmMessage = "password and confirmation must match";   
    string correctFormMessage = "¡Everything Ok!";

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
			formu.AddField("password", toMd5(pass));
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
   
        ServerManager.instance.LoginUser(nickname, toMd5(pass), checkBoxKeep.isOn);
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
    string toMd5(string t)
	{
        MD5 md5Hash = MD5.Create();
        return GetMd5Hash(md5Hash, t);
    }

    static string GetMd5Hash(MD5 md5Hash, string input)
    {

        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
        StringBuilder sBuilder = new StringBuilder();

        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        return sBuilder.ToString();
    }
}
