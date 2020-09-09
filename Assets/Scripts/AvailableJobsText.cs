using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AvailableJobsText : MonoBehaviour {

    [SerializeField]
    private TMP_Text availableJobsText;

    GameController gameController;

    private int lastAvailableJobs;
    
    void Start() {
        gameController = FindObjectOfType<GameController>();
        lastAvailableJobs = gameController.jobsAvailable;
        availableJobsText.text = "Jobs " + lastAvailableJobs;
    }

    void Update() {
        if (lastAvailableJobs != gameController.jobsAvailable) {
            availableJobsText.text = "Jobs " + gameController.jobsAvailable;
        } else {
            lastAvailableJobs = gameController.jobsAvailable;
        }
    }
}
