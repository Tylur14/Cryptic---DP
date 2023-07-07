using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float bounceForce;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<SuperSimple2dCharacterMotor>())
        {
            var motor = other.GetComponent<SuperSimple2dCharacterMotor>();
            motor.velocity.y += bounceForce;
        }
    }
}
