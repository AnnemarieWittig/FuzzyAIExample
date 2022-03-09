using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equations/InputTrapezeEquations")]
public class InputTrapezeEquation : TrapezeEquation
{
    [SerializeField] public BattleChoices wishedOutcome;

    public BattleChoices getWishedOutcome => wishedOutcome;
}
