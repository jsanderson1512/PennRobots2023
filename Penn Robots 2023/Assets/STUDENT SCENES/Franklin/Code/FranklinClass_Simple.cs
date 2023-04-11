using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Intel.RealSense;
using WebSocketSharp;
using System;
//using Rhino.Geometry;


namespace FranklinUnityStuff {
    public class FranklinClass_Simple : MonoBehaviour
    {
        public Transform theThingToMove;
        public Transform whereToMoveTo;
        public float howFarInFront = 0.2f;

        private bool DrawingOn = false;
        public float distTolerance;
        private Vector3 lastPlotPosition;
        private float currentDistance;
        

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Start function print once!");
        }

        // Update is called once per frame
        void Update()
        {
            //Debug.Log("Update function print every frame!" + Time.time);
            if (DrawingOn)
            {
                Debug.Log("Drawing is On");

                currentDistance = Vector3.Distance(lastPlotPosition, whereToMoveTo.position);

                if (currentDistance < distTolerance)
                {
                    Vector3 editedPosition = whereToMoveTo.position;
                    editedPosition += (whereToMoveTo.forward * howFarInFront);

                    // plot some point
                    GameObject plotThisThing = Instantiate(theThingToMove.gameObject, editedPosition, whereToMoveTo.rotation);

                    // set last plot position to current position\

                    plotThisThing.transform.RotateAround(theThingToMove.transform.position, theThingToMove.transform.right, 90);
                }
            }
        }

 
        public void StartDrawing() { 
            DrawingOn= true;
        }

        public void StopDrawing()
        {
            DrawingOn= false;
        }

        public void BringTargetToMe()
        {
            // This script that will take a gameobject and move its position and rotation to another gameobject on click
            Debug.Log("I called BringTargetToMe");

            Vector3 editedPosition = whereToMoveTo.position;
            editedPosition += (whereToMoveTo.forward * howFarInFront);

            theThingToMove.position = editedPosition;

            theThingToMove.rotation = whereToMoveTo.rotation;

            theThingToMove.transform.RotateAround(theThingToMove.transform.position, theThingToMove.transform.right, 90);
        }

    }
}

