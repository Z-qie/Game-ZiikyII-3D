//using UnityEngine;
//using UnityEngine.AI;
//using System.Collections;
//using System.Collections.Generic;
//using System;

//// type
//// 0 = floor
//// 1 = wall
//// 2 = edge

//public enum TileType
//{
//    FLOOR,
//    WALL,
//    EDGE,
//    PATH,
//    SPAWNING,
//    RUNE
//}

//// for each Room, we gotta set a random position for the rune
//// for each Path, we gotta set the value as Path

////[ExecuteInEditMode]
//public class MapGenerator : MonoBehaviour
//{
//    public MapConfig config;
//    public NavMeshSurface navMeshSurface;

//    //public GameObject wallPrefab;
//    public GameObject floorPrefab;

//    [Range(0, 100)]
//    public int randomFillPercent;

//    public MeshGenerator meshGenerator;
//    public GameObject wallPrefab;

//    TileType[,] map;


//    void Awake()
//    {

//    }

//    void Start()
//    {
//        GenerateMap();
//        //UpdateNavMesh();
//        //navMeshSurface.BuildNavMesh();
//    }

//    void Update()
//    {
//        if (Input.GetKeyDown("m"))
//        {
//            GenerateMap();
//            navMeshSurface.BuildNavMesh();
//        }
//    }

//    void GenerateMap()
//    {
//        //foreach (Transform child in transform)
//        //    GameObject.Destroy(child.gameObject);
//        //foreach (Transform child in transform)
//        //    GameObject.DestroyImmediate(child.gameObject);

//        map = new TileType[config.mapWidth, config.mapHeight];
//        RandomFillMap();

//        SmoothMap(config.smoothStrength);
//        ProcessMap();

//        GenerateWallEdge();
//        GenerateWall();

//    }

//    void GenerateWallEdge()
//    {
//        for (int tileX = 0; tileX < config.mapWidth; tileX++)
//        {
//            for (int tileY = 0; tileY < config.mapHeight; tileY++)
//            {
//                if (map[tileX, tileY] == TileType.WALL)
//                {
//                    AssignEdge(tileX, tileY);
//                }
//            }
//        }
//        //meshGenerator.GenerateMesh(map);
//    }

//    void AssignEdge(int tileX, int tileY)
//    {
//        for (int x = tileX - 1; x <= tileX + 1; x++)
//        {
//            for (int y = tileY - 1; y <= tileY + 1; y++)
//            {
//                if (config.IsInMapRange(x, y) && map[x, y] == TileType.FLOOR) // if is floor;
//                {
//                    map[tileX, tileY] = TileType.EDGE;
//                    return;
//                }
//            }
//        }
//    }

//    void GenerateWall()
//    {
//        for (int x = 0; x < config.mapWidth; x++)
//        {
//            for (int y = 0; y < config.mapHeight; y++)
//            {
//                if (map[x, y] == TileType.EDGE)
//                {
//                    Vector3 wallPosition = config.CoordToWorldPos(x, y, config.tileSize);
//                    wallPrefab.transform.localScale = new Vector3(1, 1, 1);
//                    Instantiate<GameObject>(wallPrefab, wallPosition, Quaternion.identity, transform);
//                }
//            }
//        }

//        //floor
//        Vector3 floorPosition = new Vector3(0, -config.tileSize / 2, 0);
//        floorPrefab.transform.localScale = new Vector3(config.mapWidth * config.tileSize, config.tileSize, config.mapHeight * config.tileSize);
//        Instantiate<GameObject>(floorPrefab, floorPosition, Quaternion.identity, transform);
//    }

//    void ProcessMap()
//    {
//        List<List<Coord>> wallRegions = GetRegions(TileType.WALL); // 1=wall

//        foreach (List<Coord> wallRegion in wallRegions)
//        {
//            if (wallRegion.Count < config.wallThresholdSize)
//            {
//                foreach (Coord tile in wallRegion)
//                {
//                    map[tile.tileX, tile.tileY] = 0;
//                }
//            }
//        }

//        List<List<Coord>> roomRegions = GetRegions(0);
//        int roomThresholdSize = 50;
//        List<Room> survivingRooms = new List<Room>();

