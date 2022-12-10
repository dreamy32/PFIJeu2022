using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerSanity { Sane, Scared, Insane }
public enum PlayerState { Hide, Dark}
public class SanitySystem : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField] AudioClip[] scarySounds;
    private AudioSource audioSource;


    [Header("Sanity")]
    [SerializeField]  Image sanityBar;
    static Image staticSanityBar;
    [SerializeField] static float sanityLoss = 0.01f;
    [SerializeField] static float sanityGain = 0.01f;
    [SerializeField] Image stateImage;
    [SerializeField] Image saneImage;
    [SerializeField] Image scaredImage;
    [SerializeField] Image insaneImage;


    private static float currentSanity;
    PlayerSanity sanityLevel;

    private bool inTheDark = true;







    #region Sanity
    private void ControlSanity()
    {
        // Increase if
        // seeing eye/bloodbuckets
        // in chase
        // in the dark


        // Decrease if
        // Inside hidingSpot
        // Consuming Molson



        if (inTheDark)
            IncreaseSanity();
        //else
        //{
        //    if (currentStamina < 1 && IsGrounded)
        //        RefillStamina();
        //}
    }
    static public void ResetSanity()
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


    static void IncreaseSanity()
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
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        staticSanityBar = sanityBar;
    }


    void PlayRandomSound()
    {
        int rand = Random.Range(0, scarySounds.Length);
        audioSource.clip = scarySounds[rand];
        audioSource.Play();
    }


}
