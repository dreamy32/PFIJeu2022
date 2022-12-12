using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using static BehaviorTree;

[RequireComponent(typeof(NavMeshAgent))]
public class MonsterBehaviourTreeComponent : MonoBehaviour
{
    [SerializeField] Transform target; // joueur
    [SerializeField] Transform[] destinations; // aile gauche
    [SerializeField] Transform[] altDestinations; // aile droit
    [SerializeField] float waitTime = 2;
    [SerializeField] float detectionRange = 3;
    NavMeshAgent agent;
    Node root;
    bool AileDroit = false;
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
            new placeHolder(),
            new PatrolTask(destinations,waitTime,agent)
        });
    }

    public void SwitchWing(Transform Ptarget) //losqu'il passe devant l'oeil
    {
        root = new Selector(new List<Node>()
        {
            new Sequence(new List<Node>()
            {
                new IsInPOV(target,transform),
                new GoToTarget(target,agent)
            }),
            new GoToTargetPriority(Ptarget,agent), // mets le joueur en priority
            new PatrolTask(altDestinations,waitTime,agent)
        });
    }

    public void PriorityTarget(Transform Ptarget) // change la priorite
    {
        if (AileDroit)
        {
            root = new Selector(new List<Node>()
            {
                new Sequence(new List<Node>()
                {
                    new IsInPOV(target,transform),
                    new GoToTarget(target,agent)
                }),
                new GoToTargetPriority(Ptarget,agent),
                new PatrolTask(altDestinations,waitTime,agent)
            });
        }
        else
        {
            root = new Selector(new List<Node>()
            {
                new Sequence(new List<Node>()
                {
                    new IsInPOV(target,transform),
                    new GoToTarget(target,agent)
                }),
                new GoToTargetPriority(Ptarget,agent),
                new PatrolTask(destinations,waitTime,agent)
            });
        }

    }


    // Update is called once per frame
    void Update()
    {
        root.Evaluate();
    }
}
