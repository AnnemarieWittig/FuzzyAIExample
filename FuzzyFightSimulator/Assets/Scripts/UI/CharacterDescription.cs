using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDescription : MonoBehaviour
{

    [SerializeField] TMP_Text NameText;
    [SerializeField] TMP_Text CurrHP;
    [SerializeField] Slider HpSlider;

    public void Initialize(Character character)
    {
        NameText.text = character.CharacterName;
        HpSlider.maxValue = character.MaxHP;
        HpSlider.value = character.CurrentHP;
        SetHPText(character.CurrentHP);
    }

    public void SetHP(float hp)
    {
        if (hp < 0)
            hp = 0;
        HpSlider.value = hp;
        SetHPText(hp);
    }

    private void SetHPText(float hp)
    {
        hp = Mathf.Round(hp * 10.0f) * 0.1f;
        CurrHP.text = hp.ToString() + "/100";
    }



}
