using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    [SerializeField] private Transform playerPos;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerPos);
        transform.rotation *= Quaternion.FromToRotation(Vector3.left, Vector3.forward);

    }
    
    

  
}
