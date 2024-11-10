using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] spawnPoints; // Array de los 5 objetos vacíos
    public GameObject assetPrefab; // Prefab del asset a spawnear

    void Start()
    {
        SpawnAsset();
    }

    void SpawnAsset()
    {
        // Elige un índice aleatorio para el spawn point
        int randomIndex = Random.Range(0, spawnPoints.Length);

        // Instancia el asset en la posición y rotación del spawn point seleccionado
        Instantiate(assetPrefab, spawnPoints[randomIndex].transform.position, spawnPoints[randomIndex].transform.rotation);

        // Imprime en la consola el índice y nombre del spawn point
        Debug.Log($"Asset generado en el spawn point: {randomIndex} - {spawnPoints[randomIndex].name}");
    }
}
