using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robots_SendAListOfPoints : MonoBehaviour
{
    public GameObject listOfPointsParent;
    public GameObject robotTarget_WorldSpace;
    public RobotController_Kinematics controllerRobot;

    //simulation stuff
    private int currentPoint;
    private bool startSimulation;
    private bool newPoint;
    private float lerpPosStartTime;
    public float timeToLerp = 0.5f;
    private Vector3 lerpStartPosition;
    private Quaternion lerpStartRotation;
    private Vector3 lerpTargetPosition;
    private Quaternion lerpTargetRotation;


    public void SendListOfPoints()
    {
        for (int i = 0; i < listOfPointsParent.transform.childCount; i++)
        {
            robotTarget_WorldSpace.transform.position = listOfPointsParent.transform.GetChild(i).position;
            robotTarget_WorldSpace.transform.rotation = listOfPointsParent.transform.GetChild(i).rotation;

            controllerRobot.RobotTarget_RobotSpace.position = robotTarget_WorldSpace.transform.position;//set position in world to match the robot target
                                                                                                        //RobotTarget_RobotSpace.localPosition = RobotTarget_RobotSpace.localPosition + new Vector3(TCPDifferencePosition.x, TCPDifferencePosition.y, TCPDifferencePosition.z);

            controllerRobot.RobotTarget_RobotSpace.rotation = robotTarget_WorldSpace.transform.rotation;//set position in world to match the robot target

            //send to machina...
            //GetComponent<Robots_SendInstructionToMaster>().SendToMaster();
            GetComponent<MachinaActionController>().GoToRobotTarget();
        }

    }
    
    
    
    
    
    public void Toggle_SimulateListOfPoints()
    {
        if(startSimulation == true)
        {
            startSimulation = false; 
        }
        else
        {
            startSimulation = true;
            newPoint = true;
         
        }
    }


    public void StepBack()
    {
        currentPoint--;

        robotTarget_WorldSpace.transform.position = listOfPointsParent.transform.GetChild(currentPoint).position;
        robotTarget_WorldSpace.transform.rotation = listOfPointsParent.transform.GetChild(currentPoint).rotation;
    }

    public void StepForward()
    {
        currentPoint++;

        robotTarget_WorldSpace.transform.position = listOfPointsParent.transform.GetChild(currentPoint).position;
        robotTarget_WorldSpace.transform.rotation = listOfPointsParent.transform.GetChild(currentPoint).rotation;
    }

    private void Update()
    {
        if (startSimulation)
        {
            int childCount = listOfPointsParent.transform.childCount;

            if (newPoint)
            {
                //set start conditions 
                lerpStartPosition = robotTarget_WorldSpace.transform.position;
                lerpTargetPosition = listOfPointsParent.transform.GetChild(currentPoint).position;
                lerpStartRotation = robotTarget_WorldSpace.transform.rotation;
                lerpTargetRotation = listOfPointsParent.transform.GetChild(currentPoint).rotation;
                lerpPosStartTime = Time.time;
                newPoint = false;
            }

            float percentageTimeElapsed = (Time.time - lerpPosStartTime) / timeToLerp;

            // Set our position as a fraction of the distance between the markers.
            robotTarget_WorldSpace.transform.position = Vector3.Lerp(lerpStartPosition, lerpTargetPosition, percentageTimeElapsed);
            float posDiffNew = Vector3.Distance(lerpTargetPosition, robotTarget_WorldSpace.transform.position);
            robotTarget_WorldSpace.transform.rotation = Quaternion.Lerp(lerpStartRotation, lerpTargetRotation, percentageTimeElapsed);

            if (posDiffNew < 0.01f)
            {
                currentPoint++;
                newPoint = true;
                if (currentPoint > childCount-1)
                {
                    currentPoint = 0;
                    startSimulation = false;
                }
            }
        }
    }
}
