using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SignLoginManager : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject panelLogin, panelSign;
    TMP_Text txtInfo;
    void Start()
    {
       panelLogin = GameObject.Find("PanelLogin");
       panelSign = GameObject.Find("PanelSign");
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckConfirmPassword()
	{
        string pass = null;
        string confirmPass = null;
        TMP_InputField[] inputs = panelSign.GetComponentsInChildren<TMP_InputField>();
        foreach(TMP_InputField i in inputs)
		{
            if (i.name == "InputPassword")
			{
                pass = i.text;

			}else if (i.name == "InputConfirmPassword")
            {
                confirmPass = i.text;
			}
		}
        if (pass == confirmPass)
		{
            Debug.Log("son  iguales");
		}
		else
		{
            Debug.Log("no son iguales");
		}
	}
}
