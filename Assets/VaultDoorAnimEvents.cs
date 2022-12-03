using UnityEngine;

public class VaultDoorAnimEvents : MonoBehaviour
{
    [SerializeField] private AudioSource audioHandle;
    [SerializeField] private AudioSource audioDoor;
    
    public void PlayAudioDoor()
    {
        audioDoor.Play();
    }
    public void PlayAudioHandle()
    {
        audioHandle.Play();
    }
}