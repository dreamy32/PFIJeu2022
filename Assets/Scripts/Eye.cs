using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    [SerializeField] private Transform playerPos;
    [SerializeField] private MonsterBehaviourTreeComponent monster;
    [SerializeField] private float distance = 10f;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerPos);
        transform.rotation *= Quaternion.FromToRotation(Vector3.left, Vector3.forward);

        if(Vector3.Distance(transform.position,playerPos.position) <= distance)
        {
            monster.SwitchWing(playerPos);
        }

    }
    
    

  
}
