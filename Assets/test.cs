using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    [SerializeField] Transform cible;
    float waitTime = 10;
    float time = 0;
    [SerializeField] MonsterBehaviourTreeComponent monstre;
    void Update()
    {
        //Debug.Log(time.ToString());
        time += Time.deltaTime;
        if (time >= waitTime)
        {
            monstre.SwitchWing(cible);
            time = 0;
        }
    }
}
