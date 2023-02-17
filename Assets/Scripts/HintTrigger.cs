using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintTrigger : MonoBehaviour
{
    [SerializeField] Canvas hintCanvas;
    [SerializeField] TMP_Text hintCanvasText;
    [SerializeField] string triggerText;

    public static event Action OnReducePlayerSpeed;
    public static event Action OnRestorePlayerSpeed;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (hintCanvasText != null && hintCanvas != null)
            {
                hintCanvasText.text = triggerText;
                hintCanvas.enabled = true;
                OnReducePlayerSpeed?.Invoke();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (hintCanvasText != null && hintCanvas != null)
            {
                hintCanvasText.text = "";
                hintCanvas.enabled = false;
                OnRestorePlayerSpeed?.Invoke();
            }
        }
    }
}
