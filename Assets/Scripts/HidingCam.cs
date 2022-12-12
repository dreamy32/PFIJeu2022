using UnityEngine;
public class HidingCam : MonoBehaviour
{
    [SerializeField] private Camera hidingCam;
    [SerializeField] private float sensitivity = 50f;
    [SerializeField] private float minAngle = -45.0f;
    [SerializeField] private float maxAngle = 45.0f;

    private float _rotationValue;


    private void Awake()
    {
        hidingCam.enabled = false;
    }

    public void Update()
    {
        if (hidingCam.enabled)
        {
            //Rotate view
            _rotationValue += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            _rotationValue = Mathf.Clamp(_rotationValue, minAngle, maxAngle);
            hidingCam.transform.parent.localRotation = Quaternion.Euler(0, 0, _rotationValue);
        }
    }
}