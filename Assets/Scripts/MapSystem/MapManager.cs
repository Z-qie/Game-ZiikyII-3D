using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using System;

// type
// 0 = floor
// 1 = wall
// 2 = edge

public enum TileType
{
    FLOOR,
    WALL,
    EDGE,
    PATH,
    SPAWNING,
    RUNE
}

// for each Room, we gotta set a random position for the rune
// for each Path, we gotta set the value as Path

//[ExecuteInEditMode]
public class MapManager : MonoBehaviour
{
    public MapConfig config;
    public NavMeshSurface navMeshSurface;

    //public GameObject wallPrefab;
    public GameObject floorPrefab;
    public WallGenerator wallGenerator;
    public GameObject wallPrefab;

    TileType[,] map;


    void Start()
    {
        map = new TileType[config.mapWidth, config.mapHeight];
        GenerateMap();
        //UpdateNavMesh();
        navMeshSurface.BuildNavMesh();
    }

    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            GenerateMap();
            navMeshSurface.BuildNavMesh();
        }
    }

    void GenerateMap()
    {
        if (config.useRandomSeed)
        {
            config.seed = Time.time.ToString();
        }

        config.pseudoRandom = new System.Random(config.seed.GetHashCode());

        RandomFillMap();
        SmoothMap();
        EliminateRedundent();


        //floor
        //Vector3 floorPosition = new Vector3(0, -config.tileSize / 2, 0);
        //floorPrefab.transform.localScale = new Vector3(config.mapWidth * config.tileSize, config.tileSize, config.mapHeight * config.tileSize);
        //Instantiate<GameObject>(floorPrefab, floorPosition, Quaternion.identity, transform);


        GenerateWallEdge();
        //GenerateWall();

    }

    void GenerateWallEdge()
    {
        for (int tileX = 0; tileX < config.mapWidth; tileX++)
        {
            for (int tileY = 0; tileY < config.mapHeight; tileY++)
            {
                if (map[tileX, tileY] == TileType.WALL)
                {
                    AssignEdge(tileX, tileY);
                }
            }
        }
        wallGenerator.GenerateMesh(map);
        wallGenerator.GenerateWall();
    }

    void AssignEdge(int tileX, int tileY)
    {
        for (int x = tileX - 1; x <= tileX + 1; x++)
        {
            for (int y = tileY - 1; y <= tileY + 1; y++)
            {
                if (config.IsInMapRange(x, y) && map[x, y] == TileType.FLOOR) // if is floor;
                {
                    map[tileX, tileY] = TileType.EDGE;
                    return;
                }
            }
        }
    }


    void RandomFillMap()
    {
        for (int x = 0; x < config.mapWidth; x++)
        {
            for (int y = 0; y < config.mapHeight; y++)
            {
                map[x, y] = TileType.WALL;
            }
        }

        for (int x = 0; x < config.mapWidth; x++)
        {
            for (int y = 0; y < config.mapHeight; y++)
            {
                if (config.pseudoRandom.Next(0, config.mapWidth * config.mapHeight / 6) > Coord.SqrMagnitude(new Coord(x, y), new Coord(config.mapWidth / 2, config.mapHeight / 2)))
                    map[x, y] = TileType.FLOOR;
            }
        }
    }

    void SmoothMap()
    {
        for (int i = 0; i < config.smoothStrength; i++)
        {
            for (int x = 0; x < config.mapWidth; x++)
            {
                for (int y = 0; y < config.mapHeight; y++)
                {
                    int neighbourWallTiles = GetSurroundingWallCount(x, y);

                    if (neighbourWallTiles > 4)
                        map[x, y] = TileType.WALL;
                    else if (neighbourWallTiles < 4)
                        map[x, y] = TileType.FLOOR;
                }
            }
        }
    }

    void EliminateRedundent()
    {
        List<List<Coord>> wallRegions = GetRegions(TileType.WALL); // 1=wall

        int thresholdSize = 20;
        foreach (List<Coord> wallRegion in wallRegions)
        {
            if (wallRegion.Count < thresholdSize)
            {
                foreach (Coord tile in wallRegion)
                {
                    map[tile.tileX, tile.tileY] = TileType.FLOOR;
                }
            }
        }

        List<List<Coord>> roomRegions = GetRegions(TileType.FLOOR);
        foreach (List<Coord> roomRegion in roomRegions)
        {
            if (roomRegion.Count < thresholdSize)
            {
                foreach (Coord tile in roomRegion)
                {
                    map[tile.tileX, tile.tileY] = TileType.WALL;
                }
            }
        }
    }

    List<List<Coord>> GetRegions(TileType tileType)
    {
        List<List<Coord>> regions = new List<List<Coord>>();
        int[,] mapFlags = new int[config.mapWidth, config.mapHeight];

        for (int x = 0; x < config.mapWidth; x++)
        {
            for (int y = 0; y < config.mapHeight; y++)
            {
                if (mapFlags[x, y] == 0 && map[x, y] == tileType)
                {
                    List<Coord> newRegion = GetRegionTiles(x, y);
                    regions.Add(newRegion);

                    foreach (Coord tile in newRegion)
                    {
                        mapFlags[tile.tileX, tile.tileY] = 1;
                    }
                }
            }
        }
        return regions;
    }

    List<Coord> GetRegionTiles(int startX, int startY)
    {
        List<Coord> tiles = new List<Coord>();
        int[,] mapFlags = new int[config.mapWidth, config.mapHeight];
        TileType tileType = map[startX, startY];

        Queue<Coord> queue = new Queue<Coord>();
        queue.Enqueue(new Coord(startX, startY));
        mapFlags[startX, startY] = 1;

        while (queue.Count > 0)
        {
            Coord tile = queue.Dequeue();
            tiles.Add(tile);

            for (int x = tile.tileX - 1; x <= tile.tileX + 1; x++)
            {
                for (int y = tile.tileY - 1; y <= tile.tileY + 1; y++)
                {
                    if (config.IsInMapRange(x, y) && (y == tile.tileY || x == tile.tileX))
                    {
                        if (mapFlags[x, y] == 0 && map[x, y] == tileType)
                        {
                            mapFlags[x, y] = 1;
                            queue.Enqueue(new Coord(x, y));
                        }
                    }
                }
            }
        }
        return tiles;
    }

    int GetSurroundingWallCount(int gridX, int gridY)
    {
        int wallCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                if (config.IsInMapRange(neighbourX, neighbourY))
                {
                    if (neighbourX != gridX || neighbourY != gridY)
                    {
                        if (map[neighbourX, neighbourY] == TileType.WALL)
                            wallCount++;
                    }
                }
                else
                {
                    wallCount++;
                }
            }
        }
        return wallCount;
    }

    void DrawCircle(Coord c, int r)
    {
        for (int x = -r; x <= r; x++)
        {
            for (int y = -r; y <= r; y++)
            {
                if (x * x + y * y <= r * r)
                {
                    int drawX = c.tileX + x;
                    int drawY = c.tileY + y;
                    if (config.IsInMapRange(drawX, drawY))
                    {
                        map[drawX, drawY] = 0;
                    }
                }
            }
        }
    }

    List<Coord> GetLine(Coord from, Coord to)
    {
        List<Coord> line = new List<Coord>();

        int x = from.tileX;
        int y = from.tileY;

        int dx = to.tileX - from.tileX;
        int dy = to.tileY - from.tileY;

        bool inverted = false;
        int step = Math.Sign(dx);
        int gradientStep = Math.Sign(dy);

        int longest = Mathf.Abs(dx);
        int shortest = Mathf.Abs(dy);

        if (longest < shortest)
        {
            inverted = true;
            longest = Mathf.Abs(dy);
            shortest = Mathf.Abs(dx);

            step = Math.Sign(dy);
            gradientStep = Math.Sign(dx);
        }

        int gradientAccumulation = longest / 2;
        for (int i = 0; i < longest; i++)
        {
            line.Add(new Coord(x, y));

            if (inverted)
            {
                y += step;
            }
            else
            {
                x += step;
            }

            gradientAccumulation += shortest;
            if (gradientAccumulation >= longest)
            {
                if (inverted)
                {
                    x += gradientStep;
                }
                else
                {
                    y += gradientStep;
                }
                gradientAccumulation -= longest;
            }
        }

        return line;
    }


    void OnDrawGizmos()
    {

        if (map == null)
            return;


        for (int x = 0; x < config.mapWidth; x++)
        {
            for (int y = 0; y < config.mapHeight; y++)
            {
                if (map[x, y] == TileType.FLOOR)
                {
                    Vector3 wallPosition = config.CoordToWorldPos(x, y, config.tileSize);
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawCube(wallPosition, new Vector3(config.tileSize, 1, config.tileSize));
                }
            }
        }
    }
}