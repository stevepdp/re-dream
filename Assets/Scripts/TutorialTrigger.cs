using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] GameObject tutorialPanel;
    [SerializeField] GameObject tutorialTitleObj;
    [SerializeField] GameObject tutorialImageObj;
    [SerializeField] GameObject tutorialBodyObj;
    
    [SerializeField] string tutorialTitleText;
    [SerializeField] Sprite tutorialSprite;
    [SerializeField] string tutorialBodyText;
    bool lessonLearned;

    public static event Action OnReducePlayerSpeed;
    public static event Action OnRestorePlayerSpeed;

    void OnEnable()
    {
        JournalViewer.OnJournalOpened += HideTutorial;
        PauseMenu.OnPlayerPaused += HideTutorial;
        Crystal.OnCrystalCollected += SetLessonLearned;
    }

    void OnDisable()
    {
        JournalViewer.OnJournalOpened -= HideTutorial;
        PauseMenu.OnPlayerPaused -= HideTutorial;
        Crystal.OnCrystalCollected -= SetLessonLearned;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        { 
            if (!lessonLearned)
            {
                OnReducePlayerSpeed?.Invoke();
                SetTutorial();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnRestorePlayerSpeed?.Invoke();
            HideTutorial();
        }
    }

    void HideTutorial()
    {
        tutorialTitleObj?.SetActive(false);
        tutorialImageObj?.SetActive(false);
        tutorialBodyObj?.SetActive(false);
        tutorialPanel?.SetActive(false);
    }

    void ShowTutorial()
    {
        tutorialTitleObj?.SetActive(true);
        tutorialImageObj?.SetActive(true);
        tutorialBodyObj?.SetActive(true);
        tutorialPanel?.SetActive(true);
    }

    void SetLessonLearned() => lessonLearned = true;

    void SetTutorial()
    {
        TMP_Text titleText = tutorialTitleObj.GetComponent<TMP_Text>();
        if (titleText != null) titleText.text = tutorialTitleText;

        Image image = tutorialImageObj.GetComponent<Image>();
        if (image != null)
        {
            image.sprite = tutorialSprite;
            image.SetNativeSize();
        }

        TMP_Text bodyText = tutorialBodyObj.GetComponent<TMP_Text>();
        if (bodyText != null) bodyText.text = tutorialBodyText;

        ShowTutorial();
    }
}
