using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingData : MonoBehaviour {

    public TileBase[] roadTiles;

    public TileBase[] residentialTiles;

    public TileBase[] workPlaceTiles;

    public TileBase[] educationalTiles;


    public Building[] roadBuildings;

    public Building[] residentialBuildings;

    public Building[] workPlaceBuildings;

    public Building[] educationalBuildings;


    public Sprite[] roadSprites;

    public Sprite[] residentalSprites;

    public Sprite[] workPlaceSprites;

    public Sprite[] educationalSprites;

    void Awake() {
        CreateBuildingArrays();
    }

    private void CreateBuildingArrays() {
        CreateRoadArray();
        CreateResidentialArray();
        CreateWorkPlaceArray();
        CreateEducationalArray();
    }

    private void CreateRoadArray() {
        roadBuildings = new Building[4];

        roadBuildings[0] = new Building("CrackedRoad", roadTiles[0], roadSprites[0], 1);
        roadBuildings[1] = new Building("Road", roadTiles[1], roadSprites[1], 2);
        roadBuildings[2] = new Building("RightRoad", roadTiles[2], roadSprites[2], 3);
        roadBuildings[3] = new Building("LeftRoad", roadTiles[3], roadSprites[3], 3);

        foreach (Building road in roadBuildings) {
            road.SetActive(true);
        }
    }

    private void CreateResidentialArray() {
        residentialBuildings = new Building[2];

        residentialBuildings[0] = new Building("Shack", residentialTiles[0], residentalSprites[0], BuildingType.Residential, 5, 5, 0, 50, 2);
        residentialBuildings[1] = new Building("Big Shack", residentialTiles[1], residentalSprites[1], BuildingType.Residential, 15, 15, 0, 200, 3);
    }

    private void CreateWorkPlaceArray() {
        workPlaceBuildings = new Building[7];

        workPlaceBuildings[0] = new Building("CoalMine", workPlaceTiles[0], workPlaceSprites[0], BuildingType.WorkSpace, 7, 25, 12, 100, 2, Trait.Sturdy, Trait.Charming, Needs.None);
        workPlaceBuildings[1] = new Building("HunterShack", workPlaceTiles[1], workPlaceSprites[1], BuildingType.WorkSpace, 5, 15, 7, 100, 2, Trait.Swift, Trait.Sophisticated, Needs.Food);
        workPlaceBuildings[2] = new Building("Well", workPlaceTiles[4], workPlaceSprites[4], BuildingType.WorkSpace, 3, 5, 0, 50, 2, Trait.Gentle, Trait.Sturdy, Needs.Water);
        workPlaceBuildings[3] = new Building("WatchTower", workPlaceTiles[2], workPlaceSprites[2], BuildingType.WorkSpace, 4, 30, 10, 200, 3, Trait.Aesthetic, Trait.Hygienic, Needs.Safety);
        workPlaceBuildings[4] = new Building("MedicTent", workPlaceTiles[5], workPlaceSprites[5], BuildingType.WorkSpace, 5, 30, 10, 150, 2, Trait.Hygienic, Trait.Aesthetic, Needs.Health);
        workPlaceBuildings[5] = new Building("Pub", workPlaceTiles[3], workPlaceSprites[3], BuildingType.WorkSpace, 6, 45, 20, 250, 3, Trait.Charming, Trait.Gentle, Needs.Leisure);
        workPlaceBuildings[6] = new Building("Museum", workPlaceTiles[6], workPlaceSprites[6], BuildingType.WorkSpace, 10, 80, 8, 500, 4, Trait.Sophisticated, Trait.Swift, Needs.Culture);

    }

    private void CreateEducationalArray() {
        educationalBuildings = new Building[1];
    }

    public Building GetBuilding(string name) {
        for (int i = 0; i < roadBuildings.Length; i++) {
            if (roadBuildings[i].name.Equals(name)) {
                return roadBuildings[i].Clone();
            }
        }
        for (int i = 0; i < residentialBuildings.Length; i++) {
            if (residentialBuildings[i].name.Equals(name)) {
                return residentialBuildings[i].Clone();
            }
        }
        for (int i = 0; i < workPlaceBuildings.Length; i++) {
            if (workPlaceBuildings[i].name.Equals(name)) {
                return workPlaceBuildings[i].Clone();
            }
        }
        for (int i = 0; i < educationalBuildings.Length; i++) {
            if (educationalBuildings[i].name.Equals(name)) {
                return educationalBuildings[i].Clone();
            }
        }

        return null;
    }
}