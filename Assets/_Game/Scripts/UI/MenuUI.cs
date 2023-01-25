using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;

    [Header("Popups")]
    [SerializeField] private Popup optionsPopup;

    private void Start()
    {
        playButton.onClick.AddListener(PlayAction);
        optionsButton.onClick.AddListener(OptionsAction);
    }

    private void PlayAction()
    {
        SceneManager.LoadScene("Game");
    }

    private void OptionsAction()
    {
        optionsPopup.Open();
    }
}
