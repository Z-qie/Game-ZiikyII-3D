//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using Torec;

//public class MeshGenerator : MonoBehaviour
//{
//    public MapConfig config;
//    //public MapGenerator mapGenerator;
//    public GameObject wallPrefab;
//    public Material material;
//    public int iter;
//    public int numVertexPerEdge;

//    private Mesh mesh;
//    private List<Vector3> vertices;
//    private List<int> triangles;
//    private List<Coord> orderedEdges;

//    private int numEdges = 0;
//    private bool[,] connectedEdgeMap;
//    //List<Vector3> vertices;
//    //List<int> triangles;

//    private void Start()
//    {
//        mesh = new Mesh();
//        vertices = new List<Vector3>();
//        triangles = new List<int>();
//        orderedEdges = new List<Coord>();
//        connectedEdgeMap = new bool[config.mapWidth, config.mapHeight];

//        GetComponent<MeshFilter>().mesh = mesh;
//    }

//    private void ReorderEdges(TileType[,] map)
//    {
//        FindFirstEdge(map);
//        while (connectionNextEdge(map, orderedEdges[orderedEdges.Count - 1]) != -1) ;
//    }

//    private int connectionNextEdge(TileType[,] map, Coord currentEdge)
//    {
//        //set flag
//        connectedEdgeMap[currentEdge.tileX, currentEdge.tileY] = true;

//        for (int x = currentEdge.tileX - 1; x <= currentEdge.tileX + 1; x++)
//        {
//            for (int y = currentEdge.tileY - 1; y <= currentEdge.tileY + 1; y++)
//            {
//                // only check perpendicular surrounding
//                if ((x == currentEdge.tileX && y != currentEdge.tileY) || (x != currentEdge.tileX && y == currentEdge.tileY))
//                {
//                    if (config.IsInMapRange(x, y) && map[x, y] == TileType.EDGE)
//                    {
//                        // find the next unconnected edge
//                        if (connectedEdgeMap[x, y] == false)
//                        {
//                            connectedEdgeMap[x, y] = true;
//                            Coord newEdge = new Coord(x, y);
//                            orderedEdges.Add(newEdge);
//                            return orderedEdges.Count;
//                        }
//                    }
//                }
//            }
//        }

//        // if outside the loop, meaning all edges are connected
//        return -1;
//    }

//    private void FindFirstEdge(TileType[,] map)
//    {
//        for (int i = 0; i < config.mapWidth; i++)
//        {
//            for (int j = 0; j < config.mapHeight; j++)
//            {
//                if (map[i, j] == TileType.EDGE)
//                {
//                    Coord edge = new Coord(i, j);
//                    orderedEdges.Add(edge);
//                    return;
//                }
//            }
//        }
//    }

//    //IEnumerator test()
//    //{

//    //    //test
//    //    for (int i = 0; i < orderedEdges.Count; i++)
//    //    {
//    //        Vector3 wallPosition = config.CoordToWorldPos(orderedEdges[i].tileX, orderedEdges[i].tileY, config.tileSize);
//    //        wallPrefab.transform.localScale = new Vector3(1, 1, 1);
//    //        Instantiate<GameObject>(wallPrefab, wallPosition, Quaternion.identity, transform);
//    //        yield return new WaitForSeconds(0.01f);
//    //    }

//    //    //for (int i = 0; i < vertices.Count; i++)
//    //    //{
//    //    //    wallPrefab.transform.localScale = new Vector3(1, 1, 1);
//    //    //    Instantiate<GameObject>(wallPrefab, vertices[i], Quaternion.identity, transform);
//    //    //    yield return new WaitForSeconds(0.01f);
//    //    //}
//    //}

//    public void GenerateMesh(TileType[,] map)
//    {

//        foreach (Transform child in transform)
//            GameObject.Destroy(child.gameObject);

//        mesh = new Mesh();
//        vertices = new List<Vector3>();
//        triangles = new List<int>();
//        orderedEdges = new List<Coord>();
//        connectedEdgeMap = new bool[config.mapWidth, config.mapHeight];
//        GetComponent<MeshFilter>().mesh = mesh;

//        ReorderEdges(map);

//        // tb deleted
//        //StartCoroutine(test());

//        // Generate vertices
//        GenerateVertices();
//        GenerateTriangles();


