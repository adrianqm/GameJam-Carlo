using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public delegate void PlayerDelegate();
    public static event PlayerDelegate OnPlayerStatsUpdate;

    public Image healthBar;
    private float startHealth;
    public int health;
    
    void Awake(){
        if(instance == null)
            instance = this;
    }

    void Start(){
        Zombie.OnDamagePlayer += OnDamagePlayer;
        startHealth = health;
    }
    
    void OnDamagePlayer(Zombie zombie){
        Hit(zombie.damage);
    }

    void Hit(int damage){
        health -= damage;
        healthBar.fillAmount = health / startHealth;
        Debug.Log(healthBar.fillAmount);
        if(health <= 0){
            Die();
            health = 0;
        }
        OnPlayerStatsUpdate.Invoke();
    }

    void Die(){
        Destroy(gameObject);
    }
}
