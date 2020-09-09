using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IncomeText : MonoBehaviour {

    [SerializeField]
    private TMP_Text incomeText;
    GameController gameController;

    private int lastIncome;

    void Start() {
        gameController = FindObjectOfType<GameController>();
        lastIncome = gameController.income;
        incomeText.text = "Income " + gameController.income.ToString();
    }


    void Update() {
        if (lastIncome != gameController.income) {
            incomeText.text = "Income " + gameController.income.ToString();
        } else {
            lastIncome = gameController.income;
        }
    }
}
