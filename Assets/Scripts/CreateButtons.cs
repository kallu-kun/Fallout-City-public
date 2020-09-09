using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateButtons : MonoBehaviour {

    private GameObject roadMenu;

    private GameObject residentialMenu;

    private GameObject workPlaceMenu;

    private BuildingData buildingData;

    [SerializeField]
    private GameObject houseButton;

    List<GameObject> buttons;

    void Start() {
        roadMenu = GameObject.Find("Canvas").transform.Find("RoadMenu").gameObject;
        residentialMenu = GameObject.Find("Canvas").transform.Find("ResidentialMenu").gameObject;
        workPlaceMenu = GameObject.Find("Canvas").transform.Find("IncomeMenu").gameObject;


        buildingData = GetComponent<BuildingData>();

        buttons = new List<GameObject>();
        GenerateButtons();
    }

    void Update() {
        
    }

    public void GenerateButtons() {
        DestroyButtons();

        buildingData.workPlaceBuildings[0].SetActive(true);
        buildingData.residentialBuildings[0].SetActive(true);
        buildingData.residentialBuildings[1].SetActive(true);

        int roadButtonXpos = 0;
        int residentialButtonXpos = 0;
        int workPlaceButtonXpos = 0;

        foreach(Building building in buildingData.roadBuildings) {
            if (building.active) {
                GameObject button = Instantiate(houseButton, new Vector3(400 + roadButtonXpos, -50, 0), Quaternion.identity);
                roadButtonXpos += 100;
                button.GetComponent<BuildButton>().building = building;
                button.GetComponent<Image>().sprite = building.sprite;
                button.transform.SetParent(roadMenu.transform, false);
                buttons.Add(button);
            }
        }
        foreach(Building building in buildingData.residentialBuildings) {
            if (building.active) {
                GameObject button = Instantiate(houseButton, new Vector3(400 + residentialButtonXpos, -50, 0), Quaternion.identity);
                residentialButtonXpos += 100;
                button.GetComponent<BuildButton>().building = building;
                button.GetComponent<Image>().sprite = building.sprite;
                button.transform.SetParent(residentialMenu.transform, false);
                buttons.Add(button);
            }
        }
        foreach(Building building in buildingData.workPlaceBuildings) {
            if (building.active) {
                GameObject button = Instantiate(houseButton, new Vector3(400 + workPlaceButtonXpos, -50, 0), Quaternion.identity);
                workPlaceButtonXpos += 100;
                button.GetComponent<BuildButton>().building = building;
                button.GetComponent<Image>().sprite = building.sprite;
                //button.GetComponent<RectTransform>().sizeDelta = new Vector2();
                button.transform.SetParent(workPlaceMenu.transform, false);
                buttons.Add(button);
            }
        }
    }

    private void DestroyButtons() {
        if (buttons.Count > 0) {
            foreach (GameObject button in buttons) {
                Destroy(button);
            }
        }
    }
}
