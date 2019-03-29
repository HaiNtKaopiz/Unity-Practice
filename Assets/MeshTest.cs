using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTest : MonoBehaviour
{
    private MeshFilter meshFilter;
    private Mesh mesh;

    // Start is called before the first frame update
    void Start()
    {
        meshFilter = gameObject.GetComponent<MeshFilter>();
        mesh = new Mesh();

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        Vector3 A = new Vector3(0, 0, 0);
        Vector3 B = new Vector3(1, 0, 0);
        Vector3 C = new Vector3(1, 1, 0);
        Vector3 D = new Vector3(0, 1, 0);

    }
}
