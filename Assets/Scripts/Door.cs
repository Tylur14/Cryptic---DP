using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Player closestPlayer;
    [SerializeField] private Transform doorEndpoint;
    [SerializeField] private InputPrompt prompt;
    
    private void OnEnable()
    {
        GameEvents.OnInteract += delegate
        {
            if (closestPlayer != null)
            {
                closestPlayer.gameObject.transform.position = doorEndpoint.position;
            }
        };
    }
    private void OnDisable()
    {
        GameEvents.OnInteract -= delegate
        {
            if (closestPlayer != null)
            {
                closestPlayer.gameObject.transform.position = doorEndpoint.position;
            }
        };
    }
    
    private void TryInteract()
    {
        if (closestPlayer == null) return;
        GameEvents.OnInteract.Raise();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null && closestPlayer == null)
        {
            closestPlayer = other.GetComponent<Player>();
            GameEvents.OnInteractButtonDown += TryInteract;
            if(prompt != null)
                prompt.Show();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Inventory>() != null)
        {
            if (other.GetComponent<Player>() == closestPlayer)
            {
                GameEvents.OnInteractButtonDown -= TryInteract;
                closestPlayer = null;
                if(prompt != null)
                    prompt.Hide();
            }
        }
    }
}
