using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public GameObject bola;
    public Transform lanzador_bola;
    private Vector3 pos;
    private Quaternion targetRotation;
    void Start(){
        pos  = transform.position;
        targetRotation = transform.rotation;
    }

    void Update()
    {
        

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){

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
                GameObject bullet = Instantiate(bola, lanzador_bola.position, targetRotation) as GameObject;
                bullet.GetComponent<Rigidbody>().AddForce(pos*100);

            }
        }       
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime*2.0f); 
    }
}
