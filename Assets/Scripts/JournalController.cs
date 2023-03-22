using UnityEngine;

public class JournalController : MonoBehaviour
{
    [SerializeField] JournalPage[] journalPageScriptableObjects;
    int firstPage = 0;
    int pageOffset = 1;
    int lastUnlockedPage = 0;

    public int pageCount
    {
        get { return journalPageScriptableObjects.Length - pageOffset; }
    }

    public int LastUnlockedPage
    {
        get { return lastUnlockedPage; }
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
            lastUnlockedPage = puzzlePiece.StoryId;
        }
        else if (puzzlePieceObj is PuzzlePieceForChallenges puzzlePieceForChallenges)
        {
            journalPageScriptableObjects[puzzlePieceForChallenges.StoryId].pageUnlocked = true;
            lastUnlockedPage = puzzlePieceForChallenges.StoryId;
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
