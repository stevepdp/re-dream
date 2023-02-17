using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hint : MonoBehaviour
{
    Canvas hintCanvas;
    [SerializeField] TMP_Text hintText;
    [SerializeField] float hintTimeout;

    void Awake()
    {
        hintCanvas = GetComponent<Canvas>();
    }

    void OnEnable()
    {
        Player.OnPlayerIdle += ShowMovementHint;
        Player.OnPlayerInput += HideHint;
    }

    void OnDisable()
    {
        Player.OnPlayerIdle -= ShowMovementHint;
        Player.OnPlayerInput -= HideHint;
    }

    void HideHint()
    {
        hintCanvas.enabled = false;
    }

    void ShowHint()
    {
        if (hintCanvas != null) hintCanvas.enabled = true;
    }

    void ShowMovementHint()
    {
        hintText.text = "Press <bold>W</bold> or <bold>UP Arrow</bold> to move.";
        ShowHint();
    }
}
