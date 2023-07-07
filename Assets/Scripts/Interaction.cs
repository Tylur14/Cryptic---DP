using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{
    [SerializeField] private  bool consumeRequiredItem;
    [SerializeField] private  string requiredItem;
    [SerializeField, TextArea] private  string successDialogue;
    [SerializeField] private InputPrompt prompt;
    [SerializeField] private Transform itemSpawnPoint;
    [SerializeField] private  GameObject resultingObject;
    [SerializeField] private  UnityEvent onInteract;

    public Inventory closestInventory;
    
    protected virtual void OnEnable()
    {
        GameEvents.OnInteract += delegate
        {
            if (closestInventory != null)
            {
                if(prompt != null)
                    prompt.Show();
            }
        };
    }

    protected virtual void OnDisable()
    {
        GameEvents.OnInteract -= delegate
        {
            if (closestInventory != null)
            {
                if(prompt != null)
                    prompt.Show();
            }
        };
    }

    private void TryInteract()
    {
        if (closestInventory == null) return;
        
        if (!string.IsNullOrEmpty(requiredItem))
        {
            // if (closestInventory.heldItem == null)
            // {
            //     if(!string.IsNullOrEmpty(hintDialogue))
            //         GameEvents.OnDisplayDialogueWindow.Raise(hintDialogue);
            //     return;
            // }
            // if (closestInventory.heldItem.ItemName != requiredItem)
            // {
            //     GameEvents.OnDisplayDialogueWindow.Raise("Nothing happens...");
            //     return;
            // }
            if (consumeRequiredItem)
            {
                Destroy(closestInventory.heldItem.gameObject);
                closestInventory.heldItem = null;
            }
        }
        
        if (resultingObject != null)
        {
            Instantiate(resultingObject, itemSpawnPoint.position, Quaternion.identity);
        }
        
        if(!string.IsNullOrEmpty(successDialogue))
            GameEvents.OnDisplayDialogueWindow.Raise(successDialogue);
        
        onInteract.Invoke();
        GameEvents.OnInteract.Raise();
        closestInventory = null;
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Inventory>() != null && closestInventory == null)
        {
            closestInventory = other.GetComponent<Inventory>();
            if (!string.IsNullOrEmpty(requiredItem))
            {
                if (closestInventory.heldItem == null)
                {
                    closestInventory = null;
                    return;
                }
                if (closestInventory.heldItem.ItemName != requiredItem)
                {
                    closestInventory = null;
                    return;
                }
            }
            GameEvents.OnInteractButtonDown += TryInteract;
            if(prompt != null)
                prompt.Show();
        }
        else if(closestInventory != null && (closestInventory.heldItem == null || closestInventory.heldItem.ItemName != requiredItem))
        {
            closestInventory = null;
            GameEvents.OnInteractButtonDown -= TryInteract;
            if(prompt != null)
                prompt.Hide();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Inventory>() != null)
        {
            if (other.GetComponent<Inventory>() == closestInventory)
            {
                GameEvents.OnInteractButtonDown -= TryInteract;
                closestInventory = null;
                if(prompt != null)
                    prompt.Hide();
            }
        }
    }
}
