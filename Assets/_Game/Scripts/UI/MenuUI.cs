using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(SaveOptions))]
public class MenuUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;

    [Header("Popups")]
    [SerializeField] private Popup optionsPopup;

    [Header("Options")]
    [SerializeField] private Slider sessionSlider;
    [SerializeField] private TextMeshProUGUI sessionText;
    [SerializeField] private Slider spawSlider;
    [SerializeField] private TextMeshProUGUI spawText;

    private SaveOptions _saveOptions;

    private void Start()
    {
        Time.timeScale = 1;
        _saveOptions = GetComponent<SaveOptions>();

        playButton.onClick.AddListener(PlayAction);
        optionsButton.onClick.AddListener(OptionsAction);
        sessionSlider.onValueChanged.AddListener(SessionSlider);
        spawSlider.onValueChanged.AddListener(SpawSlider);

        sessionSlider.value = _saveOptions.SessionTimerInSeconds / 60;
        spawSlider.value = _saveOptions.SpawTimerInSeconds;
    }

    private void PlayAction()
    {
        SceneManager.LoadScene("Game");
    }

    private void OptionsAction()
    {
        optionsPopup.Open();
    }

    private void SessionSlider(float value)
    {
        _saveOptions.SaveSessionTime(value * 60);
        sessionText.text = $"{value}m";
    }

    private void SpawSlider(float value)
    {
        _saveOptions.SaveSpawTime(value);
        var formattedFloat = value.ToString("F2");
        spawText.text = $"{formattedFloat}s";
    }
}
