using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource soundSource;
    [SerializeField] AudioSource musicSource;


    public void PlaySound(AudioClip clip )
    {
        soundSource.clip = clip;
        soundSource.enabled = true;
    }
    public void StopSound(AudioClip clip)
    {
        soundSource.clip = clip;
        soundSource.enabled = false;
    }

    //public void PlayMusic(AudioClip clip)
    //{
    //    musicSource.clip = clip;
    //    musicSource.Play();
    //}
    //public void StopMusic(AudioClip clip)
    //{
    //    musicSource.clip = clip;
    //    musicSource.Stop();
    //}



}
