using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    // Método para reiniciar el juego (cargar la escena principal)
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void PlayAgain()
    {
        // Asegúrate de reemplazar "GameScene" con el nombre exacto de tu escena principal
        SceneManager.LoadScene("Demo_01");
    }
    public void Instrucciones()
    {
        // Asegúrate de reemplazar "GameScene" con el nombre exacto de tu escena principal
        SceneManager.LoadScene("Instrucciones");
    }
    // Método para ir al menú principal
    public void GoToMainMenu()
    {
        // Asegúrate de reemplazar "MainMenu" con el nombre exacto de tu escena de menú
        SceneManager.LoadScene("Menu");
    }

    // Método para salir del juego
    public void QuitGame()
    {
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }
}
