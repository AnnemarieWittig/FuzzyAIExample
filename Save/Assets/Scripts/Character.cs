using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] public string characterName;
    [SerializeField] public int level;

    [SerializeField] public int damage;
    [SerializeField] public int maxHP;
    [SerializeField] public int currentHP;
}
