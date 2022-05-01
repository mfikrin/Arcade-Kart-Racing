using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackZone : MonoBehaviour
{
    public bool isGate;

    private void Awake()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            CarController car = other.GetComponent<CarController>();
            car.curTrackZone = this;
            car.zonesPassed++;
             //&& car.zonesPassed > 50
            if (isGate)
            {
                if (GameManager.instance.isStartedLap)
                {
                    if (GameManager.instance.playersToBegin == GameManager.instance.cars.Count)
                    {
                        GameManager.instance.isStartedLap = false;
                    }
                    
                    //Debug.Log("MASUK STARTED");
                    //car.zonesPassed = 0;
                    car.curLap++;
                    GameManager.instance.CheckIsWinner(car);
                    car.curLap = Mathf.Clamp(car.curLap, car.curLap, GameManager.instance.lapsToWin);
                    GameManager.instance.UpdateLapsToWin(car);
                }
                else if (car.zonesPassed > 44)
                {
                    //Debug.Log("MASUK SINI");
                    car.zonesPassed = 0;
                    car.curLap++;
                    GameManager.instance.CheckIsWinner(car);
                    car.curLap = Mathf.Clamp(car.curLap, car.curLap, GameManager.instance.lapsToWin);
                    GameManager.instance.UpdateLapsToWin(car);
                }
                


            }

        }
    }
}