//        foreach (List<Coord> roomRegion in roomRegions)
//        {
//            if (roomRegion.Count < roomThresholdSize)
//            {
//                foreach (Coord tile in roomRegion)
//                {
//                    map[tile.tileX, tile.tileY] = TileType.WALL;
//                }
//            }
//            else
//            {
//                survivingRooms.Add(new Room(roomRegion, map));
//            }
//        }
//        survivingRooms.Sort();
//        survivingRooms[0].isMainRoom = true;
//        survivingRooms[0].isAccessibleFromMainRoom = true;

//        ConnectClosestRooms(survivingRooms);
//    }

//    void ConnectClosestRooms(List<Room> allRooms, bool forceAccessibilityFromMainRoom = false)
//    {

//        List<Room> roomListA = new List<Room>();
//        List<Room> roomListB = new List<Room>();

//        if (forceAccessibilityFromMainRoom)
//        {
//            foreach (Room room in allRooms)
//            {
//                if (room.isAccessibleFromMainRoom)
//                {
//                    roomListB.Add(room);
//                }
//                else
//                {
//                    roomListA.Add(room);
//                }
//            }
//        }
//        else
//        {
//            roomListA = allRooms;
//            roomListB = allRooms;
//        }

//        int bestDistance = 0;
//        Coord bestTileA = new Coord();
//        Coord bestTileB = new Coord();
//        Room bestRoomA = new Room();
//        Room bestRoomB = new Room();
//        bool possibleConnectionFound = false;

//        foreach (Room roomA in roomListA)
//        {
//            if (!forceAccessibilityFromMainRoom)
//            {
//                possibleConnectionFound = false;
//                if (roomA.connectedRooms.Count > 0)
//                {
//                    continue;
//                }
//            }

//            foreach (Room roomB in roomListB)
//            {
//                if (roomA == roomB || roomA.IsConnected(roomB))
//                {
//                    continue;
//                }

//                for (int tileIndexA = 0; tileIndexA < roomA.edgeTiles.Count; tileIndexA++)
//                {
//                    for (int tileIndexB = 0; tileIndexB < roomB.edgeTiles.Count; tileIndexB++)
//                    {
//                        Coord tileA = roomA.edgeTiles[tileIndexA];
//                        Coord tileB = roomB.edgeTiles[tileIndexB];
//                        int distanceBetweenRooms = (int)(Mathf.Pow(tileA.tileX - tileB.tileX, 2) + Mathf.Pow(tileA.tileY - tileB.tileY, 2));

//                        if (distanceBetweenRooms < bestDistance || !possibleConnectionFound)
//                        {
//                            bestDistance = distanceBetweenRooms;
//                            possibleConnectionFound = true;
//                            bestTileA = tileA;
//                            bestTileB = tileB;
//                            bestRoomA = roomA;
//                            bestRoomB = roomB;
//                        }
//                    }
//                }
//            }
//            if (possibleConnectionFound && !forceAccessibilityFromMainRoom)
//            {
//                CreatePassage(bestRoomA, bestRoomB, bestTileA, bestTileB);
//            }
//        }

//        if (possibleConnectionFound && forceAccessibilityFromMainRoom)
//        {
//            CreatePassage(bestRoomA, bestRoomB, bestTileA, bestTileB);
//            ConnectClosestRooms(allRooms, true);
//        }

//        if (!forceAccessibilityFromMainRoom)
//        {
//            ConnectClosestRooms(allRooms, true);
//        }
//    }

//    void CreatePassage(Room roomA, Room roomB, Coord tileA, Coord tileB)
//    {
//        Room.ConnectRooms(roomA, roomB);
//        //Debug.DrawLine (CoordToWorldPoint (tileA), CoordToWorldPoint (tileB), Color.green, 100);

//        List<Coord> line = GetLine(tileA, tileB);
//        foreach (Coord c in line)
//        {
//            DrawCircle(c, config.pathWidth);
//        }
//    }

//    void DrawCircle(Coord c, int r)
//    {
//        for (int x = -r; x <= r; x++)
//        {
//            for (int y = -r; y <= r; y++)
//            {
//                if (x * x + y * y <= r * r)
//                {
//                    int drawX = c.tileX + x;
//                    int drawY = c.tileY + y;
//                    if (config.IsInMapRange(drawX, drawY))
//                    {
//                        map[drawX, drawY] = 0;
//                    }
//                }
//            }
//        }
//    }

