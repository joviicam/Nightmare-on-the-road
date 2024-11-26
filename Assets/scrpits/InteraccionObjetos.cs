using UnityEngine;

public class InteraccionObjetos : MonoBehaviour
{
    public float distanciaRaycast = 5f;
    public LayerMask layerObjeto; // Define el layer para los objetos
    public Inventory inventarioJugador; // Referencia al inventario del jugador

    void Start()
    {
        // Asegúrate de que el inventario esté asignado
        if (inventarioJugador == null)
        {
            Debug.LogError("El inventario del jugador no está asignado. Asegúrate de que el objeto del jugador tenga el componente 'Inventory'.");
        }
    }

    void Update()
    {
        // Detecta la interacción con el objeto cuando se presiona el botón izquierdo del ratón
        if (Input.GetMouseButtonDown(0)) // Detecta clic izquierdo
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, distanciaRaycast, layerObjeto))
            {
                if (hit.collider != null)
                {
                    // Si el objeto tiene el tag "Llave", añade al inventario
                    if (hit.collider.gameObject.CompareTag("Llave"))
                    {
                        inventarioJugador.AddItem("Llave");
                        Destroy(hit.collider.gameObject); // Destruye el objeto llave al ser recogido
                    }
                    // Si el objeto tiene el tag "Gasolina", añade al inventario
                    else if (hit.collider.gameObject.CompareTag("Gasolina"))
                    {
                        inventarioJugador.AddItem("Gasolina");
                        Destroy(hit.collider.gameObject); // Destruye el objeto gasolina
                    }
                    // Si el objeto tiene el tag "Herramienta", añade al inventario
                    else if (hit.collider.gameObject.CompareTag("Herramienta"))
                    {
                        inventarioJugador.AddItem("Herramienta");
                        Destroy(hit.collider.gameObject); // Destruye el objeto herramienta
                    }
                    // Otras interacciones con objetos
                }
            }
        }
    }
}
