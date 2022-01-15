//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class DashThunderRenderer : MonoBehaviour
//{
//    public float intervalSize;
//    public float thinWidth;
//    public float thickWidth;
//    public float arcHeight;
//    public float sinHeight;
//    public float sinHeightTime;
//    public float noiseHeight;

//    public Transform playerTransform;

//    private Vector3 end;
//    private Vector3 start;
//    private int numVertex; // tbc delete
//    private float currentSinHeightTime;
//    private float[] sinHeights;
//    private LineRenderer[] lineRenderers;
//    private System.Random random;
//    // Start is called before the first frame update
//    void Start()
//    {
//        random = new System.Random(Time.time.GetHashCode());
//        lineRenderers = GetComponentsInChildren<LineRenderer>();

      
//        sinHeights = new float[lineRenderers.Length];
//    }

//    private void OnEnable()
//    {
//        start = playerTransform.position;

//        foreach (var lineRenderer in lineRenderers)
//        {
//            lineRenderer.enabled = true;
//        }
//    }

//    private void OnDisable()
//    {
//        for (int i = 0; i < lineRenderers.Length; i++)
//        {
//            lineRenderers[i].positionCount = 0;
//        }
//        foreach (var lineRenderer in lineRenderers)
//        {
//            lineRenderer.enabled = false;
//        }
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        end = playerTransform.position;

//        numVertex = (int)((end - start).magnitude / intervalSize);

//        for (int i = 0; i < lineRenderers.Length; i++)
//        {
//            lineRenderers[i].widthMultiplier = thickWidth;
//            lineRenderers[i].positionCount = numVertex;
//        }
//        lineRenderers[0].widthMultiplier = thinWidth;
//        lineRenderers[1].widthMultiplier = thinWidth;

//        Vector3 interval = (end - start) / numVertex;



//        calculateSinHeight();

//        for (int i = 0; i < lineRenderers.Length; i++)
//        {
//            float arcHeight = calculateArcHeight();

//            for (int j = 0; j < numVertex; j++)
//            {
//                Vector3 position = interval * j + start;

//                float arc = Mathf.Sin(((float)j / (numVertex - 1)) * Mathf.PI) * arcHeight;

//                float sin;

//                if (i < 2)
//                    sin = Mathf.Sin(2 * Mathf.PI * (sinHeights[i] + ((float)j / (numVertex - 1)) * 1.4f)) * sinHeight * 2.4f;
//                else if (i < 4)
//                    sin = Mathf.Sin(2 * Mathf.PI * (sinHeights[i] + ((float)j / (numVertex - 1)))) * sinHeight;
//                else
//                    sin = Mathf.Sin(2 * Mathf.PI * (sinHeights[i] + ((float)j / (numVertex - 1)) * 1.4f)) * sinHeight * 1.2f;

//                Vector3 noise = new Vector3(((float)random.NextDouble() - 0.5f) * noiseHeight * 2, ((float)random.NextDouble() - 0.5f) * noiseHeight * 2, ((float)random.NextDouble() - 0.5f) * noiseHeight * 2);

//                Vector3 forward = interval.normalized;
//                Vector3 right =  Quaternion.Euler(0f, -90f, 0f) * forward;
//                //Vector3 right = new Vector3(forward.z, 0, -forward.x).normalized;
//                position = position + (arc + sin) * (forward + Vector3.up);
//                //position.x += arc + sin;
//                //position.y += arc + sin;
//                //position.z += arc + sin;

//                lineRenderers[i].SetPosition(j, position + noise);
//            }
//        }
//    }

//    private float calculateArcHeight()
//    {
//        return Mathf.PingPong(Time.time * 80, arcHeight);
//    }

//    private void calculateSinHeight()
//    {
//        if (currentSinHeightTime > 0)
//        {
//            currentSinHeightTime -= Time.deltaTime;
//        }
//        else
//        {
//            currentSinHeightTime = sinHeightTime;
//            for (int i = 0; i < lineRenderers.Length; i++)
//            {
//                sinHeights[i] = (float)random.NextDouble();
//            }
//        }
//    }
//}
