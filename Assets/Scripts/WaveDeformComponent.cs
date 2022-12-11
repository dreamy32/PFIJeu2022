using System;
using System.Collections;
using UnityEngine;

public class WaveDeformComponent : MonoBehaviour
{
    
    [SerializeField] private float amplitude = 4f;

    private MeshFilter _meshFilter;
    private Vector3[] vertices;
    float yOffset;


    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        vertices = _meshFilter.mesh.vertices;
    }

    private void Update() 
    {
        yOffset += Time.deltaTime;
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].y = amplitude * Mathf.Sin((transform.position.x + vertices[i].x)  + yOffset);
        }
        _meshFilter.mesh.vertices = vertices;
        _meshFilter.mesh.RecalculateNormals();

    }
}