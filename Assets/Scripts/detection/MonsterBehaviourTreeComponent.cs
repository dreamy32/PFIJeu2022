using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using static BehaviorTree;

[RequireComponent(typeof(NavMeshAgent))]
public class MonsterBehaviourTreeComponent : MonoBehaviour
{
    [SerializeField] Transform target; // joueur
    [SerializeField] Transform[] destinations;
    [SerializeField] float waitTime = 2;
    [SerializeField] float detectionRange = 3;
    NavMeshAgent agent;
    Node root;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetupTree();
    }

    private void SetupTree()
    {
        root = new Selector(new List<Node>()
        {
            new Sequence(new List<Node>()
            {
                new IsInPOV(target,transform),
                new GoToTarget(target,agent)
            }),
            new PatrolTask(destinations,waitTime,agent)
        });
    }

    // Update is called once per frame
    void Update()
    {
        root.Evaluate();
    }
}
