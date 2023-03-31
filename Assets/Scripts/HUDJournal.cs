using UnityEngine;

public class HUDJournal : MonoBehaviour
{
    MeshRenderer hudJournalNotifierMesh;

    void Awake()
    {
        hudJournalNotifierMesh = GetComponent<MeshRenderer>();
    }

    void OnEnable()
    {
        JournalViewer.OnJournalOpened += HideJournalNotification;
        PuzzlePiece.OnPuzzlePieceCollected += ShowJournalNotification;
        PuzzlePieceForChallenges.OnPuzzlePieceCollected += ShowJournalNotification;
    }

    void OnDisable()
    {
        JournalViewer.OnJournalOpened -= HideJournalNotification;
        PuzzlePiece.OnPuzzlePieceCollected -= ShowJournalNotification;
        PuzzlePieceForChallenges.OnPuzzlePieceCollected -= ShowJournalNotification;
    }

    void HideJournalNotification()
    {
        if (hudJournalNotifierMesh != null)
            hudJournalNotifierMesh.enabled = false;
    }

    void ShowJournalNotification(Object puzzlePiece)
    {
        if (hudJournalNotifierMesh != null)
            hudJournalNotifierMesh.enabled = true;
    }
}
