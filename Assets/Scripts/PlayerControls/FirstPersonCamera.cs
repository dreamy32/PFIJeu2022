using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] Transform player;
    public float sensitivity = 100;
    float _verticalRotation = 0;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 delta = Mouse.current.delta.ReadValue() * Time.deltaTime * sensitivity;
        
        _verticalRotation -= delta.y; 
        _verticalRotation = Mathf.Clamp(_verticalRotation, -90, 90);
        transform.localRotation = Quaternion.Euler(_verticalRotation, 0, 0);
        
        player.Rotate(Vector3.up * delta.x);
    }
}
