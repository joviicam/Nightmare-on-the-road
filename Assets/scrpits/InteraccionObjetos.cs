using UnityEngine;

public class InteraccionObjetos : MonoBehaviour
{
    public float distanciaRaycast = 5f;
    public LayerMask layerObjeto; // Define el layer para los objetos
    public Inventory inventarioJugador; // Referencia al inventario del jugador

    void Start()
    {
        // Aseg�rate de que el inventario est� asignado
        if (inventarioJugador == null)
        {
            Debug.LogError("El inventario del jugador no est� asignado. Aseg�rate de que el objeto del jugador tenga el componente 'Inventory'.");
        }
    }

    void Update()
    {
        // Detecta la interacci�n con el objeto cuando se presiona el bot�n izquierdo del rat�n
        if (Input.GetMouseButtonDown(0)) // Detecta clic izquierdo
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, distanciaRaycast, layerObjeto))
            {
                if (hit.collider != null)
                {
                    // Si el objeto tiene el tag "Llave", a�ade al inventario
                    if (hit.collider.gameObject.CompareTag("Llave"))
                    {
                        inventarioJugador.AddItem("Llave");
                        Destroy(hit.collider.gameObject); // Destruye el objeto llave al ser recogido
                    }
                    // Si el objeto tiene el tag "Gasolina", a�ade al inventario
                    else if (hit.collider.gameObject.CompareTag("Gasolina"))
                    {
                        inventarioJugador.AddItem("Gasolina");
                        Destroy(hit.collider.gameObject); // Destruye el objeto gasolina
                    }
                    // Si el objeto tiene el tag "Herramienta", a�ade al inventario
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
