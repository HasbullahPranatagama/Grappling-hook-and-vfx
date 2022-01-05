using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    InputManager inputManager;

    Vector3 moveDirection;
    Transform cameraObject;

    Rigidbody playerRigidBody;

    public float movementSpeed = 7;
    public float maxMoveSpeedMag = 4500;
    public float rotationSpeed = 15;
    public float jumpForce = 550f;
    public float gravityValue = -2;
    public float gravityInAirValue = -9;

    public bool isGround;
    private bool isJump;


    public Transform groundCheck;               //ground check refference untuk posisi check sphere
    public float groundDistance = 0.4f;         //radius sensor (check sphere) untuk deteksinya       
    public LayerMask groundMask;                // layer mask untuk check ground


    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRigidBody = GetComponent<Rigidbody>();

        cameraObject = Camera.main.transform;// memasukkan Camera yg di tag sebagai main camera, jika ada lebih dr 1 main camera maka yg di deteksi deluan yg di assidn
    }

    public void HandleInUpdate()
    {
        IsGround();

    }

    public void HandleAllMovement()
    {

        HandleMovement();
        HandleRotation();
        HandleJump();
        LimitVelocityMag();
    }


    private void HandleMovement()
    {
        moveDirection = cameraObject.forward * inputManager.verticalInput ;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();

        moveDirection = moveDirection * movementSpeed;
        moveDirection.y = 0;


        if (isGround && playerRigidBody.velocity.y < 0)
        {
            moveDirection.y = gravityValue; 
        }
        HandleGravity();


        Vector3 movementVelocity = moveDirection + playerRigidBody.velocity;
        playerRigidBody.velocity = movementVelocity;



    }
       
    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward; // keep the forward position(rotation) so it not clamp back to first original rotation


        Quaternion targetRotation = Quaternion.LookRotation(targetDirection); //melihat ke arah target direction
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); //membalikkan badan player dari rotasi a ke rotasi b dalam kecepatan tertentu 

        transform.rotation = playerRotation;
    }

    private void LimitVelocityMag()
    {
        if(playerRigidBody.velocity.magnitude> maxMoveSpeedMag)
        {
            playerRigidBody.velocity = Vector3.ClampMagnitude(playerRigidBody.velocity, maxMoveSpeedMag);
        }
        
    }




    private void HandleGravity()
    {
        if (!isGround)
        {
            //moveDirection.y = playerRigidBody.velocity.y; // dia jatuh berdasarkan kecepatan naik keatas dari lompatan dan di tambahkan gravitasi yg bertambah dari waktu ke waktu
            //moveDirection.y += gravityInAirValue * Time.deltaTime;
            moveDirection.y += gravityInAirValue * Time.deltaTime; // dia jatuh berdasarkan kecepatan naik keatas dari lompatan dan di tambahkan gravitasi yg bertambah dari waktu ke waktu
            moveDirection.y += gravityInAirValue * Time.deltaTime;

        }
    }

    private void IsGround()
    {
        // mengecek boolean ground memakai check sphere, dengan mengambil letak posisi sensornya, berapa jauh jarak sensornya, dan layer apa saja yg bisa dijadikan untuk check booleannya
        isGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

    }


    private void HandleJump()
    {

        if (inputManager.isJump_Input & isGround)
        {
            playerRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            inputManager.isJump_Input = false;
        }
    }
}
