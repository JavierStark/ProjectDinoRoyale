using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackButton : MonoBehaviour
{
    public void ToPlayStore() {

        Application.OpenURL("market://details?id=" + Application.identifier);
       
    }
}
