using UnityEngine;

public class ObjetoRecogible : MonoBehaviour
{
    public string nombreObjeto; // Nombre del objeto (e.g., "Llave", "Gasolina", "Herramienta")

    void Start()
    {
        if (string.IsNullOrEmpty(nombreObjeto))
        {
            Debug.LogWarning("El objeto " + gameObject.name + " no tiene un nombre asignado.");
        }
    }
}