//    List<Coord> GetLine(Coord from, Coord to)
//    {
//        List<Coord> line = new List<Coord>();

//        int x = from.tileX;
//        int y = from.tileY;

//        int dx = to.tileX - from.tileX;
//        int dy = to.tileY - from.tileY;

//        bool inverted = false;
//        int step = Math.Sign(dx);
//        int gradientStep = Math.Sign(dy);

//        int longest = Mathf.Abs(dx);
//        int shortest = Mathf.Abs(dy);

//        if (longest < shortest)
//        {
//            inverted = true;
//            longest = Mathf.Abs(dy);
//            shortest = Mathf.Abs(dx);

//            step = Math.Sign(dy);
//            gradientStep = Math.Sign(dx);
//        }

//        int gradientAccumulation = longest / 2;
//        for (int i = 0; i < longest; i++)
//        {
//            line.Add(new Coord(x, y));

//            if (inverted)
//            {
//                y += step;
//            }
//            else
//            {
//                x += step;
//            }

//            gradientAccumulation += shortest;
//            if (gradientAccumulation >= longest)
//            {
//                if (inverted)
//                {
//                    x += gradientStep;
//                }
//                else
//                {
//                    y += gradientStep;
//                }
//                gradientAccumulation -= longest;
//            }
//        }

//        return line;
//    }

//    List<List<Coord>> GetRegions(TileType tileType)
//    {
//        List<List<Coord>> regions = new List<List<Coord>>();
//        int[,] mapFlags = new int[config.mapWidth, config.mapHeight];

//        for (int x = 0; x < config.mapWidth; x++)
//        {
//            for (int y = 0; y < config.mapHeight; y++)
//            {
//                if (mapFlags[x, y] == 0 && map[x, y] == tileType)
//                {
//                    List<Coord> newRegion = GetRegionTiles(x, y);
//                    regions.Add(newRegion);

//                    foreach (Coord tile in newRegion)
//                    {
//                        mapFlags[tile.tileX, tile.tileY] = 1;
//                    }
//                }
//            }
//        }
//        return regions;
//    }

//    List<Coord> GetRegionTiles(int startX, int startY)
//    {
//        List<Coord> tiles = new List<Coord>();
//        int[,] mapFlags = new int[config.mapWidth, config.mapHeight];
//        TileType tileType = map[startX, startY];

//        Queue<Coord> queue = new Queue<Coord>();
//        queue.Enqueue(new Coord(startX, startY));
//        mapFlags[startX, startY] = 1;

//        while (queue.Count > 0)
//        {
//            Coord tile = queue.Dequeue();
//            tiles.Add(tile);

//            for (int x = tile.tileX - 1; x <= tile.tileX + 1; x++)
//            {
//                for (int y = tile.tileY - 1; y <= tile.tileY + 1; y++)
//                {
//                    if (config.IsInMapRange(x, y) && (y == tile.tileY || x == tile.tileX))
//                    {
//                        if (mapFlags[x, y] == 0 && map[x, y] == tileType)
//                        {
//                            mapFlags[x, y] = 1;
//                            queue.Enqueue(new Coord(x, y));
//                        }
//                    }
//                }
//            }
//        }
//        return tiles;
//    }

//    void RandomFillMap()
//    {
//        if (config.useRandomSeed)
//        {
//            config.seed = Time.time.ToString();
//        }

//        System.Random pseudoRandom = new System.Random(config.seed.GetHashCode());

//        for (int x = 0; x < config.mapWidth; x++)
//        {
//            for (int y = 0; y < config.mapHeight; y++)
//            {
//                if (x == 0 || x == config.mapWidth - 1 || y == 0 || y == config.mapHeight - 1)
//                {
//                    map[x, y] = TileType.WALL; // make sure all boundaries are wall
//                }
//                else
//                {
//                    map[x, y] = (pseudoRandom.Next(0, 100) < randomFillPercent) ? TileType.WALL : TileType.FLOOR;
//                }
//            }
//        }
//    }

//    void SmoothMap(int smoothStrength)
//    {
//        for (int i = 0; i < smoothStrength; i++)
//        {
//            for (int x = 0; x < config.mapWidth; x++)
//            {
//                for (int y = 0; y < config.mapHeight; y++)
//                {
//                    int neighbourWallTiles = GetSurroundingWallCount(x, y);

