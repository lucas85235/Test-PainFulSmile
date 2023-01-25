using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveOptions : MonoBehaviour
{
    [SerializeField] private bool autoLoad = true;

    private int sessionTimerInMinutes = 120;
    private float spawTimerInSeconds = 1.8f;

    public int SessionTimerInSeconds { get => sessionTimerInMinutes; }
    public float SpawTimerInSeconds { get => spawTimerInSeconds; }
    private const string SESSION_TIMER_KEY = "SESSION_TIMER_KEY";
    private const string SPAW_TIMER_KEY = "SPAW_TIMER_KEY";

    private void Awake()
    {
        if (autoLoad)
        {
            Load();
        }
    }

    public void Load()
    {
        sessionTimerInMinutes = PlayerPrefs.GetInt(SESSION_TIMER_KEY, sessionTimerInMinutes);
        spawTimerInSeconds = PlayerPrefs.GetFloat(SPAW_TIMER_KEY, spawTimerInSeconds);
    }

    public void SaveSessionTime(float time)
    {
        PlayerPrefs.SetInt(SESSION_TIMER_KEY, Mathf.RoundToInt(time));
        sessionTimerInMinutes = Mathf.RoundToInt(time);
        PlayerPrefs.Save();
    }

    public void SaveSpawTime(float time)
    {
        PlayerPrefs.SetFloat(SPAW_TIMER_KEY, time);
        spawTimerInSeconds = time;
        PlayerPrefs.Save();
    }
}
