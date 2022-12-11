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
    private MeshFilter _meshFilter;


    private List<int> triangles;
    private List<Vector3> vertices;
    private List<Vector2> uvs;

    Mesh planeMesh;

    void Awake()
    {



        planeMesh = new Mesh();
        _meshFilter = GetComponent<MeshFilter>();
        _meshFilter.mesh = planeMesh;
        CreatePlaneMesh();


    }

    private void CreatePlaneMesh()
    {

        vertices = new List<Vector3>();
        uvs = new List<Vector2>();
        triangles = new List<int>();

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
        planeMesh.Clear();
        planeMesh.vertices = vertices.ToArray();
        planeMesh.triangles = triangles.ToArray();
        planeMesh.uv = uvs.ToArray();
        planeMesh.name = "Plane";
        planeMesh.RecalculateNormals();

    }

}








