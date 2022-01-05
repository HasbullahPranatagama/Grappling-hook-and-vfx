using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GraplingGunT : MonoBehaviour
{
    [SerializeField]
    private float spring = 4.5f;
    [SerializeField]
    private float damper = 7f;
    [SerializeField]
    private float massScale = 4.5f;



    private LineRenderer lr;
    public Vector3 grapplePoint;
    public LayerMask whatIsGrappable;

    public Transform gunTip;
    public Transform camera;
    public Transform player;
    [SerializeField]
    private float maxDistance = 100f;

    public SpringJoint joint;


    public bool isMainManuver;
    public UnityEvent eventMainManuver1;
    public UnityEvent eventMainManuver2;

    public UnityEvent offEventMainManuver1;
    public UnityEvent offEventMainManuver2;


    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {



        if (Input.GetMouseButtonDown(0))
        {
            eventMainManuver1.Invoke();
            StartGrapple();
        }
        if (Input.GetMouseButtonUp(0))
        {
            offEventMainManuver1.Invoke();
            StopGrapple();
        }
        if (Input.GetMouseButtonDown(1))
        {
            eventMainManuver2.Invoke();

            StartGrapple();
        }

        if (Input.GetMouseButtonUp(1))
        {
            offEventMainManuver2.Invoke();
            StopGrapple();

        }

    }

    private void LateUpdate()
    {
        DrawRope();

    }


    private void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappable))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);


            //the distance grapple will try to keep from grapple point
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = spring;
            joint.damper = damper;
            joint.massScale = massScale;

            lr.positionCount = 2;
        } 
          
    }

    public void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);

    }



    private void DrawRope()
    {
        // not drawing anything if there are no joint or not grapling
        if (!joint) return;

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, grapplePoint);
    }


    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }

    public void IsMainManuver(bool active)
    {
        isMainManuver = true;
    }

}
