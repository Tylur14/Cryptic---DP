using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueWindow : Window
{
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private InputPrompt prompt;

    private void OnEnable()
    {
        GameEvents.OnDisplayDialogueWindow += DisplayText;
    }

    private void OnDisable()
    {
        GameEvents.OnDisplayDialogueWindow -= DisplayText;
    }

    private void DisplayText(string incomingText)
    {
        displayText.text = incomingText;
        Show();
    }

    public override void Show()
    {
        base.Show();
        prompt.Show();
        Time.timeScale = 0;
        GameEvents.OnJumpButtonUp += Hide;
        GameEvents.OnSetPlayerInputLock.Raise(true);
    }

    public override void Hide()
    {
        base.Hide();
        prompt.Hide();
        Time.timeScale = 1;
        GameEvents.OnJumpButtonUp -= Hide;
        GameEvents.OnSetPlayerInputLock.Raise(false);
    }
}
