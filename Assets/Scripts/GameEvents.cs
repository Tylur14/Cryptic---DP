using System.Collections;
using System.Collections.Generic;
using i302.Utils.Events;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour
{
    // Primary Mouse Button Events
    public static i302Event OnPrimaryMouseButtonDown = new();
    public static i302Event OnPrimaryMouseButtonUp = new();
    public static i302Event OnPrimaryMouseButtonHeld = new();
    public static i302Event OnPrimaryMouseButtonReleased = new();
    
    // Alternate Mouse Button Events
    public static i302Event OnAltMouseButtonUp = new();
    public static i302Event OnAltMouseButtonDown = new();
    public static i302Event OnAltMouseButtonHeld = new();
    public static i302Event OnAltMouseButtonReleased = new();
    
    // Mouse Scroll Events
    public static i302Event<float> OnMouseScroll = new();
    
    // Move Left Button Events
    public static i302Event OnMoveLeftButtonDown = new();
    public static i302Event OnMoveLeftButtonUp = new();
    public static i302Event OnMoveLeftButtonHeld = new();
    public static i302Event OnMoveLeftButtonReleased = new();
    
    // Move Right Button Events
    public static i302Event OnMoveRightButtonDown = new();
    public static i302Event OnMoveRightButtonUp = new();
    public static i302Event OnMoveRightButtonHeld = new();
    public static i302Event OnMoveRightButtonReleased = new();
    
    // Swap Button Events
    public static i302Event OnSwapButtonDown = new();
    public static i302Event OnSwapButtonUp = new();
    public static i302Event OnSwapButtonHeld = new();
    public static i302Event OnSwapButtonReleased = new();
    
    // Drop Button Events
    public static i302Event OnDropButtonDown = new();
    public static i302Event OnDropButtonUp = new();
    public static i302Event OnDropButtonHeld = new();
    public static i302Event OnDropButtonReleased = new();
    
    // Pickup Button Events
    public static i302Event OnPickupButtonDown = new();
    public static i302Event OnPickupButtonUp = new();
    public static i302Event OnPickupButtonHeld = new();
    public static i302Event OnPickupButtonReleased = new();
    
    // Interact Button Events
    public static i302Event OnInteractButtonDown = new();
    public static i302Event OnInteractButtonUp = new();
    public static i302Event OnInteractButtonHeld = new();
    public static i302Event OnInteractButtonReleased = new();
    
    // Jump Button Events
    public static i302Event OnJumpButtonDown = new();
    public static i302Event OnJumpButtonUp = new();
    public static i302Event OnJumpButtonHeld = new();
    public static i302Event OnJumpButtonReleased = new();

    public static i302Event<string> OnDisplayDialogueWindow = new();
    public static i302Event<bool> OnSetPlayerInputLock = new();
    public static i302Event<SuperSimple2dCharacterMotor> OnPlayerDeath = new();
    public static i302Event OnInteract = new();
}
