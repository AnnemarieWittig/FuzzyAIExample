using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDescription : MonoBehaviour
{

    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    [SerializeField] Slider hpSlider;

    public void Initialize(Character character)
    {
        nameText.text = character.characterName;
        levelText.text = "Lvl " + character.level;
        hpSlider.maxValue = character.maxHP;
        hpSlider.value = character.currentHP;
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }

}
