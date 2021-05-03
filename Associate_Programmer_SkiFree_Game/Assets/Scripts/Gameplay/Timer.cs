using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float totalSeconds = 0;

    public float SecondsLeft
    {
        get
        {
            return totalSeconds;
        }
    }

    bool started = false;
    bool running = false;

    public float Duration
    {
        set
        {
            if (!running)
            {
                totalSeconds = value;
            }
        }
    }

    public bool Finished
    {
        get { return started && !running; }
    }
    public bool Running
    {
        get { return running; }
    }


    public void Run()
    {
        if (totalSeconds > 0)
        {
            started = true;
            running = true;
        }
    }

    private void Update()
    {
        if (running)
        {
            totalSeconds -= Time.deltaTime;
            if (totalSeconds <= 0)
            {
                running = false;
            }
        }
    }

    public void Stop()
    {
        running = false;
        started = false;
    }


}
