using UnityEngine;

public class SeguirJugador : MonoBehaviour
{
    private Transform jugador; // Referencia al jugador
    private bool siguiendo = false; // Estado de si est� siguiendo o no

    public float distanciaSeguir = 5f; // Distancia a la que la ni�a empieza a seguir al jugador
    public float velocidad = 3.5f; // Velocidad de movimiento de la ni�a
    public LayerMask obstaculosLayer; // Capa para detectar paredes u obst�culos
    public float distanciaRaycast = 1f; // Distancia para verificar los obst�culos

    void Start()
    {
        // Encuentra al jugador
        jugador = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (jugador == null)
        {
            Debug.LogError("No se encontr� al jugador con el tag Player.");
        }
    }

    void Update()
    {
        // Si se activa el seguimiento, la ni�a empieza a moverse hacia el jugador
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
                Vector3 direccion = (jugador.position - transform.position).normalized;

                // Usa un raycast para evitar que atraviese obst�culos
                if (!Physics.Raycast(transform.position, direccion, distanciaRaycast, obstaculosLayer))
                {
                    // Mueve la ni�a hacia el jugador
                    transform.position = Vector3.MoveTowards(transform.position, jugador.position, velocidad * Time.deltaTime);
                    // Gira hacia el jugador
                    transform.LookAt(new Vector3(jugador.position.x, transform.position.y, jugador.position.z));
                }
            }
        }
    }
}
