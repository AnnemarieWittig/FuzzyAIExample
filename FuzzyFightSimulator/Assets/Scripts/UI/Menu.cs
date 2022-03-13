using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    [SerializeField] public GameObject PauseMenuUI;
    [SerializeField] public GameObject GameMenuUI;
    [SerializeField] public GameObject PlayerField;
    [SerializeField] public GameObject EnemyField;

    public void Resume()
    {
        changeActiveOfGame(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        changeActiveOfGame(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    private void changeActiveOfGame(bool active)
    {
        PauseMenuUI.SetActive(!active);
        GameMenuUI.SetActive(active);
        PlayerField.SetActive(active);
        EnemyField.SetActive(active);
    }
}
