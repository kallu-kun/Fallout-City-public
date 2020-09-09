using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyText : MonoBehaviour {
    
    [SerializeField]
    private TMP_Text moneyText;
    GameController gameController;

    private int lastMoney;

    void Start() {
        gameController = FindObjectOfType<GameController>();
        lastMoney = gameController.money;
        moneyText.text = "Money " + gameController.money.ToString();
    }

    
    void Update() {
        if (lastMoney != gameController.money) {
            moneyText.text = "Money " + gameController.money.ToString();
        } else {
            lastMoney = gameController.money;
        }
    }
}
