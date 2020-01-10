using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    public float rotationVelocity = 4.0f;

    void Start(){
        InputManager.OnTouchDown += StartRotatePlayer;
    }

    void StartRotatePlayer(Vector3 pos, Quaternion targetRotation){
        StopAllCoroutines();
        StartCoroutine(RotatePlayer(pos, targetRotation));
    }

    IEnumerator RotatePlayer(Vector3 pos, Quaternion targetRotation){
        while(Quaternion.Angle(transform.rotation, targetRotation) > 2){
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime*rotationVelocity);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            yield return new WaitForEndOfFrame();
        }
    }
}
