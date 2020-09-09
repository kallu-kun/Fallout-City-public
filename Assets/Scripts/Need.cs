using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Need {

    public Needs name;

    public int amountReceived;

    public int maxAmount;

    public bool active;

    public Need(Needs name) {
        this.name = name;
        amountReceived = 0;
        maxAmount = 100;
        active = false;
    }
}