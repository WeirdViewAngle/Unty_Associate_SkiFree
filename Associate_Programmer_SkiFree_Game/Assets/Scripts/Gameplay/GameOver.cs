using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameOver
{
    static GameObject MainPanel, WinMessage, LoseMessage;

    public static void Initialize()
    {
        MainPanel = UIManager._Instance.mainPanel;
        WinMessage = UIManager._Instance.winMessaga;
        LoseMessage = UIManager._Instance.loseMessage;
    }



    public static void Win()
    {
        Time.timeScale = 0;
        MainPanel.SetActive(true);
        WinMessage.SetActive(true);
    }

    public static void Lose()
    {
        Time.timeScale = 0;
        MainPanel.SetActive(true);
        LoseMessage.SetActive(true);

    }
}
