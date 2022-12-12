using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerSanity { Sane, Scared, Insane }
public class SanitySystem : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField] AudioClip[] scarySounds;
    private AudioSource audioSource;


    [Header("Sanity")]
    [SerializeField]  Image sanityBar;
    static Image staticSanityBar;
    [SerializeField]  float sanityLoss = 0.005f;
    [SerializeField]  float sanityGain = 0.005f;
    [SerializeField] Image stateImage;
    [SerializeField] Image saneImage;
    [SerializeField] Image scaredImage;
    [SerializeField] Image insaneImage;


    private static float currentSanity;
    PlayerSanity sanityLevel;
    

    public static bool inTheDark = true;
    private bool isHiding = false;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        staticSanityBar = sanityBar;
    }
    private void OnDestroy()
    {
        currentSanity = 0;
        staticSanityBar = null;
        inTheDark = true;
    }

    #region Sanity
    private void ControlSanity()
    {
        // Increase si le courant ainsi que la flashlight sont fermés
        //if (!GameManager.GlobalPowerState)
        //    IncreaseSanity();
        if (inTheDark)
            IncreaseSanity();
        else
            DecreaseSanity();


        switch (sanityLevel)
        {
            case PlayerSanity.Sane:
                //PlayRandomSound();
                break;
            case PlayerSanity.Scared:
               
                break;
            case PlayerSanity.Insane:
                
                break;
        }
    }
    public static  void ResetSanity()
    {
        currentSanity = 0f;
        staticSanityBar.fillAmount = currentSanity / 1;
    }
    void DecreaseSanity()
    {
        currentSanity -= sanityLoss * Time.deltaTime;
        if (currentSanity <= 0)
        {
            currentSanity = 0;
        }
        staticSanityBar.fillAmount = currentSanity / 1;
    }


    private void IncreaseSanity()
    {
        currentSanity += sanityGain * Time.deltaTime;
        if (currentSanity >= 1)
            currentSanity = 1;

        staticSanityBar.fillAmount = currentSanity / 1;
    }
    #endregion


    private void Update()
    {
        sanityLevel = GetPlayerSanity();
        ControlSanity();
    }

    private PlayerSanity GetPlayerSanity()
    {
        if (currentSanity <=0.5f)
        {
            stateImage.sprite = saneImage.sprite;
            return PlayerSanity.Sane;
        }
        else if(currentSanity > 0.5f && currentSanity <= 0.8f)
        {
            stateImage.sprite = scaredImage.sprite;
            return PlayerSanity.Scared;
        }
        else
            stateImage.sprite = insaneImage.sprite;
            return PlayerSanity.Insane;
    }

    void PlayRandomSound()
    {
        if (!audioSource.isPlaying)
        {
            int rand = Random.Range(0, scarySounds.Length);
            audioSource.clip = scarySounds[rand];
            audioSource.Play();
        }
       
    }


}
