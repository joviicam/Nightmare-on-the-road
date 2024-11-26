using UnityEngine;

public class PuertaConLlave : MonoBehaviour
{
    private bool abierta = false; // Estado de la puerta (abierta o cerrada)
    public float velocidadApertura = 2f; // Velocidad de apertura
    public Vector3 anguloApertura = new Vector3(0, -90, 0); // Ángulo de apertura
    private Quaternion rotacionObjetivo; // Rotación hacia la que se interpola
    private Quaternion rotacionInicial; // Rotación inicial de la puerta

    public string llaveRequerida = "Llave"; // Nombre de la llave necesaria
    private Inventory inventarioJugador; // Referencia al inventario del jugador
    public SeguirJugador niña;

    void Start()
    {
        // Busca al jugador por su tag y asigna el inventario
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        if (jugador != null)
        {
            Debug.Log("Jugador encontrado: " + jugador.name);
            inventarioJugador = jugador.GetComponent<Inventory>();
            if (inventarioJugador != null)
            {
                Debug.Log("Inventario asignado correctamente.");
            }
            else
            {
                Debug.LogError("El jugador " + jugador.name + " no tiene el script Inventory.");
            }
        }
        else
        {
            Debug.LogError("No se encontró un objeto con el tag Player.");
        }

        // Inicializa las rotaciones
        rotacionInicial = transform.localRotation; // Rotación inicial de la puerta
        rotacionObjetivo = rotacionInicial; // Inicialmente, el objetivo es la posición inicial
    }

    public void Abrir()
    {
        // Verifica si el jugador tiene la llave antes de abrir
        if (!abierta && !TieneLlave())
        {
            Debug.Log("Necesitas la " + llaveRequerida + " para abrir esta puerta.");
            return;
        }

        // Cambia entre abrir y cerrar la puerta solo si el estado ha cambiado
        if (abierta)
        {
            rotacionObjetivo = rotacionInicial; // Si está abierta, regresa a la rotación inicial
            Debug.Log("Cerrando puerta. Rotación objetivo: " + rotacionObjetivo.eulerAngles);
        }
        else
        {
            rotacionObjetivo = rotacionInicial * Quaternion.Euler(anguloApertura); // Gira hacia el ángulo de apertura
            Debug.Log("Abriendo puerta. Rotación objetivo: " + rotacionObjetivo.eulerAngles);
        }

        // Alterna el estado de la puerta solo si la rotación ha cambiado
        abierta = !abierta;

        if (niña != null)
        {
            niña.ComenzarASeguir();
        }
        else
        {
            Debug.LogError("No se ha asignado el script de la niña en la puerta.");
        }
    }

    private bool TieneLlave()
    {
        if (inventarioJugador == null)
        {
            Debug.LogError("El inventario del jugador no está asignado.");
            return false;
        }

        Debug.Log("Ítems en el inventario: " + string.Join(", ", inventarioJugador.GetItems()));

        bool tieneLlave = inventarioJugador.HasItem(llaveRequerida);

        if (tieneLlave)
        {
            Debug.Log("El jugador tiene la llave requerida: " + llaveRequerida);
        }
        else
        {
            Debug.Log("El jugador no tiene la llave requerida: " + llaveRequerida);
        }

        return tieneLlave;
    }

    void Update()
    {
        // Solo realiza la interpolación si la puerta no está completamente abierta o cerrada
        if (transform.localRotation != rotacionObjetivo)
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, rotacionObjetivo, velocidadApertura * Time.deltaTime);

            // Depuración adicional para verificar el movimiento
            Debug.Log("Rotación actual: " + transform.localRotation.eulerAngles);
            Debug.Log("Rotación objetivo: " + rotacionObjetivo.eulerAngles);
        }
    }
}
