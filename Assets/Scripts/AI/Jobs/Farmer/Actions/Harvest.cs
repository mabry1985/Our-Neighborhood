﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvest : GoapAction
{

    bool completed = false;
    float startTime = 0;
    public float workDuration = 2; // seconds

    public Harvest()
    {
        addEffect("hasWheat", true);
        name = "Harvest";
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Farm Door");
    }

    public override void reset()
    {
        completed = false;
        startTime = 0;
    }

    public override bool isDone()
    {
        return completed;
    }

    public override bool requiresInRange()
    {
        return true;
    }

    public override bool checkProceduralPrecondition(GameObject agent)
    {
        return true;
    }

    public override bool perform(GameObject agent)
    {
        if (startTime == 0)
        {
            Debug.Log("Starting: " + name);
            startTime = Time.time;
        }

        if (Time.time - startTime > workDuration)
        {
            Debug.Log("Finished: " + name);
            completed = true;
        }
        return true;
    }

}
