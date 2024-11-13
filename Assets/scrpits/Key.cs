using UnityEngine;

public class Key : MonoBehaviour
{
    private bool isPlayerInRange = false;

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
        // Solo permite recoger la llave si el jugador está en el rango y presiona "E"
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Encuentra el script de inventario en el jugador
            Inventory inventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();

            if (inventory != null)
            {
                inventory.AddItem("Llave");
                Destroy(gameObject); // Elimina la llave de la escena
            }
        }
    }
}