using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserNameInMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		try
		{
			GetComponent<TMPro.TMP_Text>().text = ServerManager.instance.user.nickname;
		}
		catch (System.Exception e)
		{
			Debug.Log(e);
		}
       
    }
}
