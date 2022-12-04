using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(HideOnInteract))]
public class HidingCam : MonoBehaviour
{
    [SerializeField] Camera hidingCam;
    [SerializeField] float sensitivity = 50f;
    [SerializeField] float minAngle = -45.0f;
    [SerializeField] float maxAngle = 45.0f;

    private float rotationValue;


    private void Awake()
    {
        hidingCam.enabled = false;
    }

    public void Update()
    {
        if (hidingCam.enabled)
        {
            //Rotate view
            rotationValue += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            rotationValue = Mathf.Clamp(rotationValue, minAngle, maxAngle);
            hidingCam.transform.parent.localRotation = Quaternion.Euler(0, 0, rotationValue);
        }
    }
}