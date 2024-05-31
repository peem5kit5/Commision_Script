using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Transform theCam;

    public Transform sky , treeline ;
    [Range(0f,1f)]
    public float ParallaxSpeed;


    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        sky.position = new Vector3(theCam.position.x,theCam.position.y, sky.position.z);

        treeline.position = new Vector3(
            theCam.position.x * ParallaxSpeed,
            theCam.position.y *ParallaxSpeed,
            treeline.position.z);
    }
}
