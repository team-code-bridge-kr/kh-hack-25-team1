using UnityEngine;
using UnityEngine.UI;
using TMPro; // For UI text display

public class SettingsStressSlider : MonoBehaviour
{
    public Slider stressSlider;      // Reference to the UI slider
    public TextMeshProUGUI label;    // Optional: Text to show ON/OFF state

    void Start()
    {
        // Load saved value (default = 1, meaning ON)
        float savedValue = PlayerPrefs.GetFloat("StressModeValue", 1f);
        stressSlider.value = savedValue;

        // Apply the initial state
        UpdateStressMode(savedValue);

        // Add listener for value changes
        stressSlider.onValueChanged.AddListener(OnSliderChanged);
    }

    void OnSliderChanged(float value)
    {
        // Snap value to the nearest whole number (0 or 1)
        float snappedValue = Mathf.Round(value);
        stressSlider.value = snappedValue;

        // Apply setting
        UpdateStressMode(snappedValue);

        // Save the setting
        PlayerPrefs.SetFloat("StressModeValue", snappedValue);
        PlayerPrefs.SetInt("StressOn", snappedValue == 1 ? 1 : 0);
        PlayerPrefs.Save();
    }

    void UpdateStressMode(float value)
    {
        bool isOn = value >= 1f;

        // Update the GameManager if it exists
        if (GameManager.Instance != null)
            GameManager.Instance.SetStressMode(isOn);

        // Update label text if assigned
        if (label != null)
            label.text = isOn ? "Stress Mode: ON" : "Stress Mode: OFF";

        Debug.Log($"[Settings] Stress Mode {(isOn ? "ON" : "OFF")}");
    }
}