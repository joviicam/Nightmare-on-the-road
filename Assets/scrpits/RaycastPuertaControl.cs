using UnityEngine;

public class RaycastPuertaControl : MonoBehaviour
{
    public float distanciaRaycast = 5f;
    public LayerMask layerPuerta; // Define el layer para la puerta

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detecta clic izquierdo del rat�n
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, distanciaRaycast, layerPuerta))
            {
                if (hit.collider != null && hit.collider.gameObject.CompareTag("Puerta"))
                {
                    // Verifica si la puerta tiene el script "PuertaConLlave"
                    PuertaConLlave puertaConLlave = hit.collider.GetComponent<PuertaConLlave>();
                    PuertaControl puertaNormal = hit.collider.GetComponent<PuertaControl>();

                    if (puertaConLlave != null)
                    {
                        // Interactuar con la puerta con llave
                        Debug.Log("Interacci�n con puerta con llave.");
                        puertaConLlave.Abrir();
                    }
                    else if (puertaNormal != null)
                    {
                        // Interactuar con la puerta normal
                        Debug.Log("Interacci�n con puerta normal.");
                        puertaNormal.Abrir();
                    }
                    else
                    {
                        // Mensaje si la puerta no tiene ninguno de los scripts
                        Debug.LogError("El objeto etiquetado como 'Puerta' no tiene un script de control asignado.");
                    }
                }
                else
                {
                    Debug.Log("El objeto golpeado no es una puerta v�lida.");
                }
            }
        }
    }
}
