using UnityEngine;

public class FootstepController : MonoBehaviour
{
    public AudioClip pisadasExteriores; // Sonido de pisadas al aire libre
    public AudioClip pisadasInteriores; // Sonido de pisadas dentro de la casa
    private AudioSource audioSource; // Componente para reproducir los sonidos

    private bool dentroDeCasa = false; // Estado de la ubicación del jugador

    void Start()
    {
        // Configurar el AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.loop = true; // Hacer que las pisadas se repitan
        CambiarSonido(); // Configurar el sonido inicial
    }

    void Update()
    {
        // Simula las pisadas cuando el jugador se mueve (puedes usar tu propio control de movimiento)
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
            }
        }
    }

    void CambiarSonido()
    {
        // Cambia el clip del AudioSource dependiendo de la ubicación
        audioSource.clip = dentroDeCasa ? pisadasInteriores : pisadasExteriores;
        audioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interior"))
        {
            dentroDeCasa = true;
            CambiarSonido();
        }
        else if (other.CompareTag("Exterior"))
        {
            dentroDeCasa = false;
            CambiarSonido();
        }
    }
}
