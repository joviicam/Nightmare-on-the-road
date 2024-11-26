using UnityEngine;

public class PuertaConLlave : MonoBehaviour
{
    private bool abierta = false; // Estado de la puerta (abierta o cerrada)
    public float velocidadApertura = 2f; // Velocidad de apertura
    public Vector3 anguloApertura = new Vector3(0, -90, 0); // �ngulo de apertura
    private Quaternion rotacionObjetivo; // Rotaci�n hacia la que se interpola
    private Quaternion rotacionInicial; // Rotaci�n inicial de la puerta

    public string llaveRequerida = "Llave"; // Nombre de la llave necesaria
    private Inventory inventarioJugador; // Referencia al inventario del jugador
    public SeguirJugador ni�a;

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
            Debug.LogError("No se encontr� un objeto con el tag Player.");
        }

        // Inicializa las rotaciones
        rotacionInicial = transform.localRotation; // Rotaci�n inicial de la puerta
        rotacionObjetivo = rotacionInicial; // Inicialmente, el objetivo es la posici�n inicial
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
            rotacionObjetivo = rotacionInicial; // Si est� abierta, regresa a la rotaci�n inicial
            Debug.Log("Cerrando puerta. Rotaci�n objetivo: " + rotacionObjetivo.eulerAngles);
        }
        else
        {
            rotacionObjetivo = rotacionInicial * Quaternion.Euler(anguloApertura); // Gira hacia el �ngulo de apertura
            Debug.Log("Abriendo puerta. Rotaci�n objetivo: " + rotacionObjetivo.eulerAngles);
        }

        // Alterna el estado de la puerta solo si la rotaci�n ha cambiado
        abierta = !abierta;

        if (ni�a != null)
        {
            ni�a.ComenzarASeguir();
        }
        else
        {
            Debug.LogError("No se ha asignado el script de la ni�a en la puerta.");
        }
    }

    private bool TieneLlave()
    {
        if (inventarioJugador == null)
        {
            Debug.LogError("El inventario del jugador no est� asignado.");
            return false;
        }

        Debug.Log("�tems en el inventario: " + string.Join(", ", inventarioJugador.GetItems()));

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
        // Solo realiza la interpolaci�n si la puerta no est� completamente abierta o cerrada
        if (transform.localRotation != rotacionObjetivo)
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, rotacionObjetivo, velocidadApertura * Time.deltaTime);

            // Depuraci�n adicional para verificar el movimiento
            Debug.Log("Rotaci�n actual: " + transform.localRotation.eulerAngles);
            Debug.Log("Rotaci�n objetivo: " + rotacionObjetivo.eulerAngles);
        }
    }
}
