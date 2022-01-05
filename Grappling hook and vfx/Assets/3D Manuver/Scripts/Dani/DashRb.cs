using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashRb : MonoBehaviour
{

    public float dashSpeed;
    public float dashSpeedTitan;


    Rigidbody rig;
    bool isDashing;
    [SerializeField]
    bool isTitan;
    [SerializeField]
    bool isTitanDash;

    public GraplingGunT grapleGun1;
    public GraplingGunT grapleGun2;

    public GameObject direction;

    public GameObject dashEffect;


    private void Start()
    {
        rig = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) & !isTitan)
            isDashing = true;
        if (Input.GetKeyDown(KeyCode.Z))
            isTitanDash = true;
        if (Input.GetKeyUp(KeyCode.Z))
            isTitanDash = false;
    }


    private void FixedUpdate()
    {
        if (isDashing)
            Dashing();
        if (isTitanDash)
            DashingToTitan();
    }



    private void Dashing()
    {
        rig.AddForce(direction.transform.forward * dashSpeed, ForceMode.Impulse );
        isDashing = false;
        
        GameObject effect = Instantiate(dashEffect, Camera.main.transform.position, dashEffect.transform.rotation);

        effect.transform.parent = Camera.main.transform;
        effect.transform.LookAt(transform);
    }

    private void DashingToTitan()
    {
        //Destroy(grapleGun1.joint);
        //Destroy(grapleGun2.joint);

        rig.AddForce(grapleGun1.grapplePoint * dashSpeedTitan );
        rig.AddForce(grapleGun2.grapplePoint * dashSpeedTitan );

        isTitanDash = false;

        GameObject effect = Instantiate(dashEffect, Camera.main.transform.position, dashEffect.transform.rotation);

        effect.transform.parent = Camera.main.transform;
        effect.transform.LookAt(transform);



    }

}
