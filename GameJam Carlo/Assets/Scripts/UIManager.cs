using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI zombiesRemaining;
    public TextMeshProUGUI score;
    public TextMeshProUGUI health;

    void Start()
    {
        GameManager.OnStatsUpdate += OnStatsUpdate;
        PlayerManager.OnPlayerStatsUpdate += OnPlayerStatsUpdate;
        
        UpdateZombiesRemaining();
        UpdateScore();
        UpdateHealth();
    }

    void OnStatsUpdate()
    {
        UpdateZombiesRemaining();
        UpdateScore();
    }

    void OnPlayerStatsUpdate()
    {
        UpdateHealth();
    }

    void UpdateZombiesRemaining(){
        zombiesRemaining.text = GameManager.instance.GetZombiesAlive().ToString();
    }

    void UpdateScore(){
        score.text = "Score: " + GameManager.instance.GetScore().ToString();
    }

    void UpdateHealth(){
        health.text = PlayerManager.instance.health.ToString();
    }
}
