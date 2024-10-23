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
        rotacionInicial = transform.rotation; // Guarda la rotación inicial
        rotacionObjetivo = rotacionInicial; // Inicializa la rotación objetivo
    }

    public void Abrir()
    {
        if (abierta)
        {
            // Si la puerta ya está abierta, regresa a la posición inicial
            rotacionObjetivo = rotacionInicial;
        }
        else
        {
            // Si la puerta está cerrada, establece la rotación objetivo para abrir
            rotacionObjetivo = Quaternion.Euler(anguloApertura);
        }
        abierta = !abierta; // Cambia el estado de la puerta
    }

    void Update()
    {
        // Interpolación de rotación hacia la rotación objetivo
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotacionObjetivo, velocidadApertura * Time.deltaTime);
    }
}
