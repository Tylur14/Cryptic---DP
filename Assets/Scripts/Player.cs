using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, Killable
{
    [SerializeField] private Animator animator;
    [SerializeField] private SuperSimple2dCharacterMotor motor;
    [SerializeField] private ParticleSystem deathFX;
    [SerializeField] private bool isDead;

    public void TakeDamage()
    {
        if (isDead) return;
        isDead = true;
        print("Taking damage");
        GameEvents.OnSetPlayerInputLock.Raise(true);
        Invoke("Die",1);
        deathFX.Play();
        animator.SetBool("isDead", true);
    }
    
    public void Die()
    {
        isDead = false;
        GameEvents.OnSetPlayerInputLock.Raise(false);
        animator.SetBool("isDead", false);
        GameEvents.OnPlayerDeath.Raise(motor);
    }
}

