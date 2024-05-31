using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class PFCameraController : MonoBehaviour
{
    public Transform target;

    public bool freezeVertical,freezeHorizontal;
    private Vector3 positionStore;

    public bool clampPosition;
    public Transform clampMin, clampMax;
    private float halfwidth, halfheight;
    public Camera theCam;

    
    // Start is called before the first frame update
    void Start()
    {
        positionStore = transform.position;

        clampMin.SetParent(null);
        clampMax.SetParent(null);

        halfheight = theCam.orthographicSize;
        halfwidth = theCam.orthographicSize * theCam.aspect;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y,transform.position.z);

        if(freezeVertical == true)
        {
            transform.position = new Vector3(transform.position.x,positionStore.y,transform.position.z);
        }
        if(freezeHorizontal == true)
        {
            transform.position = new Vector3(positionStore.x,transform.position.y,transform.position.z);
        }

        if(clampPosition == true)
        {
            transform.position = new Vector3( 
                Mathf.Clamp(transform.position.x,clampMin.position.x + halfwidth,clampMax.position.x - halfwidth),
                Mathf.Clamp(transform.position.x,clampMin.position.y + halfheight,clampMax.position.y - halfheight),
                transform.position.z);
        }
    }

    private void OnDrawGizmos() 
    {
        if(clampPosition == true)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(clampMin.position,new Vector3(clampMin.position.x,clampMax.position.y,0f));
            Gizmos.DrawLine(clampMin.position,new Vector3(clampMax.position.x,clampMin.position.y,0f));

            Gizmos.DrawLine(clampMax.position,new Vector3(clampMin.position.x,clampMax.position.y,0f));
            Gizmos.DrawLine(clampMax.position,new Vector3(clampMax.position.x,clampMin.position.y,0f));
        }
        
    }
}
