using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PNJSpawner : MonoBehaviour
{
    public static int PnjSpawned { get { return pnjNumberStatic; } }
    [SerializeField]
    private int pnjNumber = 30;
    private static int pnjNumberStatic;
    public GameObject pnjPrefab;
    private NavMeshTriangulation navMeshData;
    
    void Start ()
    {
        pnjNumberStatic = pnjNumber;
        navMeshData = NavMesh.CalculateTriangulation();

        for (int i=0; i<pnjNumber; ++i)
        {
            GameObject pnj = Instantiate(pnjPrefab, transform);
            pnj.transform.position = GetRandomLocation();
        }
	}

    Vector3 GetRandomLocation()
    {
        // Pick the first indice of a random triangle in the nav mesh
        int t = Random.Range(0, navMeshData.indices.Length - 3);

        // Select a random point on it
        Vector3 point = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[t]], navMeshData.vertices[navMeshData.indices[t + 1]], Random.value);
        Vector3.Lerp(point, navMeshData.vertices[navMeshData.indices[t + 2]], Random.value);

        return point;
    }
}
