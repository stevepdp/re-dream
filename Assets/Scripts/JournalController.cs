using UnityEngine;

public class JournalController : MonoBehaviour
{
    [SerializeField] bool[] unlockedPages;
    [SerializeField] JournalPage[] journalPageScriptableObjects;
    int firstPage = 0;

    public int pageCount
    {
        get { return unlockedPages.Length -1; }
    }

    void Awake()
    {
         unlockedPages = new bool[journalPageScriptableObjects.Length];

#if UNITY_EDITOR
        // all pages visible whilst building / testing...
        for (int i = 0; i < unlockedPages.Length; i++) { 
            unlockedPages[i] = true;
        }
#else
        // always show the first page
        unlockedPages[firstPage] = true;
#endif
    }

    void OnEnable()
    {
        PuzzlePiece.OnPuzzlePieceCollected += UnlockPage;
    }

    void OnDisable()
    {
        PuzzlePiece.OnPuzzlePieceCollected -= UnlockPage;
    }

    void UnlockPage(PuzzlePiece puzzlePiece)
    {
        //Debug.Log("Unlocking: " + puzzlePiece.StoryId);
        unlockedPages[puzzlePiece.StoryId] = true;
    }

    public object CanViewPage(int pageIndex)
    {
        if (unlockedPages[pageIndex])
            return journalPageScriptableObjects[pageIndex];
        else
            return false;
    }
}
