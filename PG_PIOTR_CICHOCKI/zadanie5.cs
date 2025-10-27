using UnityEngine;

using System.Collections.Generic;

public class zadanie5 : MonoBehaviour
{
    public GameObject cubePrefab;
    static int planesize =10;
    public int cubestospawn = 10;


    public int halfplane = planesize/2;
    public float halfcube = 0.5f;

   
    void Start()
    {
        HashSet<Vector2Int> usedPositions= new HashSet<Vector2Int>();
        for( int i =0; i<cubestospawn; i++){
            Vector2Int pos;
            do{
                int x = Random.Range(-(int)(halfplane-halfcube),(int)(halfplane-halfcube));
                int z = Random.Range(-(int)(halfplane-halfcube),(int)(halfplane-halfcube));
                pos = new Vector2Int(x,z);
            }
            while (usedPositions.Contains(pos));
            usedPositions.Add(pos);

            Vector3 spawnPosition = new Vector3(pos.x, 0.5f, pos.y);
            Instantiate(cubePrefab, spawnPosition, Quaternion.identity);

        }
    }
}
