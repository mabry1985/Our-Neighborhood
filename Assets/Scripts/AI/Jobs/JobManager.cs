using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobManager : MonoBehaviour
{

    private enum Job {
        Idle,
        Baker,
        Farmer
    }

    private Job job;

    private void Start() {
            job = Job.Idle;
    }

    private void SwitchJobs(Job job) {
        switch(job) {
            case Job.Idle:
                break;
            case Job.Baker:
                break;
            case Job.Farmer:
                break;
            default:
                break;
        }
    }

    

}
