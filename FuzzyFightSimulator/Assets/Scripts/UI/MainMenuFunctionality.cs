using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuFunctionality : MonoBehaviour
{
    [SerializeField] public GameObject MainMenuUI;
    [SerializeField] public GameObject PlayerMenu;
    [SerializeField] public GameObject EnemyMenu;

    public void OnPlayerMenuButton() {
        PlayerMenu.SetActive(true);
        MainMenuUI.SetActive(false);
        EnemyMenu.SetActive(false);
    }

    public void OnEnemyMenuButton() {
        PlayerMenu.SetActive(false);
        MainMenuUI.SetActive(false);
        EnemyMenu.SetActive(true);
    }

    public void ReturnToMainMenuFromOtherMenu() {
        MainMenuUI.SetActive(true);
        PlayerMenu.SetActive(false);
        EnemyMenu.SetActive(false);
    }

    public void SetCharacterInMenus (Character player, Character enemy) {
        CharacterMenu playerMenu = PlayerMenu.GetComponent<CharacterMenu>();
        playerMenu.Char = player;
        CharacterMenu enemyMenu = EnemyMenu.GetComponent<CharacterMenu>();
        enemyMenu.Char = enemy;
    }

}
