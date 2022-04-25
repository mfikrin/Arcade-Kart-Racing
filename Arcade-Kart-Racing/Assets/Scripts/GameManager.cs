using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<CarController> cars= new List<CarController>();
    public Transform[] spawnPoints;


    public float positionUpdateRate = 0.05f;
    private float lastPositionUpdateTime;

    public static GameManager instance;

    public int playersToBegin = 2;
    public bool gameStarted = false;

    public int lapsToWin;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (Time.time - lastPositionUpdateTime > positionUpdateRate)
        {
            lastPositionUpdateTime = Time.time;
            UpdateCarRacePositions();
        }

        if (!gameStarted && cars.Count == playersToBegin)
        {
            gameStarted = true;
            StartCountDown();

        }
    }

    void StartCountDown()
    {
        PlayerUI[] uis = FindObjectsOfType<PlayerUI>();

        for (int i = 0; i < uis.Length; i++)
        {
            uis[i].StartCountdownDisplay();
        }
        Invoke("BeginGame", 3.0f);
    }

    void BeginGame()
    {
        for (int i = 0; i < cars.Count; i++)
        {
            cars[i].canControl = true;
        }
    }

    void UpdateCarRacePositions()
    {
        cars.Sort(SortPosition);

        for(int i = 0; i < cars.Count; i++)
        {
            cars[i].racePosition = cars.Count - i;
        }
    }

    int SortPosition(CarController a, CarController b)
    {
        if (a.zonesPassed > b.zonesPassed)
        {
            return 1;
        }else if (b.zonesPassed > a.zonesPassed)
        {
            return -1;
        }

        float aDist = Vector3.Distance(a.transform.position,a.curTrackZone.transform.position);
        float bDist = Vector3.Distance(b.transform.position,b.curTrackZone.transform.position);

        return aDist > bDist ? 1 : -1;
    }

    public void CheckIsWinner(CarController car)
    {
        if (car.curLap == lapsToWin + 1)
        {
            for (int i = 0; i < cars.Count; i++)
            {
                cars[i].canControl = false;
            }
            PlayerUI[] uis = FindObjectsOfType<PlayerUI>();

            for (int i = 0; i < uis.Length; i++)
            {
                uis[i].GameOver(uis[i].car == car);
            }
        }
    }

    public void UpdateLapsToWin(CarController car)
    {
        PlayerUI[] uis = FindObjectsOfType<PlayerUI>();

        for (int i = 0; i < uis.Length; i++)
        {
            // get index

            if (uis[i].car == car)
            {
                uis[i].UpdateLaps(uis[i].car.curLap, lapsToWin);
            }
            
        }
    }



}
