using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CitizenButtonInfo : MonoBehaviour
{
    public Citizen citizen;

    public GameObject citizenNeedMenu;

    public GameObject needMenuPrefab;

    public BuildingInfoMenu buildingInfoMenu;

    GameObject citizenListMenu;

    void Start()
    {
        citizenNeedMenu = GameObject.Find("Canvas").transform.Find("NeedMenu").gameObject;
        GetComponent<Button>().onClick.AddListener(Clicked);
        citizenListMenu = GameObject.Find("Canvas").transform.Find("CitizenListMenu").gameObject;
    }

    void Update()
    {
        
    }

    public void SetCitizen(Citizen citizen) {
        this.citizen = citizen;
    }

    public void Clicked() {
        citizenNeedMenu.SetActive(true);

        citizenListMenu.SetActive(false);

        citizenNeedMenu.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = citizen.name;

        
        if(citizen.needs[0].active) {
            citizenNeedMenu.transform.Find("Food").GetComponent<TextMeshProUGUI>().text = "Food " + citizen.needs[0].amountReceived.ToString();
        } else {citizenNeedMenu.transform.Find("Food").GetComponent<TextMeshProUGUI>().text = "Food " + "-" ;}

        if(citizen.needs[1].active) {
            citizenNeedMenu.transform.Find("Water").GetComponent<TextMeshProUGUI>().text = "Water " + citizen.needs[1].amountReceived.ToString();
        } else {citizenNeedMenu.transform.Find("Water").GetComponent<TextMeshProUGUI>().text = "Water " + "-" ;}

        if(citizen.needs[2].active) {
            citizenNeedMenu.transform.Find("Safety").GetComponent<TextMeshProUGUI>().text = "Safety " + citizen.needs[2].amountReceived.ToString();
        } else {citizenNeedMenu.transform.Find("Safety").GetComponent<TextMeshProUGUI>().text = "Safety " + "-" ;}

        if(citizen.needs[3].active) {
            citizenNeedMenu.transform.Find("Health").GetComponent<TextMeshProUGUI>().text = "Health " + citizen.needs[3].amountReceived.ToString();
        } else {citizenNeedMenu.transform.Find("Health").GetComponent<TextMeshProUGUI>().text = "Health " + "-" ;}

        if(citizen.needs[4].active) {
            citizenNeedMenu.transform.Find("Leisure").GetComponent<TextMeshProUGUI>().text = "Leisure " + citizen.needs[4].amountReceived.ToString();
        } else {citizenNeedMenu.transform.Find("Leisure").GetComponent<TextMeshProUGUI>().text = "Leisure " + "-" ;}

        if(citizen.needs[5].active) {
            citizenNeedMenu.transform.Find("Culture").GetComponent<TextMeshProUGUI>().text = "Culture " + citizen.needs[5].amountReceived.ToString();
        } else {citizenNeedMenu.transform.Find("Culture").GetComponent<TextMeshProUGUI>().text = "Culture " + "-" ;}
    }
}
