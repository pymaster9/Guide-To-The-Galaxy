using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batcher : MonoBehaviour
{
    public List<GameObject> inbatch;
    public Mesh currentmesh;
    List<Vector3> verteces;
    List<int> triangles;
    // Start is called before the first frame update
    void CreateMesh()
    {
        currentmesh = new Mesh();
        verteces = new List<Vector3>();
        triangles = new List<int>();

        int trianglecounter = 0;
        foreach (GameObject curr in inbatch)
        {
            Mesh mesh = curr.GetComponent<MeshFilter>().mesh;
            foreach (Vector3 vertex in mesh.vertices)
            {
                verteces.Add((vertex * curr.transform.localScale.x) + curr.transform.position);
            }
            foreach (int Triangle in mesh.triangles)
            {

                triangles.Add(Triangle + trianglecounter);
            }
            trianglecounter += mesh.vertexCount;
        }
        currentmesh.vertices = verteces.ToArray();
        currentmesh.triangles = triangles.ToArray();
        GetComponent<MeshFilter>().mesh = currentmesh;
    }

    // Update is called once per frame
    void Start()
    {
        Invoke("CreateMesh", Random.Range(1f, 5f));
    }
}
