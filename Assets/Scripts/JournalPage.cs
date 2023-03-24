using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "New Journal Page", order = 0)]
public class JournalPage : ScriptableObject
{
    public int pageNumber;
    public string pageContent;
    public bool pageUnlocked;
    public static readonly JournalPage empty = null;
}

