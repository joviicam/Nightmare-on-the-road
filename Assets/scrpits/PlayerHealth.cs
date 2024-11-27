using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Vida m�xima
    public float regenerationTime = 60f; // Tiempo en segundos para regeneraci�n completa
    public Image damageOverlay; // Imagen para el parpadeo de da�o
    public Slider healthBar; // Barra de vida

    private float currentHealth;
    private float regenerationRate; // Cantidad de vida recuperada por segundo
    private bool isRegenerating = false;

    void Start()
    {
        currentHealth = maxHealth;
        regenerationRate = maxHealth / regenerationTime;

        // Configuraci�n inicial de la barra de vida
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        damageOverlay.enabled = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            // Cambiar de escena o mostrar pantalla de muerte
            Debug.Log("Jugador muerto");
            SceneManager.LoadScene("GameOver");
            // Aqu� cambiar�a de escena
            currentHealth = 0; // Asegurar que no sea menor a 0
        }

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        // Mostrar el parpadeo de da�o
        StartCoroutine(DamageEffect());
    }

    void Update()
    {
        if (currentHealth < maxHealth && !isRegenerating)
        {
            isRegenerating = true; // Inicia el proceso de regeneraci�n
            StartCoroutine(RegenerateHealth());
        }
    }

    System.Collections.IEnumerator RegenerateHealth()
    {
        while (currentHealth < maxHealth)
        {
            currentHealth += regenerationRate * Time.deltaTime;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            UpdateHealthUI();
            yield return null; // Espera al siguiente frame
        }
        isRegenerating = false; // Termina el proceso de regeneraci�n
    }

    void UpdateHealthUI()
    {
        // Actualizar la barra de vida
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }
    }

    System.Collections.IEnumerator DamageEffect()
    {
        damageOverlay.enabled = true; // Mostrar el efecto rojo
        yield return new WaitForSeconds(0.3f); // Esperar un segundo
        damageOverlay.enabled = false; // Ocultar el efecto rojo
    }
}
