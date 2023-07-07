using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Killable>() != null)
        {
            other.GetComponent<Killable>().TakeDamage();
        }
    }
}
