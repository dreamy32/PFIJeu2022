using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(HidingCam), typeof(AudioListener))]
public class HideOnInteract : InteractableObject
{
    [FormerlySerializedAs("_hidingCam")] [SerializeField] private Camera hidingCam;
    [SerializeField] private Canvas hideCanvas;
    [SerializeField] private Image iconPlaceholder;
    //
    private AudioListener _audioListener;
    //
    private Transform _camTransform;
    protected override void Awake()
    {
        base.Awake();
        _audioListener = GetComponent<AudioListener>();
        _audioListener.enabled = false;
    }

    public void GetOut()
    {
        var color = iconPlaceholder.color;
        color.a = 255;
        iconPlaceholder.color = color;
        hidingCam.enabled = false;
        hideCanvas.gameObject.SetActive(false);
        _camTransform.gameObject.SetActive(true);
        _audioListener.enabled = false;
    }
    protected override void OnInteract()
    {
        _camTransform = Camera.main.transform.root;
        //
        var color = iconPlaceholder.color;
        color.a = 0;
        iconPlaceholder.color = color;
        hidingCam.enabled = true;
        _camTransform.gameObject.SetActive(false);
        _audioListener.enabled = true;
        hideCanvas.gameObject.SetActive(true);
        StartCoroutine(nameof(ExitHide));
    }
    private IEnumerator ExitHide()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        GetOut();
    }
}