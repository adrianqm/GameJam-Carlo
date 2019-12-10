using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bola : MonoBehaviour
{
    public float damage = 5;
    public ParticleSystem particle;

    void OnCollisionEnter(Collision col){
        
        if(col.gameObject.CompareTag("Zombie")){
            col.gameObject.GetComponent<Zombie>().Hit(damage);
        }

        ParticleSystem particleInstance = Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(particleInstance.gameObject, particle.main.duration);
        Destroy(gameObject);
    }
}
