using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public delegate void ZombieDelegate(Zombie zombie);
    public static event ZombieDelegate OnZombieDeath;
    public static event ZombieDelegate OnDamagePlayer;
    private GameObject child;
    private SkinnedMeshRenderer zombie_mesh;

    public AudioSource deadSound;


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

    void Start(){
        child =  this.transform.GetChild(1).gameObject;
        zombie_mesh = child.GetComponent<SkinnedMeshRenderer>();
    }

    void Update(){
        if(!animator.GetBool("isAlive")){
            changeToTransparent(zombie_mesh.material);
            Color color = zombie_mesh.material.color;
            color.a -= Time.deltaTime * 5.0f;
            zombie_mesh.material.color = color;
        }
    }

    public void changeToTransparent(Material standardShaderMaterial){
        standardShaderMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        standardShaderMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        standardShaderMaterial.SetInt("_ZWrite", 0);
        standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
        standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
        standardShaderMaterial.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        standardShaderMaterial.renderQueue = 3000;
    }

    public void Hit(float damage){
        health -= damage;
        if(health <= 0)
            Die();
    }

    void Die(){
        animator.SetBool("isAlive", false);
        deadSound.Play();
        GetComponent<Collider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        OnZombieDeath.Invoke(this);
    }

    void OnTriggerStay(Collider col){
        if(canHit && col.gameObject.CompareTag("Player")){
           animator.SetBool("isAttacking", true); 
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
