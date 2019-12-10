using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public delegate void SpawnDelegate();
    public static event SpawnDelegate OnStatsUpdate;

    int score;
    int zombiesAlive;


    void Awake(){
        if(instance == null)
            instance = this;
    }

    void Start(){
        Zombie.OnZombieDeath += OnZombieDeath;

        score = 0;
        zombiesAlive = SpawnManager.instance.sizeWave;
    }

    void OnZombieDeath(Zombie zombie){
        AddPoints(zombie.points);
        SubZombiesAlive();
        OnStatsUpdate.Invoke();
    }

    void AddPoints(int points){
        score += points;
    }

    void SubZombiesAlive(){
        zombiesAlive--;
    }

    public int GetScore(){
        return score;
    }

    public int GetZombiesAlive()
    {
        return zombiesAlive;
    }
}
