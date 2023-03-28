using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBasedOnHeight : MonoBehaviour
{
    public Transform referencePlane;
    public Transform pointParentToColor;
    private MeshRenderer[] pointsToColor;


    // Start is called before the first frame update
    void Start()
    {
        pointsToColor = pointParentToColor.GetComponentsInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        for(int i = 0; i<pointsToColor.Length;i++)
        {
            float planeHeight = referencePlane.position.y;
            float sphereHeight = pointsToColor[i].transform.position.y;

            if (sphereHeight < planeHeight)
            {
                Material originalMat = pointsToColor[i].GetComponent<MeshRenderer>().material;
                Color originalColor = originalMat.color;
                originalColor.r = originalColor.r - 0.01f;

                pointsToColor[i].GetComponent<MeshRenderer>().material.color = originalColor;
            }
        }

       
    }
}
