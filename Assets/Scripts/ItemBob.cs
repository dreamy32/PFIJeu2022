using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBob : MonoBehaviour
{
    // a modifier afin que le code fonctionne avec player input ainsi que s'assurer de ne pas overcall des methodes de verification pour rien

    [SerializeField] float amount,maxAmount,smoothAmount;

    [SerializeField] float rotationAmount, maxRotationAmount, smoothRotation;

    private Vector3 initialPos;
    private Quaternion initiaRotation;

    private float movementX, movementY;


    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.localPosition;
        initiaRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateSway();

        MoveSway();
        TiltSway();
    }

    private void CalculateSway()
    {
        movementX = Input.GetAxis("Mouse X");
        movementY = Input.GetAxis("Mouse Y");
    }
    private void MoveSway()
    {
        float moveX = Mathf.Clamp(movementX * amount, -maxAmount, maxAmount);
        float moveY = Mathf.Clamp(movementY * amount, -maxAmount, maxAmount);

        Vector3 finalPos = new Vector3(moveX, moveY, 0);

        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPos + initialPos, Time.deltaTime * smoothAmount);
    }
    private void TiltSway()
    {
        float tiltY = Mathf.Clamp(movementX * rotationAmount, -maxRotationAmount, maxRotationAmount);
        float tiltX = Mathf.Clamp(movementY * rotationAmount, -maxRotationAmount, maxRotationAmount);

        Quaternion finalRotation = Quaternion.Euler(new Vector3(tiltY,tiltX,0));

        transform.localRotation = Quaternion.Slerp(transform.localRotation, finalRotation * initiaRotation, Time.deltaTime * smoothRotation);
    }
}
