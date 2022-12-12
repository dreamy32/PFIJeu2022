using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BehaviorTree
{
    public enum NodeState
    {
        Running,
        Succes,
        Failure
    }

    public abstract class Node
    {
        protected NodeState State { get; set; } = NodeState.Running;
        public Node parent;
        protected List<Node> children = new();

        Dictionary<string, object> data = new();

        public void SetData(string key, object value)
        {
            data[key] = value;
        }

        public object GetData(string key)
        {
            if (data.TryGetValue(key, out object value))
            {
                return value;
            }

            if (parent != null)
            {
                return parent.GetData(key);
            }

            return null;

        }


        protected Node()
        {
            parent = null;
        }

        protected Node(List<Node> children)
        {
            foreach (Node n in children)
            {
                Attach(n);
            }
        }

        protected void Attach(Node n)
        {
            n.parent = this;
            children.Add(n);
        }

        abstract public NodeState Evaluate();
    }


    public class Selector : Node // ou
    {
        public Selector(List<Node> n) : base(n) { }

        public override NodeState Evaluate()
        {
            foreach (Node n in children)
            {
                switch (n.Evaluate())
                {
                    case NodeState.Failure:
                        break;
                    case NodeState.Succes:
                        return State = NodeState.Succes;
                    case NodeState.Running:
                        return State = NodeState.Running;
                }
            }
            return State = NodeState.Failure;
        }


    }

    public class Sequence : Node // et
    {
        public Sequence(List<Node> n) : base(n) { }

        public override NodeState Evaluate()
        {
            foreach (Node n in children)
            {
                switch (n.Evaluate())
                {
                    case NodeState.Succes:
                        break;
                    case NodeState.Failure:
                        return State = NodeState.Failure;
                    case NodeState.Running:
                        return State = NodeState.Running;
                }
            }
            return State = NodeState.Succes;
        }
    }


    public class Inverter : Node
    {
        public Inverter(Node n) : base()
        {
            Attach(n);
        }

        public override NodeState Evaluate()
        {
            switch (children[0].Evaluate())
            {
                case NodeState.Succes:
                    State = NodeState.Failure;
                    break;
                case NodeState.Failure:
                    State = NodeState.Succes;
                    break;
                case NodeState.Running:
                    State = NodeState.Running;
                    break;
            }
            return State;
        }
    }


    public class PatrolTask : Node
    {
        Transform[] destinations;
        float waitTime;
        float elapsedTime = 0;
        int destinationIndex = 0;
        NavMeshAgent agent;
        bool isWaiting = false;
        float stoppingDistance = 1.5f;
        Animator anim;

        public PatrolTask(Transform[] destinations, float waitTime, NavMeshAgent agent,Animator anim)
        {
            this.destinations = destinations;
            this.waitTime = waitTime;
            this.agent = agent;
            this.anim = anim;
        }

        public override NodeState Evaluate()
        {
            if (isWaiting)
            {
                anim.SetBool("IsCrawling", false);
                anim.SetBool("IsCrawlingFast", false);

                elapsedTime += Time.deltaTime;
                if (elapsedTime > waitTime)
                {

                    isWaiting = false;

                }
            }
            else
            {
                anim.SetBool("IsCrawling", true);
                if (Vector3.Distance(agent.transform.position, destinations[destinationIndex].position) < stoppingDistance)
                {
                    destinationIndex = (destinationIndex + 1) % destinations.Length;
                    isWaiting = true;
                    elapsedTime = 0;
                }
                else
                {
                    agent.destination = destinations[destinationIndex].position;
                }
            }

            State = NodeState.Running;
            return State;

        }

    }

    public class GoToTarget : Node
    {
        Transform target;
        NavMeshAgent agent;

        public GoToTarget(Transform target, NavMeshAgent agent)
        {
            this.target = target;
            this.agent = agent;
        }

        public override NodeState Evaluate()
        {
            agent.destination = target.position;
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                State = NodeState.Succes;
                return State;
            }
            State = NodeState.Running;
            return State;

        }

    }

    public class GoToTargetPriority : Node
    {
        Transform target;
        NavMeshAgent agent;
        float stoppingDistance = 1.5f;
        Animator anim;

        public GoToTargetPriority(Transform target, NavMeshAgent agent, Animator anim)
        {
            this.target = target;
            this.agent = agent;
            this.anim = anim;
        }

        public override NodeState Evaluate()
        {
            anim.SetBool("IsCrawling", true);
            anim.SetBool("IsCrawlingFast", false);
            if (target == null) // le monstre est alle a la target
            {
                return NodeState.Failure;
            }
            agent.destination = target.position;
            if (agent.remainingDistance <= stoppingDistance)
            {
                State = NodeState.Succes;
                target = null;
                return State;
            }
            State = NodeState.Running;
            return State;

        }
    }

    public class placeHolder : Node
    {
        public placeHolder()
        {

        }

        public override NodeState Evaluate()
        {
            return NodeState.Failure;
        }
    }

    public class IsInPOV : Node
    {
        //cone de detection
        float height;
        float angle;
        float distance;

        Transform target;
        Transform self;
        Animator anim;
        public IsInPOV(Transform target, Transform self,Animator anim, float height = 2, float angle = 30, float distance = 10)
        {
            this.height = height;
            this.angle = angle;
            this.distance = distance;
            this.target = target;
            this.self = self;
            this.anim = anim;

        }

        public override NodeState Evaluate()
        {
            anim.SetBool("IsCrawling", false);
            anim.SetBool("IsCrawlingFast", false);
            Vector3 origine = self.position;
            Vector3 destination = target.position;
            Vector3 direction = destination - origine;

            if(direction.y < 0 || direction.y > height) // bonne hauteur
            {
                //Debug.Log("hauteur");
                anim.SetBool("IsCrawling", true);
                return NodeState.Failure;
            }

            if (Vector3.Distance(origine,destination) >= distance)
            {
                anim.SetBool("IsCrawling", true);
                return NodeState.Failure;
            }

            direction.y = 0;
            float deltaAngle = Vector3.Angle(direction, self.forward);
            //Debug.Log(deltaAngle);
            if (deltaAngle > angle) // dans l'angle
            {
                //Debug.Log("angle");
                anim.SetBool("IsCrawling", true);
                return NodeState.Failure;
            }

            origine.y += height / 2;
            destination.y = origine.y;


            if (Physics.Raycast(origine, direction, out RaycastHit hit)) // regarde s'il peut le voir (obstacle)
            {
                Debug.Log("hit");
                Debug.Log(hit.collider.tag);
                if (!target.CompareTag(hit.collider.tag))
                {
                    //Debug.DrawRay(origine, direction, Color.cyan);
                    //Debug.Log(hit.collider.name);
                    //Debug.Log("hit");
                    anim.SetBool("IsCrawling", true);
                    return NodeState.Failure;
                }

            }
            anim.SetBool("IsCrawlingFast", true);
            return NodeState.Succes;
        }
    }

    public class IsWithinRange : Node
    {
        Transform target;
        Transform self;
        float detectionRange;

        public IsWithinRange(Transform target,Transform self, float detectionRange)
        {
            this.target = target;
            this.self = self;
            this.detectionRange = detectionRange;

        }

        public override NodeState Evaluate()
        {
            State = NodeState.Failure;
            if(Vector3.Distance(self.position,target.position) <= detectionRange){
                State = NodeState.Succes;
            }
            return State;
        }

    }


}


