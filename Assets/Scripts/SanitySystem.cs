using UnityEngine;
using UnityEngine.UI;

public enum PlayerSanity
{
    Sane,
    Scared,
    Insane
}

[RequireComponent(typeof(AudioSource))]
public class SanitySystem : MonoBehaviour
{
    [Header("Sounds")] 
    [SerializeField] private AudioClip[] scarySounds;
    [SerializeField] float audioSanityLost = 0.15f;
    private AudioSource _audioSource;


    [Header("Sanity")] [SerializeField] private Image sanityBar;

    private static Image _staticSanityBar;
    [SerializeField] private float sanityLoss = 0.005f;
    [SerializeField] private float sanityGain = 0.005f;
    [SerializeField] private Image stateImage;
    [SerializeField] private Image saneImage;
    [SerializeField] private Image scaredImage;
    [SerializeField] private Image insaneImage;


    private static float _currentSanity;
    private PlayerSanity _sanityLevel;


    public static bool InTheDark = true;
    private bool _isHiding = false;


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _staticSanityBar = sanityBar;
    }

    private void OnDestroy()
    {
        _currentSanity = 0;
        _staticSanityBar = null;
        InTheDark = true;
    }

    #region Sanity

    private void ControlSanity()
    {
        // Increase si le courant est ferm�
        if (InTheDark)
            IncreaseSanity();
        else
            DecreaseSanity();


        //switch (sanityLevel)
        //{
        //    case PlayerSanity.Sane:
        //        //PlayRandomSound();
        //        break;
        //    case PlayerSanity.Scared:
               
        //        break;
        //    case PlayerSanity.Insane:
                
        //        break;
        //}
    }

    public static void ResetSanity()
    {
        _currentSanity = 0f;
        _staticSanityBar.fillAmount = _currentSanity / 1;
    }

    private void DecreaseSanity()
    {
        _currentSanity -= sanityLoss * Time.deltaTime;
        if (_currentSanity <= 0)
        {
            _currentSanity = 0;
        }

        _staticSanityBar.fillAmount = _currentSanity / 1;
    }

    private void IncreaseSanity(bool determinedAmount = false)
    {
        if (!determinedAmount)
            _currentSanity += sanityGain * Time.deltaTime;
        else
            _currentSanity += audioSanityLost;

        if (_currentSanity >= 1)
            _currentSanity = 1;

        _staticSanityBar.fillAmount = _currentSanity / 1;
    }

    #endregion


    private void Update()
    {
        _sanityLevel = GetPlayerSanity();
        ControlSanity();
    }

    private PlayerSanity GetPlayerSanity()
    {
        if (_currentSanity <= 0.5f)
        {
            stateImage.sprite = saneImage.sprite;
            return PlayerSanity.Sane;
        }
        else if (_currentSanity > 0.5f && _currentSanity <= 0.8f)
        {
            stateImage.sprite = scaredImage.sprite;
            return PlayerSanity.Scared;
        }
        else
            stateImage.sprite = insaneImage.sprite;

        return PlayerSanity.Insane;
    }

    private IEnumerator PlayRandomSound()
    {
        int maxValueRandom = 7;
        int multiple = 2;
        while (true)
        {
            if(Random.Range(0, maxValueRandom) % multiple == 0)
            {
                IncreaseSanity(true);

                if (!audioSource.isPlaying)
                {
                    int rand = Random.Range(0, scarySounds.Length);
                    audioSource.clip = scarySounds[rand];
                    audioSource.Play();
                }
            }
            yield return new WaitForSeconds(45);
        }
    }
}
