using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SanityOnTriggerEvents : MonoBehaviour
{
    [SerializeField] UnityEvent onTriggerEnter;
    [SerializeField] UnityEvent onTriggerExit;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        onTriggerEnter.Invoke();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            onTriggerExit.Invoke();
    }
}
