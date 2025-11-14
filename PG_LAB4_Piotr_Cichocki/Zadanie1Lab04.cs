using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomCubesGenerator : MonoBehaviour
{
    [Header("Spawn settings")]
    [Tooltip("Ilość obiektów do wygenerowania")]
    public int numberOfCubes = 10;

    [Tooltip("Czas (s) pomiędzy kolejnymi generacjami")]
    public float delay = 3.0f;

    [Tooltip("Odległość nad platformą, na której pojawią się obiekty")]
    public float spawnHeightOffset = 0.5f;

    [Header("References")]
    [Tooltip("Prefab obiektu do generowania")]
    public GameObject block;

    [Tooltip("Lista materiałów do losowego przydziału (najlepiej 5)")]
    public Material[] materials;

    private List<Vector3> positions = new List<Vector3>();
    private Bounds platformBounds;
    private int objectCounter = 0;

    private void Awake()
    {
        Renderer planeRenderer = GetComponent<Renderer>();
        if (planeRenderer == null)
        {
            Debug.LogError("RandomCubesGenerator wymaga komponentu Renderer na tym samym GameObject (platforma).");
            enabled = false;
            return;
        }

        platformBounds = planeRenderer.bounds;
    }

    private void Start()
    {
        if (block == null)
        {
            Debug.LogError("Prefab 'block' nie jest przypisany w Inspectorze.");
            return;
        }

        BuildSpawnPositions();

        foreach (var p in positions)
            Debug.Log($"Planned spawn: {p}");

        StartCoroutine(GenerujObiekt());
    }

    private void BuildSpawnPositions()
    {
        positions.Clear();

        int minX = Mathf.FloorToInt(platformBounds.min.x);
        int maxX = Mathf.FloorToInt(platformBounds.max.x);
        int minZ = Mathf.FloorToInt(platformBounds.min.z);
        int maxZ = Mathf.FloorToInt(platformBounds.max.z);

        if (maxX < minX || maxZ < minZ)
        {
            for (int i = 0; i < numberOfCubes; i++)
            {
                float rx = UnityEngine.Random.Range(platformBounds.min.x, platformBounds.max.x);
                float rz = UnityEngine.Random.Range(platformBounds.min.z, platformBounds.max.z);
                float ry = platformBounds.max.y + spawnHeightOffset;
                positions.Add(new Vector3(rx, ry, rz));
            }
            return;
        }

        List<Vector2Int> gridPoints = new List<Vector2Int>();
        for (int x = minX; x <= maxX; x++)
        {
            for (int z = minZ; z <= maxZ; z++)
            {
                gridPoints.Add(new Vector2Int(x, z));
            }
        }

        System.Random rng = new System.Random();
        int n = gridPoints.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            var tmp = gridPoints[k];
            gridPoints[k] = gridPoints[n];
            gridPoints[n] = tmp;
        }

        // jeśli mamy wystarczająco dużo unikalnych punktów -> weź pierwsze numberOfCubes
        int take = Math.Min(numberOfCubes, gridPoints.Count);
        for (int i = 0; i < take; i++)
        {
            Vector2Int p = gridPoints[i];
            float ry = platformBounds.max.y + spawnHeightOffset;
            positions.Add(new Vector3(p.x, ry, p.y));
        }

        // jeśli chcemy więcej niż dostępnych punktów, dopisz dodatkowe losowe (możliwe duplikaty)
        for (int i = take; i < numberOfCubes; i++)
        {
            int idx = rng.Next(gridPoints.Count);
            Vector2Int p = gridPoints[idx];
            float ry = platformBounds.max.y + spawnHeightOffset;
            positions.Add(new Vector3(p.x, ry, p.y));
        }
    }

    private IEnumerator GenerujObiekt()
    {
        Debug.Log("Wywołano coroutine GenerujObiekt");
        objectCounter = 0;

        foreach (Vector3 pos in positions)
        {
            GameObject newObj = Instantiate(block, pos, Quaternion.identity);

            // przypisz losowy materiał jeśli są materiały
            if (materials != null && materials.Length > 0)
            {
                int matIndex = UnityEngine.Random.Range(0, materials.Length);
                Renderer r = newObj.GetComponent<Renderer>();
                if (r != null)
                {
                    r.material = materials[matIndex];
                }
            }

            objectCounter++;
            yield return new WaitForSeconds(delay);
        }

        Debug.Log($"Zakończono generowanie. Wygenerowano: {objectCounter}");
    }

    private void Update()
    {
    }
}
