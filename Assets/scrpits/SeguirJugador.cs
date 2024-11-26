using UnityEngine;
using UnityEngine.AI;

public class SeguirJugador : MonoBehaviour
{
    private NavMeshAgent agente; // Referencia al NavMeshAgent
    private Transform jugador;   // Referencia al jugador
    private bool siguiendo = false; // Estado de si está siguiendo o no

    public float distanciaSeguir = 5f; // Distancia a la que la niña empieza a seguir al jugador
    public float velocidad = 3.5f; // Velocidad de movimiento de la niña

    void Start()
    {
        // Obtiene el NavMeshAgent del objeto (la niña)
        agente = GetComponent<NavMeshAgent>();

        // Asegúrate de que tenga un agente de navegación
        if (agente == null)
        {
            Debug.LogError("No se encuentra un NavMeshAgent en la niña.");
            return;
        }

        // Configura la velocidad del agente
        agente.speed = velocidad;

        // Encuentra al jugador
        jugador = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (jugador == null)
        {
            Debug.LogError("No se encontró al jugador con el tag Player.");
        }
    }

    void Update()
    {
        // Si la puerta está abierta, la niña empezará a seguir al jugador
        if (siguiendo)
        {
            Seguir();
        }
    }

    public void ComenzarASeguir()
    {
        // Inicia el seguimiento de la niña
        siguiendo = true;
        Debug.Log("La niña ha comenzado a seguir al jugador.");
    }

    private void Seguir()
    {
        if (jugador != null)
        {
            // Verifica si la distancia entre la niña y el jugador es menor que la distancia definida
            float distancia = Vector3.Distance(transform.position, jugador.position);

            if (distancia <= distanciaSeguir)
            {
                // Haz que el NavMeshAgent se mueva hacia el jugador
                agente.SetDestination(jugador.position);
            }
            else
            {
                // Si está muy lejos, detén el movimiento
                agente.ResetPath();
            }
        }
    }
}
