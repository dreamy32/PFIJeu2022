using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator), typeof(Rigidbody))]
public class EnemyComponent : MonoBehaviour
{
    //Custom Event
    public delegate void OnGrabAction();
    public event OnGrabAction OnGrab;
    
    //The player to follow
    [SerializeField] private GameObject player;
    
    //
    private NavMeshAgent _navMeshA;
    private Animator _animator;
    private Rigidbody _rb;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _navMeshA = GetComponent<NavMeshAgent>();
    }

    private void OnCollisionEnter(Collision c)
    {
        if (player.layer.Equals(c.gameObject.layer))
        {
            Grab();
        }
    }

    private void Update()
    {
        //NavMesh destination
        _navMeshA.SetDestination(player.transform.position);
        //Animator
        //animator.SetBool("isRunning", this.transform.position.magnitude > 0);
        //animator.SetBool("isFlying", _navMeshA.isOnOffMeshLink);
    }

    //Custom Event
    private void Grab() => OnGrab?.Invoke();
}