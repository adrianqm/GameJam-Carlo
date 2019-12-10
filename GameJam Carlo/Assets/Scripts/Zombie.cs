﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public delegate void ZombieDelegate(Zombie zombie);
    public static event ZombieDelegate OnZombieDeath;
    public static event ZombieDelegate OnDamagePlayer;

    Animator animator;

    public float health = 10;
    public int damage = 2;
    public int points = 20;

    public float timeBetweenHit = 1;
    
    [SerializeField]
    bool canHit = true;

    void Awake(){
        animator = GetComponent<Animator>();
    }

    public void Hit(float damage){
        health -= damage;
        if(health <= 0)
            Die();
    }

    void Die(){
        animator.SetBool("isAlive", false);
        GetComponent<Collider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        OnZombieDeath.Invoke(this);
    }

    void OnTriggerStay(Collider col){
        Debug.Log(col.gameObject.name);
        if(canHit && col.gameObject.CompareTag("Player")){
            Debug.Log("pls");
           OnDamagePlayer.Invoke(this);
           StartCoroutine("DelayBetweenHit", timeBetweenHit);
        }
    }

    IEnumerator DelayBetweenHit(float time){
        canHit = false;
        yield return new WaitForSeconds(time);
        canHit = true;
    }   
}