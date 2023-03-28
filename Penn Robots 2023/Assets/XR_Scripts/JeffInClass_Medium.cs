using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeffInClass_Medium : MonoBehaviour
{
    public GameObject thingToMove;
    public GameObject thingControlleringMotion;


    private Quaternion startRotationController;
    private Quaternion startRotationTarget;

    private Quaternion currentRotation;

    private bool rotationIsGoing = false;


    public void StartRotation()
    {
        //here is my function to stop 
        startRotationController = thingControlleringMotion.transform.rotation;
        startRotationTarget = thingToMove.transform.rotation;

        rotationIsGoing = true;
    }
    public void StopRotation()
    {
        //here is my function to stop

        rotationIsGoing = false;
        
    }



    void Update()
    {
        if(rotationIsGoing)
        {
            //figure out how to get the difference and apply it...

            Quaternion rotationDifference = thingControlleringMotion.transform.rotation * Quaternion.Inverse(startRotationController);
            Quaternion finalRotation = startRotationTarget * rotationDifference;

            thingToMove.transform.rotation = finalRotation;


            Debug.Log("rotation is going");
        }
    }
}
