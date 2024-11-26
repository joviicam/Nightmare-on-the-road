using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    public string gameOverSceneName = "GameOver"; // Nombre de la escena de GameOver

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra en la zona es el jugador
        if (other.CompareTag("Player"))
        {
            // Cambia a la escena de GameOver
            SceneManager.LoadScene(gameOverSceneName);
        }
    }
}
