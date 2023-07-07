using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputPrompt : Window
{
    [SerializeField] private TextMeshProUGUI promptDisplay;
    [SerializeField] private InputActionName input;
    [SerializeField] private string prefix;
    [SerializeField] private string suffix;

    public override void Show()
    {
        base.Show();
        string k = "";
        foreach (var key in InputManager.Instance.CurrentKeybinds)
        {
            if (key.InputAction == input)
            {
                k = key.Key.ToString();
            }
        }
        promptDisplay.text = $"{prefix}{k}{suffix}";
    }
}
