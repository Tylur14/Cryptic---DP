using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SuperSimple2dCharacterMotor playerMotor;
    private bool movingLeft, movingRight;
    private float horizontalInput;
    private bool inputsLocked;
    
    private void OnEnable()
    {
        GameEvents.OnSetPlayerInputLock += TogglePlayerInputLock;
        GameEvents.OnMoveLeftButtonHeld += delegate
        {
            if(!inputsLocked)
                movingLeft = true;
        };
        GameEvents.OnMoveLeftButtonReleased += delegate { 
            if(!inputsLocked)
                movingLeft = false; 
        };
        
        GameEvents.OnMoveRightButtonHeld += delegate
        {
            if(!inputsLocked)
                movingRight = true;
        };
        GameEvents.OnMoveRightButtonReleased += delegate
        {
            if(!inputsLocked)
                movingRight = false;
        };
        
        GameEvents.OnJumpButtonHeld += delegate
        {
            if(!inputsLocked)
                playerMotor.Jump();
        };
        GameEvents.OnJumpButtonUp += delegate { 
            if(!inputsLocked)
                playerMotor.StopJump(); 
        };
    }

    private void OnDisable()
    {
        GameEvents.OnSetPlayerInputLock -= TogglePlayerInputLock;
        GameEvents.OnMoveLeftButtonHeld -= delegate
        {
            if(!inputsLocked)
                movingLeft = true;
        };
        GameEvents.OnMoveLeftButtonReleased -= delegate { 
            if(!inputsLocked)
                movingLeft = false; 
        };
        
        GameEvents.OnMoveRightButtonHeld -= delegate
        {
            if(!inputsLocked)
                movingRight = true;
        };
        GameEvents.OnMoveRightButtonReleased -= delegate
        {
            if(!inputsLocked)
                movingRight = false;
        };
        
        GameEvents.OnJumpButtonHeld -= delegate
        {
            if(!inputsLocked)
                playerMotor.Jump();
        };
        GameEvents.OnJumpButtonUp -= delegate { 
            if(!inputsLocked)
                playerMotor.StopJump(); 
        };
    }

    public void TogglePlayerInputLock(bool state)
    {
        inputsLocked = state;
    }

    void Update()
    {
        if (inputsLocked)
        {
            horizontalInput = 0;
            return;
        }
        HandleInputs();
    }
    
    private void HandleInputs()
    {
        horizontalInput = 0;
        if (movingLeft)
            horizontalInput -= 1;
        if (movingRight)
            horizontalInput += 1;
        playerMotor.AddMoveForce(new Vector2(horizontalInput,0));
    }
}
