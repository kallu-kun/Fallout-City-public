using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HappinessText : MonoBehaviour {
    
    [SerializeField]
    private TMP_Text happinessText;

    int lastHappiness;
    int happiness;

    GameController gameController;

    void Start() {
        gameController = FindObjectOfType<GameController>();
        happinessText.text = "Happiness ";
    }

    void Update() {
        happiness = gameController.happiness;

        if (lastHappiness != happiness) {
            happinessText.text = "Happiness " + happiness;
        }
        lastHappiness = happiness;
    }
}
