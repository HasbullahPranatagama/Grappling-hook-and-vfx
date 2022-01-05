using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerLocomotion playerLocomotion;
    AnimatorManager animatorManager;


    public Vector2 movementInput;
    public float moveAmount;


    public bool isJump_Input;
       
    public float verticalInput;
    public float horizontalInput;


    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();

    }


    public void HandleAllInput()
    {
        HandleMovementInput();
        HandleJumpInput();
        //handledash input
        //handle action input
    }


    private void HandleMovementInput()
    {
        movementInput.y = Input.GetAxisRaw("Vertical");
        movementInput.x = Input.GetAxisRaw("Horizontal");

        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        #region animator

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));// di kalikan matf.abs supaya hasil angkanya selalu positif
        animatorManager.UpdateAnimatorValues(0, moveAmount);

        #endregion
    }

    private void HandleJumpInput()
    {
        if(playerLocomotion.isGround)
        if (Input.GetKeyDown(KeyCode.Space))        
            isJump_Input = true;        
    }
}
