using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Building {

    public string name;

    public TileBase tileBase;

    public Sprite sprite;

    public BuildingType buildingType { get; protected set; }

    // Residents, workers or students of the building
    public List<Citizen> citizens;

    public int maxCitizens;

    public int upkeep;

    public int incomePerCitizen;

    public int needGeneratedPerCitizen;

    public int buildingCost;

    public int borderLength;

    public Trait beneficialTrait;

    public Trait detrimentalTrait;

    public Needs fulfilledNeed;

    public bool active;


    // Tile where the building exists
    Tile tile;

    public Building(string name, TileBase tileBase, Sprite sprite, BuildingType buildingType,
                        int maxCitizens, int upkeep, int incomePerCitizen, int buildingCost, int borderLength) {
        this.name = name;
        this.tileBase = tileBase;
        this.sprite = sprite;
        this.buildingType = buildingType;
        this.maxCitizens = maxCitizens;
        this.upkeep = upkeep;
        this.incomePerCitizen = incomePerCitizen;
        this.buildingCost = buildingCost;
        this.borderLength = borderLength;

        fulfilledNeed = Needs.None;

        citizens = new List<Citizen>();
    }

    public Building(string name, TileBase tileBase, Sprite sprite, BuildingType buildingType,
                        int maxCitizens, int upkeep, int incomePerCitizen, int buildingCost, int borderLength,
                            Trait beneficialTrait, Trait detrimentalTrait, Needs fulfilledNeed) {
        this.name = name;
        this.tileBase = tileBase;
        this.sprite = sprite;
        this.buildingType = buildingType;
        this.maxCitizens = maxCitizens;
        this.upkeep = upkeep;
        this.incomePerCitizen = incomePerCitizen;
        this.buildingCost = buildingCost;
        this.borderLength = borderLength;
        this.beneficialTrait = beneficialTrait;
        this.detrimentalTrait = detrimentalTrait;
        this.fulfilledNeed = fulfilledNeed;

        needGeneratedPerCitizen = 800;

        citizens = new List<Citizen>();
    }

    //GonstruKtor for Road
    public Building(string name, TileBase tileBase, Sprite sprite, int buildingCost) {
        this.name = name;
        this.tileBase = tileBase;
        this.sprite = sprite;
        this.buildingCost = buildingCost;
        this.buildingType = BuildingType.Road;

        fulfilledNeed = Needs.None;

        borderLength = 1;
    }

    public int GenerateIncome() {
        float income = 0;

        foreach (Citizen citizen in citizens) {
            if (citizen.trait.Equals(beneficialTrait)) {
                income += incomePerCitizen * 1.4f;
            } else if (citizen.trait.Equals(detrimentalTrait)) {
                income += incomePerCitizen * 0.7f;
            } else {
                income += incomePerCitizen;
            }

            income *= citizen.happiness / 100.0f;
        }

        return Mathf.RoundToInt(income);
    }

    public int GenerateNeed() {
        return citizens.Count * needGeneratedPerCitizen;
    }

    public void AddCitizen(Citizen citizen) {
        if (citizens.Count < maxCitizens) {
            citizens.Add(citizen);
        }
    }

    public int CitizenCount() {
        return citizens.Count;
    }

    public bool HasRoom() {
        if (citizens.Count < maxCitizens) {
            return true;
        } else {
            return false;
        }
    }

    public Building Clone() {
        if(buildingType.Equals(BuildingType.Road)) {
            return new Building(name, tileBase, sprite, buildingCost);
        } else if(buildingType.Equals(BuildingType.Residential)) {
            return new Building(name, tileBase, sprite, buildingType, maxCitizens, upkeep, incomePerCitizen, buildingCost, borderLength);
        } else if (buildingType.Equals(BuildingType.WorkSpace)) {
            return new Building(name, tileBase, sprite, buildingType, maxCitizens, upkeep, incomePerCitizen, 
                buildingCost, borderLength, beneficialTrait, detrimentalTrait, fulfilledNeed);
        }

        return null;
    }

    public void SetActive(bool active) {
        this.active = active;
    }
}
