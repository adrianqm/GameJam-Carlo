using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowController : MonoBehaviour
{
    public GameObject ball;
    public Transform spawnPoint;
    private Animator animator;
    public Animator slingShoot_anim;

    public float force = 30; 

    void Awake(){
        animator = GetComponent<Animator>();
    }

    void Start(){
        InputManager.OnTouchDown += StartThrowBall;
    }

    public void StartThrowBall(Vector3 pos, Quaternion targetRotation){
        if(!animator.GetBool("isThrowing"))
            animator.SetBool("isThrowing", true);
    }

    public void ThrowBall(){
        GameObject bullet = Instantiate(ball, spawnPoint.position, InputManager.instance.targetRotation) as GameObject;
        bullet.GetComponent<Rigidbody>().AddForce(InputManager.instance.pos.normalized * force, ForceMode.Impulse);
    }
}
