using UnityEngine;

public class WaveDeformComponent : MonoBehaviour
{
    [SerializeField] private float amplitude = 4f;

    private MeshFilter _meshFilter;
    private Vector3[] _vertices;
    private float _yOffset;


    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _vertices = _meshFilter.mesh.vertices;
    }

    private void Update() 
    {
        _yOffset += Time.deltaTime;
        for (int i = 0; i < _vertices.Length; i++)
        {
            _vertices[i].y = amplitude * Mathf.Sin((transform.position.x + _vertices[i].x)  + _yOffset);
        }
        _meshFilter.mesh.vertices = _vertices;
        _meshFilter.mesh.RecalculateNormals();
    }
}