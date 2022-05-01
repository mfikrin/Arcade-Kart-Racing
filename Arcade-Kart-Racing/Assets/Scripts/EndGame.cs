using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public static string displayPlayerOne;
    public static string displayPlayerTwo;

    public TextMeshProUGUI displayPlayer1;
    public TextMeshProUGUI displayPlayer2;
    private void Start()
    {
        Debug.Log(displayPlayerOne);
        Debug.Log(displayPlayerTwo);
        displayPlayer1.text = displayPlayerOne;
        displayPlayer2.text = displayPlayerTwo;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");

    }
}
