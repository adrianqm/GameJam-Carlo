using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public GameObject bola;
    public Transform lanzador_bola;

    Animator playerAnimator;

    public float rotationVelocity = 4.0f;
    private Vector3 pos;
    private Quaternion targetRotation;

    public float timeBetweenBall = 2;
    private float timeBallCounter = 0;
    
    void Start(){
        pos  = transform.position;
        targetRotation = transform.rotation;
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        timeBallCounter += Time.deltaTime;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){
            if(timeBallCounter > timeBetweenBall){
                playerAnimator.SetBool("isThrowing",true);
                timeBallCounter = 0;
            }
            // create ray from the camera and passing through the touch position:
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            // create a logical plane at this object's position
            // and perpendicular to world Y:
            Plane plane = new Plane(Vector3.up, transform.position);
            float distance = 0; // this will return the distance from the camera
            if (plane.Raycast(ray, out distance)){ // if plane hit...
                pos = ray.GetPoint(distance); // get the point
                // pos has the position in the plane you've touched
                //Instantiate(bola, pos, Quaternion.identity);
                targetRotation = Quaternion.LookRotation(pos - transform.position, Vector3.up);
                
            }
        }     
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime*rotationVelocity);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    void FixedUpdate(){
        if(playerAnimator.GetBool("isThrowing") 
            && (playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
            && playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Throw Object")){
            GameObject bullet = Instantiate(bola, lanzador_bola.position, targetRotation) as GameObject;
            bullet.GetComponent<Rigidbody>().AddForce(pos*100);
            playerAnimator.SetBool("isThrowing",false);      
        }
    }
}
