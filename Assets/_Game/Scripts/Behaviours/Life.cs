using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Life : MonoBehaviour
{
    [SerializeField] private int maxLife = 100;
    [SerializeField] private Slider lifeSlider;

    [Header("Events")]
    [Space]
    public UnityEvent OnLifeAdded;
    public UnityEvent OnTakeDamage;
    public UnityEvent OnDeath;

    private int _currentLife;

    private void Start()
    {
        _currentLife = maxLife;
        lifeSlider.maxValue = maxLife;
        UpdateLifeUI();
    }

    public void AddLife(int value)
    {
        _currentLife += value;
        _currentLife = Mathf.Clamp(_currentLife, 0, maxLife);
        UpdateLifeUI();
        OnLifeAdded.Invoke();
    }

    public void TakeDamage(int value)
    {
        _currentLife -= value;
        _currentLife = Mathf.Clamp(_currentLife, 0, maxLife);
        UpdateLifeUI();

        if (_currentLife == 0)
        {
            OnDeath.Invoke();
        }
        else OnTakeDamage.Invoke();
    }

    private void UpdateLifeUI()
    {
        lifeSlider.value = _currentLife;
    }
}
