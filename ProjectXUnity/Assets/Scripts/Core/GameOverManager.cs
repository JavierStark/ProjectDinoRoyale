using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{

    private void OnEnable() {
        Time.timeScale = 0;
        print("stop");
    }
}
