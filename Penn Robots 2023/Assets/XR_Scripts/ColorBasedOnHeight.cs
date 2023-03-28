using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBasedOnHeight : MonoBehaviour
{
    public Transform referencePlane;
    public Transform sphereToColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float planeHeight = referencePlane.position.y;
        float sphereHeight = sphereToColor.position.y;

        if(sphereHeight<planeHeight)
        {
            Material originalMat = sphereToColor.GetComponent<MeshRenderer>().material;
            Color originalColor = originalMat.color;
            originalColor.r = originalColor.r - 1;

            sphereToColor.GetComponent<MeshRenderer>().material.color = originalColor;
        }
    }
}
