using UnityEngine;

public enum BattleChoices { ATTACK, BLOCK, ESCAPE }

public class LinguisticVariable : ScriptableObject
{
    [SerializeField] public string Label;


    public string getLabel() => Label;
}
