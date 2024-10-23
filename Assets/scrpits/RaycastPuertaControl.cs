using UnityEngine;

public class RaycastPuertaControl : MonoBehaviour
{
    public float distanciaRaycast = 5f;
    public LayerMask layerPuerta; // Define el layer para la puerta
    public PuertaControl puertaControl;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detecta clic izquierdo del ratón
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, distanciaRaycast, layerPuerta))
            {
                if (hit.collider != null && hit.collider.gameObject.CompareTag("Puerta"))
                {
                    puertaControl.Abrir(); // Cambia el estado de la puerta
                }
            }
        }
    }
}
