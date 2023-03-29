using UnityEngine;

public class PlayerController : StarterAssets.ThirdPersonController
{
    [SerializeField] float startingMoveSpeed = 0;
    [SerializeField] float startingSprintSpeed = 0;
    [SerializeField] float speedReduction = 1.5f;
    [SerializeField] bool canJump;

    void OnEnable()
    {
        Challenge.OnDisableJump += DisablePlayerJump;
        Challenge.OnEnableJump += EnablePlayerJump;
        Challenge.OnReducePlayerSpeed += ReducePlayerSpeed;
        Challenge.OnRestorePlayerSpeed += RestorePlayerSpeed;
        TutorialTrigger.OnReducePlayerSpeed += ReducePlayerSpeed;
        TutorialTrigger.OnRestorePlayerSpeed += RestorePlayerSpeed;
        JournalViewer.OnJournalOpened += LockCamera;
        JournalViewer.OnJournalClosed += FreeCamera;
        PauseMenu.OnPlayerPaused += LockCamera;
        PauseMenu.OnPlayerResumed += FreeCamera;
    }

    void OnDisable()
    {
        Challenge.OnDisableJump -= DisablePlayerJump;
        Challenge.OnEnableJump -= EnablePlayerJump;
        Challenge.OnReducePlayerSpeed -= ReducePlayerSpeed;
        Challenge.OnRestorePlayerSpeed -= RestorePlayerSpeed;
        JournalViewer.OnJournalOpened -= LockCamera;
        JournalViewer.OnJournalClosed -= FreeCamera;
        TutorialTrigger.OnReducePlayerSpeed -= ReducePlayerSpeed;
        TutorialTrigger.OnRestorePlayerSpeed -= RestorePlayerSpeed;
        PauseMenu.OnPlayerPaused -= LockCamera;
        PauseMenu.OnPlayerResumed -= FreeCamera;
    }

    void DisablePlayerJump() => canJump = false;

    void EnablePlayerJump() => canJump = true;

    protected override void JumpAndGravity()
    {
        if (!canJump)
        {
            Grounded = true;
        }
        else
        {
            base.JumpAndGravity();
        }
    }

    void FreeCamera() => LockCameraPosition = false;

    void LockCamera()=> LockCameraPosition = true;

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
