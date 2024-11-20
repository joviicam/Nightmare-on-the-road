using UnityEngine;

public class Key : MonoBehaviour
{
    private bool isPlayerInRange = false;

    // Agregar una variable para el ID del objeto
    public string itemId;

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el jugador ha entrado en el rango de la llave
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Verifica si el jugador ha salido del rango de la llave
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        // Solo permite recoger el ítem si el jugador está en el rango y presiona "E"
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Encuentra el script de inventario en el jugador
            Inventory inventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();

            if (inventory != null)
            {
                // Usa el itemId que se ha asignado en el inspector para agregar el ítem al inventario
                inventory.AddItem(itemId);
                Destroy(gameObject); // Elimina el objeto de la escena
            }
        }
    }
}
