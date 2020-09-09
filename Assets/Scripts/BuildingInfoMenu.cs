using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingInfoMenu : MonoBehaviour {
    GameController gameController;

    GameObject residentialInfo;

    GameObject WorkPlaceInfo;

    GameObject needMenu;

    public GameObject citizenListMenu;

    Building activeBuilding;

    ObjectPool citizenInfoPool;

    public GameObject citizenInfoPrefab;
    public float tick;
    void Start() {
        gameController = GetComponent<GameController>();
        residentialInfo = GameObject.Find("Canvas").transform.Find("ResidentialInfo").gameObject;
        WorkPlaceInfo = GameObject.Find("Canvas").transform.Find("WorkPlaceInfo").gameObject;
        citizenListMenu = GameObject.Find("Canvas").transform.Find("CitizenListMenu").gameObject;
        needMenu = GameObject.Find("Canvas").transform.Find("NeedMenu").gameObject;

        citizenInfoPool = citizenListMenu.AddComponent<ObjectPool>();

        citizenInfoPool.PoolObjects(citizenInfoPrefab, 10);

        foreach ( GameObject obj in citizenInfoPool.objectPool) {
            obj.transform.SetParent(citizenListMenu.transform);
        }
    }

    void Update() {
        TickCounter();
        MouseInput();
        Placeholderjutut();
    }

    void Placeholderjutut() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if(GameObject.Find("Canvas").transform.Find("NeedMenu").gameObject.activeInHierarchy) {
                GameObject.Find("Canvas").transform.Find("NeedMenu").gameObject.SetActive(false);
                citizenListMenu.SetActive(true);
            } else {
                residentialInfo.SetActive(false);
                WorkPlaceInfo.SetActive(false);
                citizenListMenu.SetActive(false);
                activeBuilding = null;
                foreach (GameObject obj in citizenInfoPool.objectPool) {
                    obj.SetActive(false);
                }
            }
        }
    }

    void TickCounter() {
        if ( tick < 0) {
            TimeTick();
            tick = 4f;
        }
        tick -= Time.deltaTime;
    }
    void TimeTick(){
        UpdateBuildingInfo(true);
    }

    void MouseInput() {
        if(Input.GetMouseButtonDown(0) && !residentialInfo.activeInHierarchy && !WorkPlaceInfo.activeInHierarchy && !residentialInfo.activeInHierarchy
                    && !citizenListMenu.activeInHierarchy && !WorkPlaceInfo.activeInHierarchy && !needMenu.activeInHierarchy) {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Building building = gameController.GetBuildingFromPosition(mouseWorldPos);
            activeBuilding = building;
            UpdateBuildingInfo(false);

        }
    }
    void UpdateBuildingInfo(bool tick) {
        if(activeBuilding != null) {
            if(activeBuilding.buildingType == BuildingType.Residential) {
                ResidentalBuilding(tick);
            } else if (activeBuilding.buildingType == BuildingType.WorkSpace) {
                WorkPlaceBuilding(tick);
            }
        }
    }

    void ResidentalBuilding(bool tick) {
        if(!tick) {
            residentialInfo.SetActive(true);
        }
        residentialInfo.transform.Find("Name").gameObject.GetComponent<TextMeshProUGUI>().text = activeBuilding.name;
        residentialInfo.transform.Find("CitizenCount").gameObject.GetComponent<TextMeshProUGUI>().text = "citizen count: " + activeBuilding.citizens.Count +
            " / " + activeBuilding.maxCitizens;
        residentialInfo.transform.Find("Upkeep").gameObject.GetComponent<TextMeshProUGUI>().text = "Upkeep: " + activeBuilding.upkeep.ToString();
        
    }
    void WorkPlaceBuilding(bool tick) {
        if(!tick) {
            WorkPlaceInfo.SetActive(true);
        }
        WorkPlaceInfo.transform.Find("Name").gameObject.GetComponent<TextMeshProUGUI>().text = activeBuilding.name;
        WorkPlaceInfo.transform.Find("WorkerCount").gameObject.GetComponent<TextMeshProUGUI>().text = "Worker count: " + activeBuilding.citizens.Count +
            " / " + activeBuilding.maxCitizens;
        WorkPlaceInfo.transform.Find("Upkeep").gameObject.GetComponent<TextMeshProUGUI>().text = "Upkeep: " + activeBuilding.upkeep.ToString();
        WorkPlaceInfo.transform.Find("IncomePerCitizen").gameObject.GetComponent<TextMeshProUGUI>().text = "Income per Citizen: " + activeBuilding.incomePerCitizen.ToString();
        WorkPlaceInfo.transform.Find("Income").gameObject.GetComponent<TextMeshProUGUI>().text = "Total income: " + activeBuilding.GenerateIncome().ToString();

        WorkPlaceInfo.transform.Find("Need").gameObject.GetComponent<TextMeshProUGUI>().text = "Generates: " +activeBuilding.fulfilledNeed.ToString();

    }

    public void OpenMenu() {
        citizenListMenu.SetActive(true);
        residentialInfo.SetActive(false);
        WorkPlaceInfo.SetActive(false);
        int rowHeight = 0;

        GameObject info = citizenListMenu.transform.Find("Info").gameObject;
        info.GetComponent<TextMeshProUGUI>().text = "Name       " + "         Trait         " + "        Happiness         " + "         Needs";
        info.SetActive(true);
        
        foreach(Citizen citizen in activeBuilding.citizens) {
            GameObject citizenInfo = citizenInfoPool.GetObject();
            citizenInfo.transform.localPosition = new Vector3(0, 160 - rowHeight, 0);
            citizenInfo.transform.Find("Name").gameObject.GetComponent<TextMeshProUGUI>().text = citizen.name;
            citizenInfo.transform.Find("Trait").gameObject.GetComponent<TextMeshProUGUI>().text = citizen.trait.ToString();
            citizenInfo.transform.Find("Happiness").gameObject.GetComponent<TextMeshProUGUI>().text = citizen.happiness.ToString();
            citizenInfo.SetActive(true);
            rowHeight += 40;
            citizenInfo.transform.Find("NeedButton").gameObject.GetComponent<CitizenButtonInfo>().SetCitizen(citizen);
        }
    }

    public void CitizenNeedsMenu() {

    }
}
