using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    [SerializeField]
    Status health;

    [SerializeField]
    Text lblHealth;


    void OnEnable()
    {
        UpdateUI(health.Current);
    }

    void Awake()
    {
        SubscribeEvent();
    }

    void OnDestroy()
    {
        UnsubscribeEvent();
    }

    void UpdateUI(int value)
    {
        lblHealth.text = "Health : " + value;
    }

    void OnValueChanged(int value)
    {
        UpdateUI(value);
    }

    void SubscribeEvent()
    {
        health.OnValueChanged += OnValueChanged;
    }

    void UnsubscribeEvent()
    {
        health.OnValueChanged -= OnValueChanged;
    }
}

