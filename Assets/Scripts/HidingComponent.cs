using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HidingComponent : MonoBehaviour
{
    [SerializeField] LayerMask hideable;
    [SerializeField] Canvas canvas;
    [SerializeField] Image hidePrompt;

    Camera playerCam;
    Camera hidingCam;
    GameObject hidingSpot;
   
    bool isHiding = false;


    float range = 1.5f;
    void Awake()
    {
        playerCam = Camera.main;
        canvas.enabled = false;
        hidePrompt.enabled = false;

    }


    void Update()
    {
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out RaycastHit hit, range, hideable))
        { 
            hidingSpot = hit.collider.gameObject;
            hidePrompt.enabled = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                Hide(hidingSpot);
            }
        }
        else
            hidePrompt.enabled = false;
    }

    private void Hide(GameObject hidingSpot)
    {
        hidingCam = hidingSpot.GetComponent<HidingCam>().Camera;
        gameObject.SetActive(false);
        hidingCam.enabled = true;
        playerCam.enabled = false;
        canvas.enabled = true;
        hidePrompt.enabled = false;
    }

    // invoked when pressing Space
    public void GetOut()
    {
        hidingCam = hidingSpot.GetComponent<HidingCam>().Camera;
        hidingCam.enabled = false;
        playerCam.enabled = true;
        gameObject.SetActive(true);
        canvas.enabled = false;
    
    }


}
