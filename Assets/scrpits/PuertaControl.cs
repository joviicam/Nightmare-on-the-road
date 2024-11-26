using UnityEngine;

public class PuertaControl : MonoBehaviour
{
    private bool abierta = false;
    public float velocidadApertura = 2f; // Velocidad para abrir/cerrar la puerta
    public Vector3 anguloApertura = new Vector3(0, -90, 0); // �ngulo relativo de apertura
    private Quaternion rotacionObjetivo; // Rotaci�n objetivo al abrir o cerrar
    private Quaternion rotacionInicial; // Rotaci�n inicial de la puerta

    private AudioSource audioSource; // Componente para reproducir sonidos
    private AudioClip sonidoApertura; // Sonido para abrir la puerta
    private AudioClip sonidoCierre;   // Sonido para cerrar la puerta

    void Start()
    {
        // Guarda la rotaci�n inicial de la puerta
        rotacionInicial = transform.localRotation;
        rotacionObjetivo = rotacionInicial; // Inicializa la rotaci�n objetivo como la inicial

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
            Debug.LogError("No se pudo cargar el audio. Aseg�rate de que los archivos est�n en la carpeta Resources y tengan los nombres correctos.");
        }
    }

    public void Abrir()
    {
        if (abierta)
        {
            // Si ya est� abierta, regresa a la rotaci�n inicial
            rotacionObjetivo = rotacionInicial;

            // Reproduce el sonido de cierre
            if (sonidoCierre != null)
            {
                audioSource.PlayOneShot(sonidoCierre);
            }
        }
        else
        {
            // Calcula la rotaci�n objetivo relativa a la inicial
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
        // Interpolaci�n suave hacia la rotaci�n objetivo
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, rotacionObjetivo, velocidadApertura * Time.deltaTime);
    }
}
