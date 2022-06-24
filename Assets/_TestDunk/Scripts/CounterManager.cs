using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterManager : MonoBehaviour
{
    public static CounterManager instance;
    public int ringsCounter;
    public int starCounter;
    public int bounceCounter;
    public Ball ball;

    void Start()
    {
        instance = this;
    }
    public void SetStarCounter()
    {
        starCounter++;
    }

    public void SetBounceCounter()
    {
        bounceCounter++;
    }

    public void SetNumberRingInBallCatcher(int value)
    {
        if (ringsCounter <= value)
        {
            ringsCounter = ball.ringNumber;
            ball.ringNumber++;
            RingSpawner.instance.SpawnRing();
            MainCanvas.instance.UpdateScore();
        }
    }
}
