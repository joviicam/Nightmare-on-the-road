using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Referencia al menú de pausa
    private bool isPaused = false; // Estado de pausa
    public FirstPersonController player;

    void Start()
    {
        // Asegurarse de que el menú de pausa esté oculto al iniciar el juego
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // El tiempo fluye normalmente al inicio
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;
        // Detectar cuando se presiona la tecla Escape
        if (Input.GetKeyDown(KeyCode.Escape)) // Comienza el bloque condicional
        {
            Debug.Log("Escape presionado");

            if (isPaused)
            {
                ResumeGame(); // Si está en pausa, reanuda el juego

            }
            else
            {
                PauseGame(); // Si no está en pausa, pausa el juego

            }
        } // Aquí termina el bloque condicional correctamente
    }


    public void PauseGame()
    {
        player.enabled = false;
        isPaused = true;
        Time.timeScale = 0f; // Detener el tiempo del juego
        pauseMenuUI.SetActive(true); // Mostrar el menú de pausa

        // Mostrar el cursor y desbloquearlo
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }


    public void ResumeGame()
    {
        player.enabled = true;
        isPaused = false;
        Time.timeScale = 1f; // Reanudar el tiempo del juego
        pauseMenuUI.SetActive(false); // Ocultar el menú de pausa

        // Ocultar el cursor y bloquearlo
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    public void RestartGame()
    {
        Time.timeScale = 1f; // Reanudar el tiempo antes de reiniciar
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Recargar la escena actual
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Reanudar el tiempo antes de cambiar de escena
        SceneManager.LoadScene("Menu"); // Cambiar a la escena del menú principal
    }
}
