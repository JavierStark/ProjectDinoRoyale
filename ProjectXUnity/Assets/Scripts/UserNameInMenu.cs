using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserNameInMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		string defaultNickname = "Offline";
		try
		{
			TMP_Text txtUserNickname = GetComponent<TMPro.TMP_Text>();
			txtUserNickname.text = defaultNickname;
			if (!string.IsNullOrEmpty( ServerManager.instance.user.nickname))
			{
				txtUserNickname.text = ServerManager.instance.user.nickname;
			}
		}
		catch (System.Exception e)
		{
			Debug.Log(e);
		}
       
    }
}
