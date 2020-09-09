using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour {

    public Building building;

    public bool rendererOn = false;

    GameController gameController;

    [SerializeField]
    public GameObject rendererPrefab;

    public GameObject buildingRenderer;

    private BuildingData buildingData;
    
    BuildPopUpText buildPopUpText;
    private GameObject buildWarning;
    public float warningTimer;

    void Start() {
        buildPopUpText = FindObjectOfType<BuildPopUpText>();
        gameController = FindObjectOfType<GameController>();
        buildingData = GetComponent<BuildingData>();
        buildWarning = GameObject.Find("Canvas").transform.Find("PopUpText").transform.Find("BuildWarning").gameObject;
        buildingRenderer = Instantiate(rendererPrefab, new Vector3(0,0,0), Quaternion.identity);
    }

    void Update() {
        if (Input.GetMouseButtonDown(0) && rendererOn == true) {
            Build();
            if (building.buildingType != BuildingType.Road) {
                RemoveIcon();
            }
        }

        FollowMouse();
        CancelBuild();
        Timer();
    }

    private void FollowMouse() {
        buildingRenderer.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //buildingRenderer.transform.position = new Vector3(RoundUpwardsToNearest(buildingRenderer.transform.position.x, 0.5f), RoundUpwardsToNearest(buildingRenderer.transform.position.y, 0.25f), 0);
        buildingRenderer.transform.position = RoundPositionToGrid(buildingRenderer.transform.position);
        Debug.Log(buildingRenderer.transform.position);
    }

    private float RoundUpwardsToNearest(float roundable, float roundInterval) {
        if (roundable > 0) {
            roundable = Mathf.Ceil(roundable / roundInterval) * roundInterval;
        } else if (roundable < 0) {
            roundable = Mathf.Floor(roundable / roundInterval) * roundInterval;
        }
        return roundable;
    }

    private Vector3 RoundPositionToGrid(Vector3 position) {
        position.x = Mathf.Round(position.x / 0.5f) * 0.5f;

        position.y = Mathf.Round(position.y / 0.25f) * 0.25f;

        position.z = 0;

        Debug.Log(position.x % 1);

        return position;
    }

    private void RemoveIcon() {
        buildingRenderer.GetComponent<Renderer>().enabled = false;
        rendererOn = false;
    }

    private void Build() {
        Vector3Int coordinate = gameController.buildingMap.WorldToCell(buildingRenderer.transform.position);

        if (!gameController.IsPositionOccupied(coordinate.x, coordinate.y, building) && gameController.NextToARoad(coordinate.x, coordinate.y, building)) {
            gameController.SetBuilding(building.Clone(), coordinate.x, coordinate.y);
            Debug.Log("Building placed at " + coordinate);
        } else {
            WarningText();
            // buildWarning.GetComponent<TMPro.TMP_Text>().enabled = true;
            Debug.Log("Can't build there");
        }
    }

    private void CancelBuild() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            RemoveIcon();
        }
    }

    private void WarningText() {
        warningTimer = 1f;
        buildWarning.GetComponent<TMPro.TMP_Text>().enabled = true;
        Timer(); 
    }

    private void Timer() {
        warningTimer -= Time.deltaTime;
        if (warningTimer <= 0) {
            buildWarning.GetComponent<TMPro.TMP_Text>().enabled = false;
        }
    }
}
