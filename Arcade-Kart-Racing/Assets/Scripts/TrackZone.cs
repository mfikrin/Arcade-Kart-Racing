using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackZone : MonoBehaviour
{
    public bool isGate;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            CarController car = other.GetComponent<CarController>();
            car.curTrackZone = this;
            car.zonesPassed++;

            if (isGate)
            {
                car.curLap++;
                GameManager.instance.CheckIsWinner(car);
                car.curLap = Mathf.Clamp(car.curLap, car.curLap, GameManager.instance.lapsToWin);
                GameManager.instance.UpdateLapsToWin(car);
               
            }

        }
    }
}
