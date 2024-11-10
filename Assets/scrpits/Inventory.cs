using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Lista para almacenar los objetos recogidos
    public List<string> items = new List<string>();

    // Método para añadir un ítem al inventario
    public void AddItem(string itemName)
    {
        if (!items.Contains(itemName)) // Evita duplicados
        {
            items.Add(itemName);
            Debug.Log(itemName + " añadido al inventario.");
        }
    }

    // Método para comprobar si un ítem está en el inventario
    public bool HasItem(string itemName)
    {
        return items.Contains(itemName);
    }
}