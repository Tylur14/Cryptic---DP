using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldableItem : MonoBehaviour
{
    public string ItemName;
    public SuperSimple2dCharacterMotor motor;
    public ParticleSystem sparkles;
    public Inventory closestInventory;

    private void OnEnable()
    {
        GameEvents.OnPickupButtonDown += delegate
        {
            if (closestInventory == null) return;
            closestInventory.TryPickup(this);
        };
    }

    private void OnDisable()
    {
        GameEvents.OnPickupButtonDown -= delegate
        {
            if (closestInventory == null) return;
            closestInventory.TryPickup(this);
        };
    }

    public void TogglePickedUp(bool state)
    {
        motor.enabled = !state;
        if (state)
        {
            sparkles.Stop();
        }
        else sparkles.Play();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Inventory>() != null)
        {
            closestInventory = other.GetComponent<Inventory>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Inventory>() != null)
        {
            if (other.GetComponent<Inventory>() == closestInventory)
            {
                closestInventory = null;
            }
        }
    }
}
