using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class WaveDeformComponent : MonoBehaviour
{
    [SerializeField] private float amplitude = 1f;
    private MeshFilter _meshFilter;

    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
    }
    private void Update()
    {
        Vector3[] vertices = _meshFilter.mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].y = amplitude * Mathf.Sin((Time.timeSinceLevelLoad + vertices[i].x));
        }

        _meshFilter.mesh.vertices = vertices;
    }
}
