using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HidingCam : MonoBehaviour
{
    [SerializeField] Camera hidingCam;
    [SerializeField] float sensitivity = 50f;
    [SerializeField] float minAngle = -45.0f;
    [SerializeField] float maxAngle = 45.0f;

    float rotationValue;


    private void Awake()
    {
        hidingCam.enabled = false;
    }
    public Camera Camera
    {
        get { return hidingCam; }
    }
    public void Update()
    {
        
        if(Camera.enabled)
        {   
            GameObject player = transform.GetChild(0).gameObject;
            //Rotate view
            rotationValue += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            rotationValue = Mathf.Clamp(rotationValue, minAngle, maxAngle);
            player.transform.localRotation = Quaternion.Euler(0, 0, rotationValue);
        }
    }
}
