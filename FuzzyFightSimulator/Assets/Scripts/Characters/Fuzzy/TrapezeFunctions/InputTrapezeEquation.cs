using UnityEngine;

[CreateAssetMenu(menuName = "Equations/InputTrapezeEquations")]
public class InputTrapezeEquation : TrapezeEquation
{
    [SerializeField] public BattleChoices WishedOutcome;

    public BattleChoices getWishedOutcome => WishedOutcome;
}
