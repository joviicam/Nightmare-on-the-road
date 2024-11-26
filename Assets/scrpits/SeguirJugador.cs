using UnityEngine;
using UnityEngine.AI;

public class SeguirJugador : MonoBehaviour
{
    private NavMeshAgent agente; // Referencia al NavMeshAgent
    private Transform jugador;   // Referencia al jugador
    private bool siguiendo = false; // Estado de si est� siguiendo o no

    public float distanciaSeguir = 5f; // Distancia a la que la ni�a empieza a seguir al jugador
    public float velocidad = 3.5f; // Velocidad de movimiento de la ni�a

    void Start()
    {
        // Obtiene el NavMeshAgent del objeto (la ni�a)
        agente = GetComponent<NavMeshAgent>();

        // Aseg�rate de que tenga un agente de navegaci�n
        if (agente == null)
        {
            Debug.LogError("No se encuentra un NavMeshAgent en la ni�a.");
            return;
        }

        // Configura la velocidad del agente
        agente.speed = velocidad;

        // Encuentra al jugador
        jugador = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (jugador == null)
        {
            Debug.LogError("No se encontr� al jugador con el tag Player.");
        }
    }

    void Update()
    {
        // Si la puerta est� abierta, la ni�a empezar� a seguir al jugador
        if (siguiendo)
        {
            Seguir();
        }
    }

    public void ComenzarASeguir()
    {
        // Inicia el seguimiento de la ni�a
        siguiendo = true;
        Debug.Log("La ni�a ha comenzado a seguir al jugador.");
    }

    private void Seguir()
    {
        if (jugador != null)
        {
            // Verifica si la distancia entre la ni�a y el jugador es menor que la distancia definida
            float distancia = Vector3.Distance(transform.position, jugador.position);

            if (distancia <= distanciaSeguir)
            {
                // Haz que el NavMeshAgent se mueva hacia el jugador
                agente.SetDestination(jugador.position);
            }
            else
            {
                // Si est� muy lejos, det�n el movimiento
                agente.ResetPath();
            }
        }
    }
}
