using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(AudioSource), typeof(Outline))]
public class BreakerComponent : InteractableObject
{
    private Animator _animator;
    private AudioSource _audioSource;
    private Outline _outline;
    private static readonly int PullTrigger = Animator.StringToHash("Pull");
    private bool _isCooldownEnded = true;
    [SerializeField] private float cooldownTime;

    protected override void Awake()
    {
        base.Awake();
        //
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _outline = GetComponent<Outline>();
        //
        _outline.OutlineColor = Color.green;
    }

    protected override void OnInteract()
    {
        if (_isCooldownEnded)
        {
            _isCooldownEnded = false;
            StartCoroutine(nameof(SetCooldown));
            _animator.SetTrigger(PullTrigger);
        }
    }

    public void InteractAnimEvent()
    {
        _animator.ResetTrigger(PullTrigger);
        _outline.OutlineColor = Color.red;
        GameManager.GlobalPowerState = !GameManager.GlobalPowerState;
        _audioSource.Play();
    }

    private IEnumerator SetCooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        _isCooldownEnded = true;
        _outline.OutlineColor = Color.green;
    }
}