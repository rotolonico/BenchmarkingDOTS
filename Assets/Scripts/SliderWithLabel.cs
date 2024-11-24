using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SliderWithLabel : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI labelText;
    [SerializeField] private TMP_InputField labelInputField;
    [SerializeField] private string labelName;

    [SerializeField] private bool isExponential;


    private void Awake()
    {
        slider.onValueChanged.AddListener(OnSliderValueChanged);
        OnSliderValueChanged(slider.value);
        labelText.text = labelName;

        labelInputField.onEndEdit.AddListener(OnInputFieldChanged);
    }

    public void AddOnValueChangedListener(UnityAction<float> action) =>
        slider.onValueChanged.AddListener(_ => action.Invoke(GetValue()));

    private void OnSliderValueChanged(float value)
    {
        labelInputField.SetTextWithoutNotify(GetValue().ToString());
    }

    public void SetValue(int value)
    {
        slider.value = isExponential ? (float)Math.Log(value, 10) : value;
        labelInputField.SetTextWithoutNotify(value.ToString());
    }

    public int GetValue() => GetValue(slider.value);
    
    private int GetValue(float valueToGet) => isExponential ? (int)Math.Round(Math.Pow(10, valueToGet)) : (int)valueToGet;

    private void OnInputFieldChanged(string newValue)
    {
        if (int.TryParse(newValue, out var value) && value <= GetValue(slider.maxValue) && value >= GetValue(slider.minValue))
            SetValue(value);
        else
            labelInputField.SetTextWithoutNotify(GetValue().ToString());
    }
}