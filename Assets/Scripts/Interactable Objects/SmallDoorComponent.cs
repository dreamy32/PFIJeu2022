using UnityEngine;

public class SmallDoorComponent : InteractableObject
{
    private Animator _animator;
    private bool _isOpen;
    private AudioSource _audio;
    
    [Header("AudioClips")]
    [SerializeField] private AudioClip openingSound;
    [SerializeField] private AudioClip closingSound;

    private static readonly int OpenTrigger = Animator.StringToHash("Open");
    private static readonly int CloseTrigger = Animator.StringToHash("Close");
    protected override void Awake()
    {
        //InteractableObject attributes
        base.Awake();
        SingleUsage = false; //�viter de call l'animator pour rien
        _isOpen = false;
        _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }
    private void PlaySound(AudioClip clip)
    {
        _audio.clip = clip;
        _audio.Play();
    }
    protected override void OnInteract()
    {
        if (!_isOpen)
        {
            PlaySound(openingSound);

            _animator.SetTrigger(OpenTrigger);
            _animator.ResetTrigger(CloseTrigger);
            _isOpen = true;
        }
        else
        {
            PlaySound(closingSound);

            _animator.SetTrigger(CloseTrigger);
            _animator.ResetTrigger(OpenTrigger);
            _isOpen = false;
        }
    }
}
