using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobManager : MonoBehaviour
{
    public List<GameObject> jobs;

    public enum Job {
        Idle,
        Baker,
        Farmer
    }

    private Job job;

    private void Awake() {
        JobSwitch("Farmer");
    }

    public void JobSwitch(string job) {
        var jobEnum = Enum.Parse (typeof(Job), job);

        switch(jobEnum) {
            case Job.Idle:
                ChangeJobs(job);
                break;
            case Job.Baker:
                ChangeJobs(job);
                break;
            case Job.Farmer:
                ChangeJobs(job);
                break;
            default:
                break;
        }
    }

    private void ChangeJobs(string job) {
        var jobCount = Enum.GetNames(typeof(Job)).Length;
        
        for (int i = 0; i < jobCount; i++)
        {
            jobs[i].SetActive(false);
    
            if (jobs[i].tag == job) {
                jobs[i].SetActive(true);
                print(jobs[i] + "set active to true!");
            }
            else if (jobs[i].tag != job) {
                jobs[i].SetActive(false);
            } else if (jobs[i].tag == "Untagged") {
                Debug.Log(jobs[i] + "needs to be tagged with the enum job value");
            }

        }
    }

}
