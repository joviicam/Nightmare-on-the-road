using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InventoryItem
{
    public string itemName;   // Nombre del ítem
    public Sprite itemImage;  // Imagen asociada al ítem
    public AudioSource audioSource; // Ahora es AudioSource en lugar de AudioClip
}

public class Inventory : MonoBehaviour
{
    // Lista para almacenar los objetos recogidos
    private List<string> items = new List<string>();

    // Referencia a los slots del inventario en el Canvas
    public List<Image> inventorySlots;

    // Lista serializable para configurar ítems en el Inspector
    public List<InventoryItem> inventoryItemsList = new List<InventoryItem>();

    // Diccionario interno para buscar ítems por nombre
    private Dictionary<string, InventoryItem> itemData = new Dictionary<string, InventoryItem>();

    void Start()
    {
        // Llena el diccionario a partir de la lista configurada en el Inspector
        foreach (var item in inventoryItemsList)
        {
            if (!itemData.ContainsKey(item.itemName))
            {
                itemData.Add(item.itemName, item);
            }
        }

        // Asegura que los slots estén vacíos al inicio
        foreach (var slot in inventorySlots)
        {
            slot.sprite = null;
            slot.color = new Color(1, 1, 1, 0); // Hace transparente el recuadro
        }
    }

    // Método para añadir un ítem al inventario
    public void AddItem(string itemName)
    {
        // Verifica si el ítem ya está en el inventario
        if (!items.Contains(itemName) && itemData.ContainsKey(itemName))
        {
            items.Add(itemName);
            Debug.Log(itemName + " añadido al inventario.");

            // Actualiza el inventario visual
            UpdateInventoryUI(itemData[itemName]);

            // Reproduce el sonido correspondiente utilizando AudioSource
            PlayItemSound(itemData[itemName]);
        }
        else
        {
            Debug.Log(itemName + " ya está en el inventario o no existe.");
        }
    }

    // Método para reproducir el sonido correspondiente usando el AudioSource del ítem
    void PlayItemSound(InventoryItem item)
    {
        if (item.audioSource != null && !item.audioSource.isPlaying)
        {
            item.audioSource.Play(); // Reproduce el sonido del AudioSource de ese ítem
        }
        else
        {
            Debug.LogWarning("No se ha asignado un AudioSource o ya está sonando.");
        }
    }

    // Método para actualizar el inventario visual
    void UpdateInventoryUI(InventoryItem newItem)
    {
        foreach (var slot in inventorySlots)
        {
            if (slot.sprite == null)
            {
                slot.sprite = newItem.itemImage; // Asigna la imagen del ítem
                slot.color = Color.white; // Hace visible el recuadro
                break;
            }
        }
    }

    // Método para comprobar si un ítem está en el inventario
    public bool HasItem(string itemName)
    {
        return items.Contains(itemName);
    }

    // Método para obtener la lista de ítems (si necesitas mostrarla o debuggear)
    public List<string> GetItems()
    {
        return items;
    }
}
