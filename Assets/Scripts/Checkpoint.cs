using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private static Checkpoint CurrentCheckpoint;

    [SerializeField] private bool isDefault;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private ParticleSystem effects;

    private void OnEnable()
    {
        CurrentCheckpoint = null;
        if (isDefault)
        {
            CurrentCheckpoint = this;
            ToggleCheckpoint(true);
        }
        else ToggleCheckpoint(false);
        GameEvents.OnPlayerDeath += delegate(SuperSimple2dCharacterMotor motor)
        {
            if(CurrentCheckpoint != this) return;
            CurrentCheckpoint.RespawnPlayer(motor);
        };
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerDeath -= delegate(SuperSimple2dCharacterMotor motor)
        {
            if(CurrentCheckpoint != this) return;
            CurrentCheckpoint.RespawnPlayer(motor);
        };
    }

    private void RespawnPlayer(SuperSimple2dCharacterMotor motor)
    {
        motor.velocity = Vector2.zero;
        motor.transform.position = respawnPoint.position;
    }

    private void ToggleCheckpoint(bool state)
    {
        if(state) effects.Play();
        else effects.Stop();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(CurrentCheckpoint != null && CurrentCheckpoint != this) CurrentCheckpoint.ToggleCheckpoint(false);        
            CurrentCheckpoint = this;
            CurrentCheckpoint.ToggleCheckpoint(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
    }
}
