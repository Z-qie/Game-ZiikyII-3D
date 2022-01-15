using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu()]
public class MapConfig : ScriptableObject
{
    // generation
    public int randomFillPercent;
    public int smoothStrength;
    public string seed;
    public bool useRandomSeed;

    // creation
    public int mapWidth = 100;
    public int mapHeight = 100;
    public int wallHeight = 40;
    public int tileSize = 6;
    public int pathWidth = 2;

    public System.Random pseudoRandom;

    public Vector3 CoordToWorldPos(int x, int y, float tileSize)
    {
        return new Vector3(tileSize * (-mapWidth / 2 + .5f + x), 0, tileSize * (-mapHeight / 2 + .5f + y));
    }

    public bool IsInMapRange(int x, int y)
    {
        return x >= 0 && x < mapWidth && y >= 0 && y < mapHeight;
    }

}
public struct Coord
{
    public int tileX;
    public int tileY;

    public Coord(int x, int y)
    {
        tileX = x;
        tileY = y;
    }

    public static Coord operator +(Coord a, Coord b)
    {
        return new Coord(a.tileX + b.tileX, a.tileY + b.tileY);
    }

    public static float SqrMagnitude(Coord a, Coord b)
    {
        return (a.tileX - b.tileX) * (a.tileX - b.tileX) + (a.tileY - b.tileY) * (a.tileY - b.tileY);
    }
}