using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenTesti : MonoBehaviour {

    

    // Start is called before the first frame update
    void Start() {
        TextAsset lastnames = Resources.Load<TextAsset>("lastnames");

        string[] lastname = lastnames.text.Split(new char[] { '\n' });

        for (int i = 1; i < 10; i++) {
            Debug.Log(lastname[i]);
        }
    
    }

    // Update is called once per frame
    void Update() {
        
    }
}
