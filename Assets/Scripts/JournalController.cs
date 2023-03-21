using UnityEngine;

public class JournalController : MonoBehaviour
{
    [SerializeField] JournalPage[] journalPageScriptableObjects;
    int firstPage = 0;
    int pageOffset = 1;

    public int pageCount
    {
        get { return journalPageScriptableObjects.Length - pageOffset; }
    }

    void Awake()
    {
        // always show the first page
        journalPageScriptableObjects[firstPage].pageUnlocked = true;
    }

    void OnEnable()
    {
        PuzzlePiece.OnPuzzlePieceCollected += UnlockPage;
        PuzzlePieceForChallenges.OnPuzzlePieceCollected += UnlockPage;
    }

    void OnDisable()
    {
        PuzzlePiece.OnPuzzlePieceCollected -= UnlockPage;
        PuzzlePieceForChallenges.OnPuzzlePieceCollected -= UnlockPage;
    }

    void UnlockPage(object puzzlePieceObj)
    {
        if (puzzlePieceObj is PuzzlePiece puzzlePiece)
        {
            journalPageScriptableObjects[puzzlePiece.StoryId].pageUnlocked = true;
        }
        else if (puzzlePieceObj is PuzzlePieceForChallenges puzzlePieceForChallenges)
        {
            journalPageScriptableObjects[puzzlePieceForChallenges.StoryId].pageUnlocked = true;
        }
        else
        {
            return;
        }
    }

    public object CanViewPage(int pageIndex)
    {
        if (journalPageScriptableObjects[pageIndex].pageUnlocked)
            return journalPageScriptableObjects[pageIndex];
        else
            return false;
    }
}