//        //// update mesh
//        //
//        //mesh.Clear();
//        //mesh.vertices = vertices.ToArray();
//        //mesh.triangles = triangles.ToArray();
//        //mesh.RecalculateNormals();
//        //mesh.RecalculateTangents();
//        //MatrixWallGeneration();
//        //Subdivision(iter);
//    }

//    private void Subdivision(int iter)
//    {

//        mesh = CatmullClark.Subdivide(mesh, iter, new CatmullClark.Options
//        {
//            //boundaryInterpolation = CatmullClark.Options.BoundaryInterpolation.normal,
//            //boundaryInterpolation = CatmullClark.Options.BoundaryInterpolation.fixBoundaries,
//            boundaryInterpolation = CatmullClark.Options.BoundaryInterpolation.fixCorners,
//        });
//        GetComponent<MeshFilter>().mesh = mesh;
//        //var obj = new GameObject("newMesh");
//        //obj.transform.SetParent(this.transform);
//        //obj.transform.position = this.transform.position;
//        //obj.AddComponent<MeshFilter>().sharedMesh = newMesh;
//        //obj.AddComponent<MeshRenderer>().material = this.GetComponent<MeshRenderer>().material;
//    }
//    private void GenerateVertices()
//    {
//        float heightDiff = (float)config.wallHeight / numVertexPerEdge;
//        for (int i = 0; i < orderedEdges.Count; i++)
//        {
//            for (int j = 0; j < numVertexPerEdge; j++)
//            {
//                Vector3 vertex = config.CoordToWorldPos(orderedEdges[i].tileX, orderedEdges[i].tileY, config.tileSize);

//                //float noise = Mathf.PerlinNoise(vertex.x * .3f, vertex.z * .3f) * 2f;
//                //float noise = config.tileSize / 3;
//                //vertex.x += config.pseudoRandom.Next(-1, 1) * noise;
//                //vertex.z += config.pseudoRandom.Next(-1, 1) * noise;
//                //vertex.y = heightDiff * j + config.pseudoRandom.Next(-1, 1) * noise;


//                vertex.y = heightDiff * j;
//                vertices.Add(vertex);
//            }
//        }
//    }

//    private void GenerateTriangles()
//    {
//        int vertexIndex = 0;

//        for (int i = 0; i < orderedEdges.Count - 2; i++)
//        {
//            for (int j = 0; j < numVertexPerEdge - 1; j++)
//            {
//                //triangles.Add(vertexIndex);
//                //triangles.Add(vertexIndex + numVertexPerEdge);
//                //triangles.Add(vertexIndex + 1);
//                //triangles.Add(vertexIndex + 1);
//                //triangles.Add(vertexIndex + numVertexPerEdge);
//                //triangles.Add(vertexIndex + numVertexPerEdge + 1);
//                triangles.Add(vertexIndex + 1);
//                triangles.Add(vertexIndex + numVertexPerEdge);
//                triangles.Add(vertexIndex);
//                triangles.Add(vertexIndex + numVertexPerEdge + 1);
//                triangles.Add(vertexIndex + numVertexPerEdge);
//                triangles.Add(vertexIndex + 1);
//                vertexIndex++;
//            }
//            vertexIndex++;
//        }

//        for (int j = 0; j < numVertexPerEdge; j++)
//        {
//            triangles.Add(vertexIndex + 1);
//            triangles.Add(j);
//            triangles.Add(vertexIndex);
//            triangles.Add(j + 1);
//            triangles.Add(j);
//            triangles.Add(vertexIndex + 1);
//            vertexIndex++;
//        }
//    }

//    void OnDrawGizmo()
//    {

//        for (int i = 0; i < vertices.Count; i++)
//        {
//            Vector3 position = vertices[i];
//            Gizmos.DrawSphere(position, 1f);
//        }
//    }

//    void MatrixWallGeneration()
//    {

//        for (int i = 0; i < vertices.Count; i++)
//        {
//            int randomSize = config.pseudoRandom.Next(1, 10);
//            //wallPrefab.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
//            wallPrefab.transform.localScale = new Vector3(config.pseudoRandom.Next(5, 15), config.pseudoRandom.Next(5, 15), config.pseudoRandom.Next(5, 15));
//            Instantiate<GameObject>(wallPrefab, vertices[i], Quaternion.identity, transform);
//        }
//    }
//}