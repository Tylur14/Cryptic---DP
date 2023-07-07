using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private float followSpeed;
    
    public HoldableItem heldItem;
    public HoldableItem storedItem;

    [SerializeField] private InputPrompt storedItemSwapPrompt;
    
    [SerializeField] private Transform heldItemPosition;
    [SerializeField] private Transform storedItemPosition;

    private void OnEnable()
    {
        GameEvents.OnSwapButtonDown += SwapItems;
        GameEvents.OnDropButtonDown += DropItem;
    }

    private void OnDisable()
    {
        GameEvents.OnSwapButtonDown -= SwapItems;
        GameEvents.OnDropButtonDown -= DropItem;
    }

    private void Update()
    {
        AnimateItemFollow();
    }

    public void AnimateItemFollow()
    {
        if (heldItem != null)
        {
            //heldItem.transform.position = Vector3.Lerp(heldItem.transform.position,heldItemPosition.position,followSpeed * Time.deltaTime);
            heldItem.transform.position = heldItemPosition.position;
        }
        if (storedItem != null)
        {
            if(!storedItemSwapPrompt.isActive)
                storedItemSwapPrompt.Show();
            // storedItem.transform.position = Vector3.Lerp(storedItem.transform.position,storedItemPosition.position,followSpeed * Time.deltaTime);
            storedItem.transform.position = storedItemPosition.position;
        }
        else
        {
            if(storedItemSwapPrompt.isActive)
                storedItemSwapPrompt.Hide(); 
        }
    }

    public void SwapItems()
    {
        (heldItem, storedItem) = (storedItem, heldItem);
    }

    public void DropItem()
    {
        if (heldItem == null) return;
        heldItem.TogglePickedUp(false);
        heldItem = null;
    }
    
    public bool TryPickup(HoldableItem incomingItem)
    {
        if (heldItem == null && storedItem != incomingItem)
        {
            heldItem = incomingItem;
            heldItem.TogglePickedUp(true);
            return true;
        }

        if (storedItem == null && heldItem != incomingItem)
        {
            storedItem = incomingItem;
            storedItem.TogglePickedUp(true);
            return true;
        }

        return false;
    }
}
