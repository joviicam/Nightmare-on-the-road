using UnityEngine;

public class PuertaControl : MonoBehaviour
{
    private bool abierta = false;
    public float velocidadApertura = 2f; // Velocidad para abrir/cerrar la puerta
    public Vector3 anguloApertura = new Vector3(0, -90, 0); // Ángulo relativo de apertura
    private Quaternion rotacionObjetivo; // Rotación objetivo al abrir o cerrar
    private Quaternion rotacionInicial; // Rotación inicial de la puerta

    private AudioSource audioSource; // Componente para reproducir sonidos
    private AudioClip sonidoApertura; // Sonido para abrir la puerta
    private AudioClip sonidoCierre;   // Sonido para cerrar la puerta

    void Start()
    {
        // Guarda la rotación inicial de la puerta
        rotacionInicial = transform.localRotation;
        rotacionObjetivo = rotacionInicial; // Inicializa la rotación objetivo como la inicial

        // Configura el AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Cargar sonidos desde la carpeta Resources
        sonidoApertura = Resources.Load<AudioClip>("open-door");
        sonidoCierre = Resources.Load<AudioClip>("door-close"); // Puedes usar un sonido diferente si es necesario.

        // Verifica que los sonidos se cargaron correctamente
        if (sonidoApertura == null || sonidoCierre == null)
        {
            Debug.LogError("No se pudo cargar el audio. Asegúrate de que los archivos estén en la carpeta Resources y tengan los nombres correctos.");
        }
    }

    public void Abrir()
    {
        if (abierta)
        {
            // Si ya está abierta, regresa a la rotación inicial
            rotacionObjetivo = rotacionInicial;

            // Reproduce el sonido de cierre
            if (sonidoCierre != null)
            {
                audioSource.PlayOneShot(sonidoCierre);
            }
        }
        else
        {
            // Calcula la rotación objetivo relativa a la inicial
            rotacionObjetivo = rotacionInicial * Quaternion.Euler(anguloApertura);

            // Reproduce el sonido de apertura
            if (sonidoApertura != null)
            {
                audioSource.PlayOneShot(sonidoApertura);
            }
        }
        abierta = !abierta; // Cambia el estado de la puerta
    }

    void Update()
    {
        // Interpolación suave hacia la rotación objetivo
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, rotacionObjetivo, velocidadApertura * Time.deltaTime);
    }
}
