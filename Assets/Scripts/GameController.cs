using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour {

    public Tilemap groundMap;

    public Tilemap buildingMap;

    TileData tileData;
    BuildingData buildingData;

    Tile[,] map;
    public int size;

    List<Citizen> citizens;
    List<Building> residentialBuildings;
    List<Building> workSpaceBuildings;

    bool[] activeNeeds;

    public float tinyTick;
    public float smallTick;

    public int population;
    public int populationCap;
    public int employedCitizens;
    public int unEmployedCitizens;
    public int jobsAvailable;

    public int money;
    public int income;
    public int happiness;

    void Start() {
        size = 64;

        tileData = GetComponent<TileData>();
        buildingData = GetComponent<BuildingData>();

        CreateTileArray();
        RenderGround();

        money = 1000;
        citizens = new List<Citizen>();
        residentialBuildings = new List<Building>();
        workSpaceBuildings = new List<Building>();

        activeNeeds = new bool[System.Enum.GetNames(typeof(Needs)).Length - 1];
    }

    void Update() {
        TickCounter();
    }

    public Building GetBuildingFromPosition(Vector3 mouseWorldPos) {
        Vector3Int coordinate = buildingMap.WorldToCell(mouseWorldPos);
        Building building = null;
        if(coordinate.x > 0 && coordinate.x < size && coordinate.y > 0 && coordinate.y < size) {
            building = map[coordinate.x, coordinate.y].building;
        }
        return building;
    }

    public void TickCounter() {
        if (tinyTick < 0) {
            TinyTick();
        }
        if (smallTick < 0) {
            SmallTick();
        }

        tinyTick -= Time.deltaTime;
        smallTick -= Time.deltaTime;
    }

    private void TinyTick() {
        tinyTick = 1.0f;

        UpdatePopulationStats();
        CalculateIncome();
        DistributeNeeds();
    }

    private void SmallTick() {
        smallTick = 3.0f;

        SpawnCitizens();
        AddWorkers();
        UnlockNeedsAndBuildings();
    }

    private void CalculateIncome() {
        int income = 0;
        foreach (Building building in workSpaceBuildings) {
            income += building.GenerateIncome();
            income -= building.upkeep;
        }

        foreach (Building building in residentialBuildings) {
            income -= building.upkeep;
        }

        this.income = income;
        money += income;
    }

    private void DistributeNeeds() {
        int[] needValues = new int[System.Enum.GetNames(typeof(Needs)).Length - 1];

        foreach (Building building in workSpaceBuildings) {
            if (building.fulfilledNeed != Needs.None) {
                needValues[(int)building.fulfilledNeed] += building.GenerateNeed();
            }
        }

        int[] needValuePerCitizen = new int[needValues.Length];

        for (int i = 0; i < needValuePerCitizen.Length; i++) {
            needValuePerCitizen[i] = (needValues[i] + 100) / (population + 1);
            if (needValuePerCitizen[i] > 100) {
                needValuePerCitizen[i] = 100;
            }
        }

        foreach (Citizen citizen in citizens) {
            for (int i = 0; i < citizen.needs.Length; i++) {
                citizen.needs[i].amountReceived = needValuePerCitizen[i];
            }
        }
    }

    private void UpdatePopulationStats() {
        int populationCap = 0;
        int population = 0;
        int employedCitizens = 0;
        int unEmployedCitizens = 0;
        int jobsAvailable = 0;
        int totalHappiness = 50;

        foreach(Building building in residentialBuildings) {
            populationCap += building.maxCitizens;
            population += building.CitizenCount();
        }

        foreach (Building building in workSpaceBuildings) {
            jobsAvailable += building.maxCitizens - building.CitizenCount();
        }

        foreach (Citizen citizen in citizens) {
            if (citizen.employed) {
                employedCitizens += 1;
            } else {
                unEmployedCitizens += 1;
            }

            citizen.CalculateHappiness();
            totalHappiness += citizen.happiness;
        }

        this.population = population;
        this.populationCap = populationCap;
        this.employedCitizens = employedCitizens;
        this.unEmployedCitizens = unEmployedCitizens;
        this.jobsAvailable = jobsAvailable;
        happiness = totalHappiness / (population + 1);
    }

    private void SpawnCitizens() {
        int spawnAmount = 1;

        if (populationCap - population > 15) {
            spawnAmount = 2;
        } else if (populationCap - population > 30) {
            spawnAmount = 3;
        }

        print(spawnAmount);

        for (int i = 0; i < spawnAmount; i++) {
            int citizenSpawn = Random.Range(0, 100 - happiness);

            if (population == 0) {
                citizenSpawn = 0;
            }

            if (citizenSpawn < 15 && population < populationCap) {
                Citizen citizen = new Citizen();
                citizens.Add(citizen);
                GetLeastInhabitedBuilding().AddCitizen(citizen);

                citizen.PrintCitizenProperties();
            }
        }
    }

    private void AddWorkers() {
        foreach (Building building in workSpaceBuildings) {
            if (building.CitizenCount() < building.maxCitizens) {
                Citizen nWorker = GetUnemployedCitizen();

                if (nWorker != null) {
                    building.AddCitizen(nWorker);
                    nWorker.employed = true;
                    Debug.Log(nWorker.name + " is employed at " + building.name);
                }
            }
        }
    }

    private void UnlockNeedsAndBuildings() {
        int[] needUnlockPopulations = { 7, 15, 25, 40, 60, 90 };

        for (int i = 0; i < System.Enum.GetNames(typeof(Needs)).Length - 1; i++) {
            if (!activeNeeds[i] && population >= needUnlockPopulations[i]) {
                activeNeeds[i] = true;
                buildingData.workPlaceBuildings[i + 1].SetActive(true);
                GetComponent<CreateButtons>().GenerateButtons();
            }
        }

        foreach (Citizen citizen in citizens) {
            for (int i = 0; i < activeNeeds.Length; i++) {
                if (activeNeeds[i]) {
                    citizen.EnableNeed((Needs)i);
                }
            }
        }
    }

    void CreateTileArray() {
        map = new Tile[size, size];

        for (int x = 0; x < size; x++) {
            for (int y = 0; y < size; y++) {
                Tile tile = new Tile(TileType.Dirt3, x, y);
                map[x, y] = tile;
            }
        }

        int dirtPatchesAmount = Random.Range(10, 16);
        int bigPatches = 4;

        for (int i = 0; i < dirtPatchesAmount; i++) {
            int patchRadius;

            if (bigPatches > 0) {
                patchRadius = Random.Range(12, 18);
                bigPatches--;
            } else {
                patchRadius = Random.Range(4, 10);
            }

            int maxX = size / ((i % 2) + 1);
            int minX = maxX - size / 2;

            int maxY = size / ((i % 4) / 2 + 1);
            int minY = maxY - size / 2;

            int patchCenterX = Random.Range(minX, maxX);
            int patchCenterY = Random.Range(minY, maxY);


            int patchIndex = Random.Range(0, 5);
            TileType patchType = TileType.Dirt1;

            switch (patchIndex) {
                case 0: patchType = TileType.Dirt1; break;
                case 1: patchType = TileType.Dirt1; break;
                case 2: patchType = TileType.Dirt2; break;
                case 3: patchType = TileType.Dirt2; break;
                case 4: patchType = TileType.Mud; break;
            }

            for (int x = patchCenterX - patchRadius; x < patchCenterX + patchRadius; x++) {
                for (int y = patchCenterY - patchRadius; y < patchCenterY + patchRadius; y++) {
                    if (x >= 0 && x < size && y >= 0 && y < size) {

                        int centerDistance = Mathf.Abs(patchCenterX - x) + Mathf.Abs(patchCenterY - y);
                        float randomBlend = Random.Range(0, centerDistance);

                        if (centerDistance <= patchRadius + 1 && randomBlend < 2.0f + patchRadius / 2.0f) {
                            map[x, y].tileType = patchType;
                        }
                    }
                }
            }
        }
    }

    void RenderGround() {
        for (int x = 0; x < size; x++) {
            for (int y = 0; y < size; y++) {
                if (map[x,y].tileType == TileType.Dirt1) {
                    groundMap.SetTile(new Vector3Int(x, y, 0), tileData.dirt1);
                } else if (map[x, y].tileType == TileType.Dirt2) {
                    groundMap.SetTile(new Vector3Int(x, y, 0), tileData.dirt2);
                } else if (map[x, y].tileType == TileType.Dirt3) {
                    groundMap.SetTile(new Vector3Int(x, y, 0), tileData.dirt3);
                } else if (map[x, y].tileType == TileType.Mud) {
                    groundMap.SetTile(new Vector3Int(x, y, 0), tileData.mud);
                }
            }
        }

        int roadXLoc = Random.Range(5, size - 5);

        for (int i = 14; i >= 0; i--) {
            SetBuilding(buildingData.roadBuildings[3], roadXLoc, i);
        }
    }

    public void SetBuilding(Building building, int xPos, int yPos) {
        buildingMap.SetTile(new Vector3Int(xPos, yPos, 0), building.tileBase);
        money -= building.buildingCost;

        for (int x = xPos; x > xPos - building.borderLength; x--) {
            for (int y = yPos; y > yPos - building.borderLength; y--) {
                map[x, y].AddBuilding(building);
            }
        }

        if (building.buildingType.Equals(BuildingType.Residential)) {
            residentialBuildings.Add(building);
        } else if (building.buildingType.Equals(BuildingType.WorkSpace)) {
            workSpaceBuildings.Add(building);
        }
    }

    public bool IsPositionOccupied(int xPos, int yPos, Building building) {
        bool result = false;

        for (int x = xPos; x > xPos - building.borderLength; x--) {
            for (int y = yPos; y > yPos - building.borderLength; y--) {
                if (map[x, y].building != null || x < 0 || x >= size || y < 0 || y >= size) {
                    result = true;
                }
            }
        }

        return result;
    }

    public bool NextToARoad(int xPos, int yPos, Building building) {
        bool result = false;

        List<Tile> checkedTiles = new List<Tile>();

        int minX = xPos - building.borderLength;
        int minY = yPos - building.borderLength;

        int maxX = xPos + 1;
        int maxY = yPos + 1;

        for (int x = maxX; x >= minX; x--) {
            for (int y = maxY; y >= minY; y--) {
                if ((x == maxX || x == minX) && y != maxY && y != minY) {
                    checkedTiles.Add(map[x, y]);
                }
                if ((y == maxY || y == minY) && x != maxX && x != minX) {
                    checkedTiles.Add(map[x, y]);
                }
            }
        }

        foreach (Tile tile in checkedTiles) {
            if (tile.building != null && tile.building.buildingType.Equals(BuildingType.Road)) {
                result = true;
                break;
            }
            print(tile.position.x + " " + tile.position.y);
        }

        return result;
    }

    private Building GetLeastInhabitedBuilding() {
        Building result = null;

        for (int i = 0; i < residentialBuildings.Count; i++) {
            Building building = residentialBuildings[i];
            if (building.HasRoom()) {
                if (result == null) {
                    result = building;
                } else if (building.CitizenCount() < result.CitizenCount()) {
                    result = building;
                }
            }
        }

        return result;
    }

    private Citizen GetUnemployedCitizen() {
        Citizen result = null;

        foreach (Citizen citizen in citizens) {
            if (!citizen.employed) {
                result = citizen;
                break;
            }
        }

        return result;
    }
}