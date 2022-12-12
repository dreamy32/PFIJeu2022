using UnityEngine;

//This script makes the item sway when the player moves the camera
public class ItemBob : MonoBehaviour
{
    //Amount of sway, maximum amount of sway, and the smoothness of the sway
    [SerializeField] private float
        amount,
        maxAmount,
        smoothAmount;

    //Amount of rotation, maximum rotation, and smoothness of the rotation
    [SerializeField] private float
        rotationAmount,
        maxRotationAmount,
        smoothRotation;

    //Initial transform values
    private Vector3 _initialPos; 
    private Quaternion _initiaRotation;

    //Variables to keep track of the player's mouse movements in the X and Y direction
    private float _movementX, _movementY;


    private void Awake()
    {
        _initialPos = transform.localPosition;
        _initiaRotation = transform.localRotation;
    }

    private void Update()
    {
        CalculateSway();

        MoveSway();
        TiltSway();
    }

    //Calculating the sway of the item
    private void CalculateSway()
    {
        _movementX = Input.GetAxis("Mouse X");
        _movementY = Input.GetAxis("Mouse Y");
    }

    //Moving the item based on the sway
    private void MoveSway()
    {
        //Calculating the amount of sway on the X and Y axis
        float moveX =
            Mathf.Clamp(_movementX * amount, -maxAmount, maxAmount);
        float moveY =
            Mathf.Clamp(_movementY * amount, -maxAmount, maxAmount);

        //Creating a vector3 to store the final position of the item
        Vector3 finalPos = new Vector3(moveX, moveY, 0);

        //Moving the item to the final position calculated above
        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            finalPos + _initialPos,
            Time.deltaTime * smoothAmount
        );
    }

    //Tilting the item based on the sway
    private void TiltSway()
    {
        //Calculating the amount of rotation on the X and Y axis
        float tiltY =
            Mathf.Clamp(_movementX * rotationAmount, -maxRotationAmount,
                maxRotationAmount);
        float tiltX =
            Mathf.Clamp(_movementY * rotationAmount, -maxRotationAmount,
                maxRotationAmount);

        //Creating a quaternion to store the final rotation
        Quaternion finalRotation = Quaternion.Euler(new Vector3(tiltY, tiltX, 0));

        //Rotating the item to the final rotation calculated above
        transform.localRotation = Quaternion.Slerp(
            transform.localRotation,
            finalRotation * _initiaRotation,
            Time.deltaTime * smoothRotation
        );
    }
}