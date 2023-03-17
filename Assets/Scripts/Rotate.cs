using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 0.25f;
    bool canRotate;

    void OnEnable()
    {
        JournalViewer.OnJournalOpened += PauseRotation;
        JournalViewer.OnJournalClosed += EnableRotation;
        PauseMenu.OnPlayerPaused += PauseRotation;
        PauseMenu.OnPlayerResumed += EnableRotation;
    }

    void OnDisable()
    {
        JournalViewer.OnJournalOpened -= PauseRotation;
        JournalViewer.OnJournalClosed -= EnableRotation;
        PauseMenu.OnPlayerPaused -= PauseRotation;
        PauseMenu.OnPlayerResumed -= EnableRotation;
    }

    void Start()
    {
        canRotate = true;
    }

    void Update()
    {
        if (canRotate) RotateObject();
    }

    void EnableRotation() => canRotate = true;
    
    void PauseRotation() => canRotate = false;

    void RotateObject()
    {
        transform.Rotate(0, rotateSpeed, 0, Space.World);
    }
}
