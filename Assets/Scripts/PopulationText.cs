using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopulationText : MonoBehaviour {

    [SerializeField]
    private TMP_Text startText;
    GameController gameController;
    
    private int lastPopulation;
    private int lastPopulationCap;

    void Start() {
        gameController = FindObjectOfType<GameController>();
        lastPopulation = gameController.population;
        lastPopulationCap = gameController.populationCap;
        startText.text = "Population " + gameController.population.ToString() + "/" + gameController.populationCap.ToString();
    }

    void Update() {
        if (lastPopulation != gameController.population || lastPopulationCap != gameController.populationCap) {
            startText.text = "Population " + gameController.population.ToString() + "/" + gameController.populationCap.ToString();
        } else {
            lastPopulation = gameController.population;
            lastPopulationCap = gameController.populationCap;
        }
    }
}
