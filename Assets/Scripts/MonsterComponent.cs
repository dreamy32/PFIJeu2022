using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class EnemyComponent : MonoBehaviour
{
    public delegate void OnGrabAction();

    public event OnGrabAction OnGrab;
    [SerializeField] private GameObject player;
    private NavMeshAgent _navMeshA;
    Animator animator;
    Rigidbody rb;

    //Unity Event
    private void OnCollisionEnter(Collision c)
    {
        if (player.layer.Equals(c.gameObject.layer))
        {
            Grab();
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        _navMeshA = GetComponent<NavMeshAgent>();
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