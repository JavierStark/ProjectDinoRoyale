using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{


    public void StartGame()
	{
        Debug.Log("Iniciando juego");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
	}

    public void QuitGame()
	{
        Application.Quit();
        Debug.Log("Cerrando juego");
	}
}
