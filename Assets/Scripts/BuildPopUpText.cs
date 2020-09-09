using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildPopUpText : MonoBehaviour {
    
    [SerializeField]
    private TMP_Text startText;

    public string popUptext;

    void Start() {
        startText.text = "Can't build there!";
    }

    
    void Update() {
        
    }

    public void ShowPopUp() {
        GetComponent<TextMeshProUGUI>().text = popUptext;
    }

    public void ChangeText(string text) {
        popUptext = text;
    }
}
