using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterMenu : MonoBehaviour
{
    #region Variables
    [SerializeField] public Character Char;
    [SerializeField] public CharacterDescription CharWindow;
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

    [Header("Random Events")]
    [SerializeField] public Toggle CritToggle;
    [SerializeField] public TMP_InputField CritChance;
    [SerializeField] public TMP_Text[] CritTextsForColorChange;
    [SerializeField] public Toggle HealToggle;
    [SerializeField] public TMP_InputField HealChance;
    [SerializeField] public TMP_InputField HealValue;
    [SerializeField] public TMP_Text[] HealTextsForColorChange;

    #endregion

    private void Start()
    {
        AddListenersToUI();
    }

    private void AddListenersToUI()
    {
        HpSlider.onValueChanged.AddListener(delegate { SliderValueChanged(HpSlider, HpText); });
        HpText.onValueChanged.AddListener(delegate { InputFieldValueChanged(HpSlider, HpText); });
        HitChanceSlider.onValueChanged.AddListener(delegate { SliderValueChanged(HitChanceSlider, HitChanceText); });
        HitChanceText.onValueChanged.AddListener(delegate { InputFieldValueChanged(HitChanceSlider, HitChanceText); });
        DamageSlider.onValueChanged.AddListener(delegate { SliderValueChanged(DamageSlider, DamageText); });
        DamageText.onValueChanged.AddListener(delegate { InputFieldValueChanged(DamageSlider, DamageText); });
        TrainingSlider.onValueChanged.AddListener(delegate { SliderValueChanged(TrainingSlider, TrainingText); });
        TrainingText.onValueChanged.AddListener(delegate { InputFieldValueChanged(TrainingSlider, TrainingText); });
        CritToggle.onValueChanged.AddListener(delegate { CritToggleTriggered(); });
        HealToggle.onValueChanged.AddListener(delegate { HealToggleTriggered(); });
        CritChance.onDeselect.AddListener(delegate { RandomEventInputFieldChanged(CritChance); });
        HealChance.onDeselect.AddListener(delegate { RandomEventInputFieldChanged(HealChance); });
        HealValue.onDeselect.AddListener(delegate { RandomEventInputFieldChanged(HealValue); });
    }

    private void SliderValueChanged(Slider slider, TMP_InputField field)
    {
        //Value is always within bounds
        float rounded = Mathf.Round(slider.value * 10.0f) * 0.1f;
        field.text = rounded.ToString();
    }

    private void InputFieldValueChanged(Slider slider, TMP_InputField field)
    {
        //Adjust value to fit into bounds 
        float newValue = InputFieldValidation(float.Parse(field.text), slider.minValue, slider.maxValue);
        slider.value = newValue;
        field.text = newValue.ToString();
    }

    private float InputFieldValidation(float input, float minValue, float maxValue)
    {
        if (input > maxValue)
            return maxValue;
        else if (input < minValue)
            return minValue;
        return input;
    }

    private void RandomEventInputFieldChanged(TMP_InputField field)
    {
        float input = float.Parse(field.text);
        float newValue = InputFieldValidation(input, 0, 100);
        field.text = newValue.ToString();
    }

    private void HealToggleTriggered()
    {
        if (HealToggle.isOn)
            ChangeHealArea(false, Color.white, Char.HealChance.ToString(), Char.HealValue.ToString());

        else
            ChangeHealArea(true, Color.gray, "0", "0");
    }

    private void CritToggleTriggered()
    {
        if (CritToggle.isOn)
        {
            ChangeCritArea(false, Color.white, Char.CritChance.ToString());
        }

        else
            ChangeCritArea(true, Color.gray, "0");
    }

    private void ChangeHealArea(bool activate, Color color, string textChance, string textValue)
    {
        HealChance.text = textChance;
        HealValue.text = textValue;
        HealChance.readOnly = activate;
        HealValue.readOnly = activate;
        ChangeTextColor(HealTextsForColorChange, color);
    }

    private void ChangeCritArea(bool activate, Color color, string text)
    {
        CritChance.text = text;
        CritChance.readOnly = activate;
        ChangeTextColor(CritTextsForColorChange, color);
    }

    private void ChangeTextColor(TMP_Text[] texts, Color color)
    {
        foreach (TMP_Text text in texts)
        {
            text.color = color;
        }
    }

    private void OnEnable()
    {
        OpenCharacterMenu();
    }

    public void UpdateCharacterObject()
    {
        Char.CurrentHP = HpSlider.value;
        Char.Damage = DamageSlider.value;
        Char.HitChance = HitChanceSlider.value;
        Char.TrainingHours = TrainingSlider.value;
        if (CritToggle.isOn)
        {
            Char.CritAllowance = true;
            Char.CritChance = float.Parse(CritChance.text);
        }
        else
            Char.CritAllowance = false;
        if (HealToggle.isOn)
        {
            Char.HealAllowance = true;
            Char.HealChance = float.Parse(HealChance.text);
            Char.HealValue = float.Parse(HealValue.text);
        }
        else
            Char.HealAllowance = false;
        CharWindow.SetHP(Char.CurrentHP);
    }

    public void OpenCharacterMenu()
    {
        HpSlider.value = Char.CurrentHP;
        HpText.text = Char.CurrentHP.ToString();
        HitChanceSlider.value = Char.HitChance;
        HitChanceText.text = Char.HitChance.ToString();
        DamageSlider.value = Char.Damage;
        DamageText.text = Char.Damage.ToString();
        TrainingSlider.value = (float)Char.TrainingHours;
        TrainingText.text = Char.TrainingHours.ToString();
        AdjustRandomEvents();
    }

    private void AdjustRandomEvents()
    {
        if (Char.CritAllowance)
            CritToggle.isOn = true;
        else
            CritToggle.isOn = false;
        if (Char.HealAllowance)
            HealToggle.isOn = true;
        else
            HealToggle.isOn = false;
        CritToggleTriggered();
        HealToggleTriggered();
    }
}
