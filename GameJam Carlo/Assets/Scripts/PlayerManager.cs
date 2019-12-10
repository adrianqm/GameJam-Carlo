using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public delegate void PlayerDelegate();
    public static event PlayerDelegate OnPlayerStatsUpdate;
    public int health = 10;
    
    void Awake(){
        if(instance == null)
            instance = this;
    }

    void Start(){
        Zombie.OnDamagePlayer += OnDamagePlayer;
    }
    
    void OnDamagePlayer(Zombie zombie){
        Hit(zombie.damage);
    }

    void Hit(int damage){
        health -= damage;
        OnPlayerStatsUpdate.Invoke();

        if(health <= 0)
            Die();
    }

    void Die(){
        Destroy(gameObject);
    }
}
