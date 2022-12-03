using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class PlaneMeshGeneratorComponent : MonoBehaviour
{
    [SerializeField] int height = 1;
    [SerializeField] int width = 1;
    public int verticesWidth = 5;
    public int verticesHeight = 5;


    private List<int> triangles;
    private List<Vector3> vertices;
    private List<Vector2> uvs;

    void Awake()
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();
        uvs = new List<Vector2>();
        GetComponent<MeshFilter>().mesh = CreatePlaneMesh();

    }


    private Mesh CreatePlaneMesh()
    {
        Mesh planeMesh = new Mesh();
        // Creer sommets
        for (float y = 0; y < verticesHeight; y++)
        {
            for (float x = 0; x < verticesWidth; x++)
            {
                uvs.Add(new Vector2(x / verticesWidth, y / verticesHeight));
                vertices.Add(new Vector3(x * width, y * height, 0));
            }
        }



        // Creer triangles
        for (int y = 0, i = 0; y < verticesHeight - 1; y++, i++)
        {
            for (int x = 0; x < verticesWidth - 1; x++, i++)
            {
                // left triangle
                triangles.Add(i);
                triangles.Add(i + verticesWidth);
                triangles.Add(i + verticesWidth + 1);

                //// right triangle
                triangles.Add(i);
                triangles.Add(i + verticesWidth + 1);
                triangles.Add(i + 1);
            }
        }
      


        planeMesh.vertices = vertices.ToArray();
        planeMesh.triangles = triangles.ToArray();
        planeMesh.uv = uvs.ToArray();
        planeMesh.name = "Plane";
        planeMesh.RecalculateNormals();
        return planeMesh;
    }

    void Update()
    {
        Vector3[] vertices = GetComponent<MeshFilter>().mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].y = Time.deltaTime;
        }
        GetComponent<MeshFilter>().mesh.vertices = vertices;
        GetComponent<MeshFilter>().mesh.RecalculateNormals();
    }
}