//                    if (neighbourWallTiles > 4)
//                        map[x, y] = TileType.WALL;
//                    else if (neighbourWallTiles < 4)
//                        map[x, y] = TileType.FLOOR;
//                }
//            }
//        }
//    }

//    int GetSurroundingWallCount(int gridX, int gridY)
//    {
//        int wallCount = 0;
//        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
//        {
//            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
//            {
//                if (config.IsInMapRange(neighbourX, neighbourY))
//                {
//                    if (neighbourX != gridX || neighbourY != gridY)
//                    {
//                        if (map[neighbourX, neighbourY] == TileType.WALL)
//                            wallCount++;
//                    }
//                }
//                else
//                {
//                    wallCount++;
//                }
//            }
//        }
//        return wallCount;
//    }



//    class Room : IComparable<Room>
//    {
//        public List<Coord> tiles;
//        public List<Coord> edgeTiles;
//        public List<Room> connectedRooms;
//        public int roomSize;
//        public bool isAccessibleFromMainRoom;
//        public bool isMainRoom;

//        public Room()
//        {
//        }

//        public Room(List<Coord> roomTiles, TileType[,] map)
//        {
//            tiles = roomTiles;
//            roomSize = tiles.Count;
//            connectedRooms = new List<Room>();

//            edgeTiles = new List<Coord>();
//            foreach (Coord tile in tiles)
//            {
//                for (int x = tile.tileX - 1; x <= tile.tileX + 1; x++)
//                {
//                    for (int y = tile.tileY - 1; y <= tile.tileY + 1; y++)
//                    {
//                        if (x == tile.tileX || y == tile.tileY)
//                        {
//                            if (map[x, y] == TileType.WALL)
//                            {
//                                edgeTiles.Add(tile);
//                            }
//                        }
//                    }
//                }
//            }
//        }

//        public void SetAccessibleFromMainRoom()
//        {
//            if (!isAccessibleFromMainRoom)
//            {
//                isAccessibleFromMainRoom = true;
//                foreach (Room connectedRoom in connectedRooms)
//                {
//                    connectedRoom.SetAccessibleFromMainRoom();
//                }
//            }
//        }

//        public static void ConnectRooms(Room roomA, Room roomB)
//        {
//            if (roomA.isAccessibleFromMainRoom)
//            {
//                roomB.SetAccessibleFromMainRoom();
//            }
//            else if (roomB.isAccessibleFromMainRoom)
//            {
//                roomA.SetAccessibleFromMainRoom();
//            }
//            roomA.connectedRooms.Add(roomB);
//            roomB.connectedRooms.Add(roomA);
//        }

//        public bool IsConnected(Room otherRoom)
//        {
//            return connectedRooms.Contains(otherRoom);
//        }

//        public int CompareTo(Room otherRoom)
//        {
//            return otherRoom.roomSize.CompareTo(roomSize);
//        }
//    }


//    //void OnDrawGizmos()
//    //{
//    //    Gizmos.color = Color.red;
//    //    Gizmos.DrawSphere(new Vector3(0, 20, 0), 5f);

//    //    for (int x = 0; x < mapWidth; x++)
//    //    {
//    //        for (int y = 0; y < mapHeight; y++)
//    //        {
//    //            if (map[x, y] == TileType.EDGE)
//    //            {
//    //                float randomWallHeight = wallHeight + UnityEngine.Random.Range(-10f, 10f);

//    //                for (int i = 0; i < numberOfVertexPerEdge; i++)
//    //                {

//    //                    Vector3 wallPosition = new Vector3(
//    //                        tileSize * (-mapWidth / 2 + 0.5f + x),
//    //                        randomWallHeight * i / numberOfVertexPerEdge,
//    //                        tileSize * (-mapHeight / 2 + 0.5f + y));

//    //                    Gizmos.color = Color.red;
//    //                    Gizmos.DrawSphere(wallPosition, 1f);
//    //                    //wallPrefab.transform.localScale = new Vector3(tileSize, randomWallHeight, tileSize);
//    //                    //Instantiate<GameObject>(wallPrefab, wallPosition, Quaternion.identity, transform);
//    //                }

//    //            }
//    //        }
//    //    }
//    //}
//}