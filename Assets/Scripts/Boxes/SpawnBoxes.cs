using System.Collections;
using UnityEngine;
using TMPro;

public class SpawnBoxes : MonoBehaviour
{
    [Header("Puntos de spawn")]
    public Transform[] puntosSpawn;

    [Header("Prefab de la caja")]
    public GameObject cajaPrefab;

    [Header("Numero de cajas")]
    public int numCajas = 5;

    [Header("Rango de masa")]
    public float masaMin = 1f;
    public float masaMax = 2f;

    private bool[] ocupados;



    void Start()
    {
        ocupados = new bool[puntosSpawn.Length];

        SpawnCaja();
    }

    void SpawnCaja()
    {
        if (puntosSpawn.Length == 0 || cajaPrefab == null)
        {
            Debug.LogWarning("Faltan puntos de spawn o prefab");
            return;
        }

        for (int i = 0; i < numCajas; i++)
        {
            int intentos = 0;
            bool encontrado = false;
            int indice = -1;

            while (!encontrado && intentos < 20)
            {
                indice = Random.Range(0, puntosSpawn.Length);

                if (!ocupados[indice])
                {
                    encontrado = true;
                }

                intentos++;
            }

            if (!encontrado) continue;

            Transform punto = puntosSpawn[indice];

            GameObject caja = Instantiate(cajaPrefab, punto.position, punto.rotation);

            ocupados[indice] = true;

            Rigidbody rb = caja.GetComponent<Rigidbody>();

            if (rb != null)
            {
                float masa = Random.Range(masaMin, masaMax);
                rb.mass = masa;

                TextMeshPro texto = caja.GetComponentInChildren<TextMeshPro>();

                if (texto != null)
                {
                    texto.text = masa.ToString("F1") + " kg";
                }
                else
                {
                    Debug.LogWarning("No se encontró TextMeshPro en la caja");
                }
            }

            // avisar cuando se destruya para liberar el punto
            CajaSpawnTracker tracker = caja.AddComponent<CajaSpawnTracker>();
            tracker.Setup(this, indice);
        }
    }

    public void LiberarPunto(int index)
    {
        if (index >= 0 && index < ocupados.Length)
        {
            ocupados[index] = false;
        }
    }
}