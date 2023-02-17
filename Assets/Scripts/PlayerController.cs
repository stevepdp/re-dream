using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : StarterAssets.ThirdPersonController
{
    [SerializeField] float startingMoveSpeed;
    [SerializeField] float startingSprintSpeed;
    [SerializeField] float speedReduction;

    void OnEnable()
    {
        Challenge.OnReducePlayerSpeed += ReducePlayerSpeed;
        Challenge.OnRestorePlayerSpeed += RestorePlayerSpeed;
        HintTrigger.OnReducePlayerSpeed += ReducePlayerSpeed;
        HintTrigger.OnRestorePlayerSpeed += RestorePlayerSpeed;
    }

    void OnDisable()
    {
        Challenge.OnReducePlayerSpeed -= ReducePlayerSpeed;
        Challenge.OnRestorePlayerSpeed -= RestorePlayerSpeed;
        HintTrigger.OnReducePlayerSpeed -= ReducePlayerSpeed;
        HintTrigger.OnRestorePlayerSpeed -= RestorePlayerSpeed;
    }

    void ReducePlayerSpeed()
    {
        startingMoveSpeed = MoveSpeed;
        startingSprintSpeed = SprintSpeed;
        MoveSpeed = startingMoveSpeed / speedReduction;
        SprintSpeed = startingSprintSpeed / speedReduction;
    }

    void RestorePlayerSpeed()
    {
        MoveSpeed = startingMoveSpeed;
        SprintSpeed = startingSprintSpeed;
    }
}
