using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public enum InputType
{
    Keyboard,
    Gamepad,
    MouseWheel
}

public enum InputActionName
{
    MoveLeft, MoveRight, Swap, Drop, Pickup, Interact, Jump, Scroll, PrimaryMouseButton
}

public static class ExtensionMethods
{
    public static void SetChildrenActiveState(this Transform t, bool state)
    {
        foreach (Transform child in t)
        {
            child.gameObject.SetActive(state);
        }
    }
    
    public static void DestroyChildren(this Transform t)
    {
        foreach (Transform child in t)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    
    public static void DestroyChildren(this Transform t, List<GameObject> list)
    {
        foreach (Transform child in t)
        {
            GameObject.Destroy(child.gameObject);
        }
        list.Clear();
    }
}

public static class Global
{
    public static bool IsPaused;
    public static int PlayerLock;
}
