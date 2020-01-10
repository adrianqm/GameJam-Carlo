using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public delegate void OnTouchDownDelegate(Vector3 pos, Quaternion targetRotation);
    public static event OnTouchDownDelegate OnTouchDown; 

    private Transform player;
    public Transform slingShoot;
    public bool touchDown;

    public Vector3 pos;
    public Quaternion targetRotation;

    void Awake(){
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start(){
        player = FindObjectOfType<PlayerManager>().transform;

        pos = player.position;
        targetRotation = player.rotation;
    }

    void Update()
    {
         if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){
            CalculateTouchPosition();
            OnTouchDown?.Invoke(pos, targetRotation);
         }
    }

    void CalculateTouchPosition(){
        // create ray from the camera and passing through the touch position:
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        // create a logical plane at this object's position
        // and perpendicular to world Y:
        Plane plane = new Plane(Vector3.up, player.position);
        float distance = 0; // this will return the distance from the camera
        if (plane.Raycast(ray, out distance)){ // if plane hit...
            pos = ray.GetPoint(distance); // get the point
            // pos has the position in the plane you've touched
            //Instantiate(bola, pos, Quaternion.identity);
            targetRotation = Quaternion.LookRotation(pos - slingShoot.position, Vector3.up);
        }
    }
}
