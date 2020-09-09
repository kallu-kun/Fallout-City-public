using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {
    public Position position { get; protected set; }

    public TileType tileType { get; set; }

    public Building building { get; protected set; }

    public Tile(TileType tileType, int x, int y) {
        this.tileType = tileType;
        position = new Position(x, y);
    }
    
    public void AddBuilding(Building building) {
        this.building = building;
    }
}
