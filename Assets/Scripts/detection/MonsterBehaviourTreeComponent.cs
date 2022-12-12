using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using static BehaviorTree;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class MonsterBehaviourTreeComponent : MonoBehaviour
{
    [SerializeField] Transform target; // joueur
    [SerializeField] Transform[] destinations; // aile gauche
    [SerializeField] Transform[] altDestinations; // aile droit
    [SerializeField] float waitTime = 2;
    NavMeshAgent agent;
    Animator anim;
    Node root;
    bool AileDroit = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        SetupTree();
    }

    private void SetupTree()
    {
        
        root = new Selector(new List<Node>()
        {
            new Sequence(new List<Node>()
            {
                new IsInPOV(target,transform,anim),
                new GoToTarget(target,agent)
            }),
            new placeHolder(),
            new PatrolTask(destinations,waitTime,agent,anim)
        });
    }

    public void SwitchWing(Transform Ptarget) //losqu'il passe devant l'oeil
    {
        root = new Selector(new List<Node>()
        {
            new Sequence(new List<Node>()
            {
                new IsInPOV(target,transform,anim),
                new GoToTarget(target,agent)
            }),
            new GoToTargetPriority(Ptarget,agent,anim), // mets le joueur en priority
            new PatrolTask(altDestinations,waitTime,agent,anim)
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
                    new IsInPOV(target,transform,anim),
                    new GoToTarget(target,agent)
                }),
                new GoToTargetPriority(Ptarget,agent,anim),
                new PatrolTask(altDestinations,waitTime,agent,anim)
            });
        }
        else
        {
            root = new Selector(new List<Node>()
            {
                new Sequence(new List<Node>()
                {
                    new IsInPOV(target,transform,anim),
                    new GoToTarget(target,agent)
                }),
                new GoToTargetPriority(Ptarget,agent,anim),
                new PatrolTask(destinations,waitTime,agent,anim)
            });
        }

    }


    // Update is called once per frame
    void Update()
    {
        

        root.Evaluate();
    }
}
