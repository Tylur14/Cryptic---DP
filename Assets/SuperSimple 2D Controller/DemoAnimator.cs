using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoAnimator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SuperSimple2dCharacterMotor attachedMotor;
    [SerializeField] private Animator animator;
    [SerializeField] private bool flipEffect;
    [SerializeField] private float flipSpeed;

    private bool cachedFallingState;
    private float targetScale = 1f;
    
    private void Update()
    {
        bool isFalling = !attachedMotor.isGrounded;
        float xVel = attachedMotor.velocity.x;
        
        if (attachedMotor.isMoving)
        {
            
            if (!flipEffect)
                spriteRenderer.flipX = xVel < 0;
            else targetScale = xVel > 0 ? 1 : -1;
        }

        if (isFalling != cachedFallingState)
        {
            if(isFalling)
                animator.SetTrigger("startFall");    
            cachedFallingState = isFalling;
        }
        
        animator.SetBool("isMoving",attachedMotor.isMoving);
        animator.SetBool("isFalling",isFalling);

        

        if (flipEffect)
        {
            var scale = transform.localScale;
            scale.x = Mathf.Lerp(scale.x, targetScale, flipSpeed * Time.deltaTime);
            transform.localScale = scale;
        }
    }
}
