using System;
using System.Collections;
using UnityEngine;

public class WaveDeformComponent : MonoBehaviour
{
    
    [SerializeField] private float amplitude = 4f;
    [SerializeField] private float radStepPerFrame;

    private MeshFilter _meshFilter;

    float offset;

    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();

    }



    //OpenAI (Above is me)
    private void Update() 
    {
        offset += Time.deltaTime * radStepPerFrame;
        Vector3[] vertices = _meshFilter.mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].y = amplitude * Mathf.Sin((transform.position.x + vertices[i].x) / 2f + offset);
        }
        _meshFilter.mesh.vertices = vertices;
        _meshFilter.mesh.RecalculateNormals();

    }
}