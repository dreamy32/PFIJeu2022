using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class VisualDetectionComponent : MonoBehaviour
{
    [SerializeField] float distance = 10;
    [SerializeField] float angle = 30;
    [SerializeField] float height = 1.0f;
    [SerializeField] Color meshColor = Color.red;
    [SerializeField] List<GameObject> prey = new();
    [SerializeField] float waitTime = 2;

    float time = 0;
    Mesh mesh;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= waitTime)
        {
        Seek();
        time = 0;
         }

    }


    private void Seek()
    {
        for (int compteur = 0; compteur < prey.Count; compteur++)
        {

            if (IsInFov(prey[compteur]))
            {
                Debug.Log("Je te vois" + prey[compteur].name);
            }
        }
    }

    private bool IsInFov(GameObject obj)
    {
        Vector3 Origine = transform.position;
        Vector3 destination = obj.transform.position;
        Vector3 direction = destination - Origine;

        if (direction.y < 0 || direction.y > height) // bonne hauteur
        {
            return false;
        }

        direction.y = 0;
        float deltaAngle = Vector3.Angle(direction, transform.forward);
        if (deltaAngle > angle) // dans l'angle
        {
            return false;
        }

        Origine.y += height / 2;
        destination.y = Origine.y;

        RaycastHit hit;

        if (Physics.Raycast(Origine, direction, out hit)) // regarde s'il peut le voir (obstacle)
        {
            if (hit.collider.name != obj.name)
            {
                Debug.DrawRay(Origine, direction, Color.cyan);
                //Debug.Log(hit.collider.name);
                return false;
            }

        }


        return true;
    }

    Mesh CreateWedgeMesh()
    {
        Mesh mesh = new Mesh();

        int segments = 10;
        int numTriangles = (segments * 4) + 4;
        int numVertices = numTriangles * 3;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * distance;
        Vector3 bottomRight = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;

        Vector3 topCenter = bottomCenter + Vector3.up * height;
        Vector3 topRight = bottomRight + Vector3.up * height;
        Vector3 topLeft = bottomLeft + Vector3.up * height;

        int vert = 0;

        //gauche
        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomLeft;
        vertices[vert++] = topLeft;

        vertices[vert++] = topLeft;
        vertices[vert++] = topCenter;
        vertices[vert++] = bottomCenter;

        //droit
        vertices[vert++] = bottomCenter;
        vertices[vert++] = topCenter;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomCenter;

        float currentAngle = -angle;
        float deltaAngle = (angle * 2) / segments;
        for (int compteur = 0; compteur < segments; compteur++)
        {
            bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * distance;
            bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * distance;

            topRight = bottomRight + Vector3.up * height;
            topLeft = bottomLeft + Vector3.up * height;

            currentAngle += deltaAngle;

            //loin
            vertices[vert++] = bottomLeft;
            vertices[vert++] = bottomRight;
            vertices[vert++] = topRight;

            vertices[vert++] = topRight;
            vertices[vert++] = topLeft;
            vertices[vert++] = bottomLeft;

            //top

            vertices[vert++] = topCenter;
            vertices[vert++] = topLeft;
            vertices[vert++] = topRight;


            //base

            vertices[vert++] = bottomCenter;
            vertices[vert++] = bottomRight;
            vertices[vert++] = bottomLeft;
        }




        for (int compteur = 0; compteur < numVertices; compteur++)
        {
            triangles[compteur] = compteur;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }

    private void OnValidate()
    {
        mesh = CreateWedgeMesh();
    }

    private void OnDrawGizmos()
    {
        if (mesh)
        {
            Gizmos.color = meshColor;
            Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
        }
    }

}
