using UnityEngine;

public class PuertaControl : MonoBehaviour
{
    private bool abierta = false;
    public float velocidadApertura = 2f;
    public Vector3 anguloApertura = new Vector3(0, -270, 0);
    private Quaternion rotacionObjetivo;
    private Quaternion rotacionInicial;

    void Start()
    {
        rotacionInicial = transform.rotation; // Guarda la rotaci�n inicial
        rotacionObjetivo = rotacionInicial; // Inicializa la rotaci�n objetivo
    }

    public void Abrir()
    {
        if (abierta)
        {
            // Si la puerta ya est� abierta, regresa a la posici�n inicial
            rotacionObjetivo = rotacionInicial;
        }
        else
        {
            // Si la puerta est� cerrada, establece la rotaci�n objetivo para abrir
            rotacionObjetivo = Quaternion.Euler(anguloApertura);
        }
        abierta = !abierta; // Cambia el estado de la puerta
    }

    void Update()
    {
        // Interpolaci�n de rotaci�n hacia la rotaci�n objetivo
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotacionObjetivo, velocidadApertura * Time.deltaTime);
    }
}
