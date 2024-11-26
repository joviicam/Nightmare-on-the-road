using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class CarroConHerramientaYGasolina : MonoBehaviour
{
    private bool puedeConducir = false; // Controla si el jugador puede usar el carro

    public string herramientaRequerida = "Herramienta"; // Nombre de la herramienta requerida
    public string gasolinaRequerida = "Gasolina"; // Nombre de la gasolina requerida
    private Inventory inventarioJugador; // Referencia al inventario del jugador

    public string nombreEscenaFinal = "Fin"; // Nombre de la escena a la que se cambiará

    void Start()
    {
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
    }

    void OnMouseDown() // Este método se llama cuando el jugador hace clic en el carro
    {
        if (PuedeConducir())
        {
            Debug.Log("El jugador puede conducir el carro.");
            CambiarDeEscena();
        }
        else
        {
            Debug.Log("El jugador no tiene los ítems necesarios para conducir el carro.");
        }
    }

    private bool PuedeConducir()
    {
        if (inventarioJugador == null)
        {
            Debug.LogError("El inventario del jugador no está asignado.");
            return false;
        }

        // Verifica si el jugador tiene ambos ítems
        bool tieneHerramienta = inventarioJugador.HasItem(herramientaRequerida);
        bool tieneGasolina = inventarioJugador.HasItem(gasolinaRequerida);

        if (tieneHerramienta && tieneGasolina)
        {
            Debug.Log("El jugador tiene la herramienta y la gasolina necesarias.");
            return true;
        }

        if (!tieneHerramienta)
        {
            Debug.Log("El jugador no tiene la herramienta necesaria.");
        }

        if (!tieneGasolina)
        {
            Debug.Log("El jugador no tiene gasolina.");
        }

        return false;
    }

    private void CambiarDeEscena()
    {
        // Cambiar a la escena final
        SceneManager.LoadScene(nombreEscenaFinal);
    }
}
