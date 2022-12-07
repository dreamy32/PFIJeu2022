using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(HidingCam), typeof(AudioListener))]
public class HideOnInteract : InteractableObject
{
    [FormerlySerializedAs("_hidingCam")] [SerializeField] private Camera hidingCam;
    [SerializeField] private Canvas hideCanvas;
    //
    private AudioListener _audioListener;
    //
    private Transform player;
    protected override void Awake()
    {
        base.Awake();
        _audioListener = GetComponent<AudioListener>();
        _audioListener.enabled = false;
    }

    public void GetOut()
    {
        hidingCam.enabled = false;
        hideCanvas.gameObject.SetActive(false);
        player.gameObject.SetActive(true);
        _audioListener.enabled = false;
    }
    protected override void OnInteract()
    {
        player = Camera.main.transform.root;
        //
        hidingCam.enabled = true;
        player.gameObject.SetActive(false);
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