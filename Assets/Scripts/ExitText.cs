using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExitText : MonoBehaviour
{
    TMP_Text roomRequirementsText;

    void Awake()
    {
        roomRequirementsText = transform.GetComponent<TMP_Text>();
    }

    void Start()
    {
        SetDefaultExitText();
    }

    void OnEnable()
    {
        Exit.OnPlayerLeftExit += SetDefaultExitText;
        GameManager.OnRoomRequirementsMet += SetRoomCompletionText;
        GameManager.OnRoomRequirementsNotMet += SetExitRequirementsText;
    }

    void OnDisable()
    {
        Exit.OnPlayerLeftExit -= SetDefaultExitText;
        GameManager.OnRoomRequirementsMet -= SetRoomCompletionText;
        GameManager.OnRoomRequirementsNotMet -= SetExitRequirementsText;
    }

    void SetDefaultExitText() => roomRequirementsText.text = "Exit";

    void SetExitRequirementsText() => roomRequirementsText.text = "Puzzle Pieces Remaining: " + Mathf.Abs(GameManager.instance.PlayerPuzzlePiecesCount - GameManager.instance.RoomPuzzlePiecesTotal);

    void SetRoomCompletionText() => roomRequirementsText.text = "Dream Solved!";
}
