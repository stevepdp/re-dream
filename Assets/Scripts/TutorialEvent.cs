using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialEvent : MonoBehaviour
{
    [SerializeField] GameObject tutorialPanel;
    [SerializeField] GameObject tutorialTitleObj;
    [SerializeField] GameObject tutorialImageObj;
    [SerializeField] GameObject tutorialBodyObj;
    [SerializeField] float tutorialTimeout;
    Sprite tutorialSprite;

    const string BASE_DIR_ART = "Art/";
    const string BASE_DIR_CONTROLS = BASE_DIR_ART + "Controls/";

    void OnEnable()
    {
        PauseMenu.OnPlayerPaused += HideTutorial;
        Player.OnPlayerIdle += SetIdleTutorial;
        Player.OnPlayerInput += HideTutorial;
    }

    void OnDisable()
    {
        PauseMenu.OnPlayerPaused -= HideTutorial;
        Player.OnPlayerIdle -= SetIdleTutorial;
        Player.OnPlayerInput -= HideTutorial;
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

    void SetIdleTutorial()
    {
        TMP_Text titleText = tutorialTitleObj?.GetComponent<TMP_Text>();
        if (titleText != null) titleText.text = "Tutorial: Movement";

        Image tutorialImage = tutorialImageObj?.GetComponent<Image>();
        if (tutorialImage != null) tutorialImage.sprite = Resources.Load<Sprite>(BASE_DIR_CONTROLS + "left-stick-full");

        TMP_Text bodyText = tutorialBodyObj?.GetComponent<TMP_Text>();
        if (bodyText != null) bodyText.text = "...or press the <b>WASD</b> keys on your keyboard.";

        ShowTutorial();
    }
}
