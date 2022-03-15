using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterMenu : MonoBehaviour
{
    [SerializeField] public Character Char;
    [Header("HP")]
    [SerializeField] public Slider HpSlider;
    [SerializeField] public TMP_InputField HpText;
    [Header("Hit Chance")]
    [SerializeField] public Slider HitChanceSlider;
    [SerializeField] public TMP_InputField HitChanceText;
    [Header("Damage")]
    [SerializeField] public Slider DamageSlider;
    [SerializeField] public TMP_InputField DamageText;
    [Header("Training")]
    [SerializeField] public Slider TrainingSlider;
    [SerializeField] public TMP_InputField TrainingText;
    
    private void Start()
    {
        AddListenersToUI();
    }

    private void AddListenersToUI () {
        HpSlider.onValueChanged.AddListener (delegate { SliderValueChanged(HpSlider, HpText); });
        HpText.onValueChanged.AddListener(delegate { InputFieldValueChanged(HpSlider, HpText); });
        HitChanceSlider.onValueChanged.AddListener (delegate { SliderValueChanged(HitChanceSlider, HitChanceText); });
        HitChanceText.onValueChanged.AddListener (delegate { InputFieldValueChanged(HitChanceSlider, HitChanceText); });
        DamageSlider.onValueChanged.AddListener (delegate { SliderValueChanged(DamageSlider, DamageText); });
        DamageText.onValueChanged.AddListener (delegate { InputFieldValueChanged(DamageSlider, DamageText); });
        TrainingSlider.onValueChanged.AddListener (delegate { SliderValueChanged(TrainingSlider, TrainingText); });
        TrainingText.onValueChanged.AddListener (delegate { InputFieldValueChanged(TrainingSlider, TrainingText); });
    }
    
    private void SliderValueChanged (Slider slider, TMP_InputField field) {
        //Value is always within bounds
        float rounded = Mathf.Round(slider.value * 10.0f) * 0.1f;
        field.text = rounded.ToString();
    }

    private void InputFieldValueChanged (Slider slider, TMP_InputField field) {
        //Adjust value to fit into bounds 
        float newValue = float.Parse(field.text);
        if (newValue > slider.maxValue)
            newValue = slider.maxValue;
        else if (newValue < slider.minValue)
            newValue = slider.minValue;
        slider.value = newValue;
        field.text = newValue.ToString();
    }

    private void OnEnable() {
        OpenCharacterMenu();
    }

    public void UpdateCharacterObject() {
        Char.currentHP = HpSlider.value;
        Char.damage = DamageSlider.value;
        Char.hitChance = HitChanceSlider.value;
        Char.trainingHours = TrainingSlider.value;
    }

    public void OpenCharacterMenu() {
        HpSlider.value = Char.currentHP;
        HpText.text = Char.currentHP.ToString();
        HitChanceSlider.value = Char.hitChance;
        HitChanceText.text = Char.hitChance.ToString();
        DamageSlider.value = Char.damage;
        DamageText.text = Char.damage.ToString();
        TrainingSlider.value = (float)Char.trainingHours;
        TrainingText.text = Char.trainingHours.ToString();
    }
}
