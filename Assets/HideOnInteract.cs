using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(HidingCam))]
public class HideOnInteract : InteractableObject
{
    [FormerlySerializedAs("_hidingCam")] [SerializeField] private Camera hidingCam;
    [SerializeField] private Canvas hideCanvas;
    private Camera playerCam;
    public void GetOut()
    {
        hidingCam.enabled = false;
        hideCanvas.gameObject.SetActive(false);
        playerCam.enabled = true;
    }
    protected override void OnInteract()
    {
        playerCam = Camera.main;
        //
        hidingCam.enabled = true;
        playerCam.enabled = false;
        hideCanvas.gameObject.SetActive(true);
        StartCoroutine(nameof(ExitHide));
    }
    private IEnumerator ExitHide()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        GetOut();
    }
}