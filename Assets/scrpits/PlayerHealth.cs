using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Vida máxima
    public float regenerationTime = 60f; // Tiempo en segundos para regeneración completa
    public Image damageOverlay; // Imagen para el parpadeo de daño
    public Slider healthBar; // Barra de vida

    private float currentHealth;
    private float regenerationRate; // Cantidad de vida recuperada por segundo
    private bool isRegenerating = false;

    void Start()
    {
        currentHealth = maxHealth;
        regenerationRate = maxHealth / regenerationTime;

        // Configuración inicial de la barra de vida
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
            // Aquí cambiaría de escena
            currentHealth = 0; // Asegurar que no sea menor a 0
        }

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        // Mostrar el parpadeo de daño
        StartCoroutine(DamageEffect());
    }

    void Update()
    {
        if (currentHealth < maxHealth && !isRegenerating)
        {
            isRegenerating = true; // Inicia el proceso de regeneración
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
        isRegenerating = false; // Termina el proceso de regeneración
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
