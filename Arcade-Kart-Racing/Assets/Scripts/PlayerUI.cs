using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI carPositionText;
    public CarController car;

    public TextMeshProUGUI countDownText;

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI lapsToWinText;

    void Update()
    {
        carPositionText.text = "Pos "+ car.racePosition.ToString() + " / " + GameManager.instance.cars.Count.ToString();
        UpdateLaps(car.curLap, GameManager.instance.lapsToWin);
    }

    public void StartCountdownDisplay()
    {
        // coroutine (can stop and start)
        StartCoroutine(Countdown());

        IEnumerator Countdown()
        {
            countDownText.gameObject.SetActive(true);
            countDownText.text = "3";
            yield return new WaitForSeconds(1.0f);

            countDownText.text = "2";
            yield return new WaitForSeconds(1.0f);

            countDownText.text = "1";
            yield return new WaitForSeconds(1.0f);

            countDownText.text = "GO!";
            yield return new WaitForSeconds(1.0f);

            countDownText.gameObject.SetActive(false);

        }
    }

    public void UpdateLaps(int currlap, int lapToWin)
    {
        lapsToWinText.text = "Lap " + currlap.ToString() + " / " + lapToWin.ToString();
    }

    public void GameOver(bool win)
    {
        gameOverText.gameObject.SetActive(true);
        gameOverText.color = win == true ? Color.green : Color.red;
        gameOverText.text = win == true ? "You Win" : "You Lost";

        if (win)
        {
            EndGame.displayPlayerOne = "Player 1 : You Win";
            EndGame.displayPlayerTwo = "Player 2 : You Lost";
        }
        else
        {
            EndGame.displayPlayerOne = "Player 1 : You Lost";
            EndGame.displayPlayerTwo = "Player 1 : You Win";
        }

    }

}
