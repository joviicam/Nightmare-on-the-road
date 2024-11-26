using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    // M�todo para reiniciar el juego (cargar la escena principal)
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void PlayAgain()
    {
        // Aseg�rate de reemplazar "GameScene" con el nombre exacto de tu escena principal
        SceneManager.LoadScene("Demo_01");
    }
    public void Instrucciones()
    {
        // Aseg�rate de reemplazar "GameScene" con el nombre exacto de tu escena principal
        SceneManager.LoadScene("Instrucciones");
    }
    // M�todo para ir al men� principal
    public void GoToMainMenu()
    {
        // Aseg�rate de reemplazar "MainMenu" con el nombre exacto de tu escena de men�
        SceneManager.LoadScene("Menu");
    }

    // M�todo para salir del juego
    public void QuitGame()
    {
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }
}
