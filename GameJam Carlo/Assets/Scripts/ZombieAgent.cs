using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAgent : MonoBehaviour
{
    
    NavMeshAgent nav;
    Transform player;

    void Awake(){
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start(){
        nav.SetDestination(player.position);
    }

    void Update()
    {

    }
}
