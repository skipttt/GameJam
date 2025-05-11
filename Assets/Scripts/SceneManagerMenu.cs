using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerMenu : MonoBehaviour
{

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Salir()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego");
    }


}
