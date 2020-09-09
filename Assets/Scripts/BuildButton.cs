using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildButton : MonoBehaviour
{
    public Building building;

    private BuildingPlacement buildingPlacement;

    private GameObject buildingHoverInfoText;
    void Start() {
        GetComponent<Button>().onClick.AddListener(ButtonClicked);
        buildingPlacement = FindObjectOfType<BuildingPlacement>();
        buildingHoverInfoText = GameObject.Find("Canvas").gameObject.transform.Find("BuildingHoverInfoText").gameObject;
    }

    void Update() {
        
    }
    void ButtonClicked() {
        buildingPlacement.rendererOn = true;
        buildingPlacement.buildingRenderer.GetComponent<SpriteRenderer>().enabled = true;
        buildingPlacement.building = building;
        buildingPlacement.buildingRenderer.GetComponent<SpriteRenderer>().sprite = building.sprite;
    }

    public void CursorHover() {
        buildingHoverInfoText.SetActive(true);
        buildingHoverInfoText.transform.Find("Name").gameObject.GetComponent<TextMeshProUGUI>().text = building.name;
        buildingHoverInfoText.transform.Find("Cost").gameObject.GetComponent<TextMeshProUGUI>().text = "Cost: " + building.buildingCost.ToString();
        buildingHoverInfoText.transform.Find("Upkeep").gameObject.GetComponent<TextMeshProUGUI>().text = "Upkeep cost: " + building.upkeep.ToString();
        buildingHoverInfoText.transform.Find("IncomePerCitizen").gameObject.GetComponent<TextMeshProUGUI>().text = "Income per citizen: " + building.incomePerCitizen.ToString();
        buildingHoverInfoText.transform.Find("CitizenCapacity").gameObject.GetComponent<TextMeshProUGUI>().text = "Max citizen: " + building.maxCitizens.ToString();
        buildingHoverInfoText.transform.Find("Need").gameObject.GetComponent<TextMeshProUGUI>().text = "Generates: " + building.fulfilledNeed.ToString();

    }

    public void CursorHoverExit() {
        buildingHoverInfoText.SetActive(false);
    }
    
}
