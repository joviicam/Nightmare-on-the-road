using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] spawnPoints; // Array de los 5 objetos vac�os
    public GameObject assetPrefab; // Prefab del asset a spawnear

    void Start()
    {
        SpawnAsset();
    }

    void SpawnAsset()
    {
        // Elige un �ndice aleatorio para el spawn point
        int randomIndex = Random.Range(0, spawnPoints.Length);

        // Instancia el asset en la posici�n y rotaci�n del spawn point seleccionado
        Instantiate(assetPrefab, spawnPoints[randomIndex].transform.position, spawnPoints[randomIndex].transform.rotation);

        // Imprime en la consola el �ndice y nombre del spawn point
        Debug.Log($"Asset generado en el spawn point: {randomIndex} - {spawnPoints[randomIndex].name}");
    }
}
