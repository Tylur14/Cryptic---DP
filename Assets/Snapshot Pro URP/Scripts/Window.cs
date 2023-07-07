using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Window : MonoBehaviour
{
#if UNITY_EDITOR
    [FoldoutGroup("UI Window")] public string description;
#endif
    [FoldoutGroup("UI Window")] public CanvasGroup canvasGroup;
    [FoldoutGroup("UI Window")] public bool isActive;

    private void Awake()
    {
        Hide();
    }

    public void Toggle()
    {
        if (isActive)
        {
            Hide();
        }
        else Show();
    }
    
    public virtual void Show()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        isActive = true;
    }

    public virtual void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        isActive = false;
    }
}
