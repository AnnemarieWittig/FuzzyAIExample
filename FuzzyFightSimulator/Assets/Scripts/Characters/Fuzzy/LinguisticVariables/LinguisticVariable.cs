using UnityEngine;

public enum BattleChoices { ATTACK, BLOCK, ESCAPE }

public class LinguisticVariable : ScriptableObject
{
    [SerializeField] public string label;

    public string getLabel() => label;
}
