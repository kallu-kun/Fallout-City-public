using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EmployedText : MonoBehaviour {

    [SerializeField]
    private TMP_Text employedText;
    private int lastEmployedCitizens;
    GameController gameController;

    
    void Start() {
        gameController = FindObjectOfType<GameController>();
        lastEmployedCitizens = gameController.employedCitizens;
        employedText.text = "Employed " + lastEmployedCitizens;
    }

    
    void Update() {
        if (lastEmployedCitizens != gameController.employedCitizens) {
            employedText.text = "Employed " + gameController.employedCitizens;
        } else {
            lastEmployedCitizens = gameController.employedCitizens;
        }
    }
}
