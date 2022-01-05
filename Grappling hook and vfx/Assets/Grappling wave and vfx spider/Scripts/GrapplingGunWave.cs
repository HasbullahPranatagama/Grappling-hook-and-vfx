using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGunWave : MonoBehaviour
{
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, camera, player;
    [SerializeField]
    private float maxDistance = 100f;
    private SpringJoint joint;

    [SerializeField]
    private float spring = 4.5f;
    [SerializeField]
    private float damper = 7f;
    [SerializeField]
    private float massScale = 4.5f;



    public bool isSidegun = false;

    void Update()
    {
        if(isSidegun == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartGrapple();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                StopGrapple();
            }
        }

        if(isSidegun == true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                StartGrapple();
            }
            else if (Input.GetMouseButtonUp(1))
            {
                StopGrapple();
            }
        }


    }

    //Called after Update
 
    /// <summary>
    /// Call whenever we want to start a grapple
    /// </summary>
    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            //Adjust these values to fit your game.
            joint.spring = spring;
            joint.damper = damper;
            joint.massScale = massScale;

         }
    }


    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    void StopGrapple()
    {
        Destroy(joint);
    }

 
    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}
