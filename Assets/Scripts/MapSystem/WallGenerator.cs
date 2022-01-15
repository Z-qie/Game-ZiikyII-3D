using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenerator : MonoBehaviour
{
    public MapConfig config;
    public GameObject wallPrefab;
    public GameObject wallParticlePrefab;

    public int numWallPerEdge;
    public int numVertexPerEdge;
    public int wallSizeMin;
    public int wallSizeMax;

    // mesh
    private Mesh mesh;
    private List<Vector3> vertices;
    private List<int> triangles;
    private List<Coord> orderedEdges;

    private int numEdges = 0;
    private bool[,] connectedEdgeMap;


    private void Start()
    {
        mesh = new Mesh();
        vertices = new List<Vector3>();
        triangles = new List<int>();
        orderedEdges = new List<Coord>();
        connectedEdgeMap = new bool[config.mapWidth, config.mapHeight];

        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    public void GenerateMesh(TileType[,] map)
    {
        foreach (Transform child in transform)
            GameObject.Destroy(child.gameObject);

        // reset
        mesh = new Mesh();
        vertices = new List<Vector3>();
        triangles = new List<int>();
        orderedEdges = new List<Coord>();
        connectedEdgeMap = new bool[config.mapWidth, config.mapHeight];
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;

        ReorderEdges(map);
        GenerateVertices();
        GenerateTriangles();

        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    public void GenerateWall()
    {
        //Vector3 mapCenter = Vector3.one;
        //float colliderShrinkOffset = 6f;
        for (int i = 0; i < vertices.Count; i++)
        {
            //Vector3 direction = (mapCenter - vertices[i]).normalized;
            //Vector3 roomedPosition = vertices[i] - direction * colliderShrinkOffset;
            //wallPrefab.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
            wallPrefab.transform.localScale = new Vector3(
                config.pseudoRandom.Next(wallSizeMin, wallSizeMax),
                config.pseudoRandom.Next(wallSizeMin, wallSizeMax),
                config.pseudoRandom.Next(wallSizeMin, wallSizeMax));

            Instantiate<GameObject>(wallPrefab, vertices[i], Quaternion.identity, transform);
        }

        for (int i = 0; i < vertices.Count / (numVertexPerEdge) / 2; i++)
        {
            wallParticlePrefab.transform.localScale = Vector3.one;
            Instantiate<GameObject>(wallParticlePrefab, vertices[i * numVertexPerEdge * 2], Quaternion.identity, transform);
        }
    }

    private void GenerateVertices()
    {
        float heightDiff = (float)config.wallHeight / numVertexPerEdge;
        for (int i = 0; i < orderedEdges.Count; i++)
        {
            for (int j = 0; j < numVertexPerEdge; j++)
            {
                Vector3 vertex = config.CoordToWorldPos(orderedEdges[i].tileX, orderedEdges[i].tileY, config.tileSize);

                //float noise = Mathf.PerlinNoise(vertex.x * .3f, vertex.z * .3f) * 2f;
                //float noise = config.tileSize / 3;
                //vertex.x += config.pseudoRandom.Next(-1, 1) * noise;
                //vertex.z += config.pseudoRandom.Next(-1, 1) * noise;
                //vertex.y = heightDiff * j + config.pseudoRandom.Next(-1, 1) * noise;

                vertex.y = heightDiff * j;
                vertices.Add(vertex);
            }
        }
    }

    private void GenerateTriangles()
    {
        int vertexIndex = 0;

        for (int i = 0; i < orderedEdges.Count - 2; i++)
        {
            for (int j = 0; j < numVertexPerEdge - 1; j++)
            {
                triangles.Add(vertexIndex + 1);
                triangles.Add(vertexIndex + numVertexPerEdge);
                triangles.Add(vertexIndex);
                triangles.Add(vertexIndex + numVertexPerEdge + 1);
                triangles.Add(vertexIndex + numVertexPerEdge);
                triangles.Add(vertexIndex + 1);
                vertexIndex++;
            }
            vertexIndex++;
        }

        for (int j = 0; j < numVertexPerEdge; j++)
        {
            triangles.Add(vertexIndex + 1);
            triangles.Add(j);
            triangles.Add(vertexIndex);
            triangles.Add(j + 1);
            triangles.Add(j);
            triangles.Add(vertexIndex + 1);
            vertexIndex++;
        }
    }

    private void ReorderEdges(TileType[,] map)
    {
        FindFirstEdge(map);
        while (connectionNextEdge(map, orderedEdges[orderedEdges.Count - 1]) != -1) ;
    }

    private int connectionNextEdge(TileType[,] map, Coord currentEdge)
    {
        //set flag
        connectedEdgeMap[currentEdge.tileX, currentEdge.tileY] = true;

        for (int x = currentEdge.tileX - 1; x <= currentEdge.tileX + 1; x++)
        {
            for (int y = currentEdge.tileY - 1; y <= currentEdge.tileY + 1; y++)
            {
                // only check perpendicular surrounding
                if ((x == currentEdge.tileX && y != currentEdge.tileY) || (x != currentEdge.tileX && y == currentEdge.tileY))
                {
                    if (config.IsInMapRange(x, y) && map[x, y] == TileType.EDGE)
                    {
                        // find the next unconnected edge
                        if (connectedEdgeMap[x, y] == false)
                        {
                            connectedEdgeMap[x, y] = true;
                            Coord newEdge = new Coord(x, y);
                            orderedEdges.Add(newEdge);
                            return orderedEdges.Count;
                        }
                    }
                }
            }
        }

        // if outside the loop, meaning all edges are connected
        return -1;
    }

    private void FindFirstEdge(TileType[,] map)
    {
        for (int i = 0; i < config.mapWidth; i++)
        {
            for (int j = 0; j < config.mapHeight; j++)
            {
                if (map[i, j] == TileType.EDGE)
                {
                    Coord edge = new Coord(i, j);
                    orderedEdges.Add(edge);
                    return;
                }
            }
        }
    }


    //IEnumerator test()
    //{

    //    //test
    //    for (int i = 0; i < orderedEdges.Count; i++)
    //    {
    //        Vector3 wallPosition = config.CoordToWorldPos(orderedEdges[i].tileX, orderedEdges[i].tileY, config.tileSize);
    //        wallPrefab.transform.localScale = new Vector3(1, 1, 1);
    //        Instantiate<GameObject>(wallPrefab, wallPosition, Quaternion.identity, transform);
    //        yield return new WaitForSeconds(0.01f);
    //    }

    //    //for (int i = 0; i < vertices.Count; i++)
    //    //{
    //    //    wallPrefab.transform.localScale = new Vector3(1, 1, 1);
    //    //    Instantiate<GameObject>(wallPrefab, vertices[i], Quaternion.identity, transform);
    //    //    yield return new WaitForSeconds(0.01f);
    //    //}
    //}

}
