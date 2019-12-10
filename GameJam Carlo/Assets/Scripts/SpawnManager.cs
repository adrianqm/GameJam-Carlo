using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{    
    public static SpawnManager instance;

    public int sizeWave = 5;
    public float spawnDelay = 2;
    public float radio = 10;
    
    public GameObject zombie;

    void Awake(){
        if(instance == null)
            instance = this;
    }

    void Start()
    {
        StartCoroutine("SpawnWave");
    }

    IEnumerator SpawnWave(){
        int zombiesRemaining = sizeWave;
        while(zombiesRemaining > 0){
            Instantiate(zombie, RandomPosition(), Quaternion.identity);
            zombiesRemaining--;
            yield return new WaitForSeconds(spawnDelay);
        }
    }
    
    Vector3 RandomPosition(){
        float angle = Random.Range(0, 360);
        return GetPositionFromAngle(angle);
    }

    Vector3 GetPositionFromAngle(float angle){
        float x = radio * Mathf.Cos(angle);
        float z = radio * Mathf.Sin(angle);

        return new Vector3(x, 0, z);
    }
}
