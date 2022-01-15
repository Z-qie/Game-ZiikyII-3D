using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Torec;

public class Ripple : MonoBehaviour
{
    //public Transform player;
    //public Camera rippleCamera;

    //public float cameraSize;
    //public float waterUnitSize;
    public Camera interactiveRippleCamera;
    public Camera reflectionCamera;


    private RenderTexture prevRT;
    private RenderTexture currentRT;
    private RenderTexture tempRT;
    public RenderTexture interactiveRT;
    public RenderTexture reflectiveRT;

    public Shader rippleShader;
    //public Shader drawShader;
    public Shader interactiveShader;

    //private float rippleIntervalDistance;
    //public float rippleInterval = 0.5f;
    private Material rippleMat;
    //private Material drawMat;
    private Material interactiveMat;


    public int textureSize;

    private Mesh subdiviedMesh;
    private Mesh originalMesh;

    private int iter = 0;

    private void Awake()
    {
        subdiviedMesh = new Mesh();
        originalMesh = GetComponent<MeshFilter>().mesh;
        //Subdivision(iter);

    }
    void Start()
    {
        prevRT = CreateRTR();
        currentRT = CreateRTR();
        tempRT = CreateRTR();
        //interactiveRT = CreateRTR();
        //reflectiveRT = CreateRTRGBA();


        rippleMat = new Material(rippleShader);
        //drawMat = new Material(drawShader);
        interactiveMat = new Material(interactiveShader);

        //reflectionCamera.targetTexture = reflectiveRT;
        //interactiveRippleCamera.targetTexture = interactiveRT;
        GetComponent<Renderer>().material.SetTexture("_Reflection", reflectiveRT);
        GetComponent<Renderer>().material.mainTexture = currentRT;
    }

    private void Subdivision(int iter)
    {

        subdiviedMesh = CatmullClark.Subdivide(originalMesh, iter, new CatmullClark.Options
        {
            //boundaryInterpolation = CatmullClark.Options.BoundaryInterpolation.normal,
            boundaryInterpolation = CatmullClark.Options.BoundaryInterpolation.fixBoundaries,
            //boundaryInterpolation = CatmullClark.Options.BoundaryInterpolation.fixCorners,
        });
        GetComponent<MeshFilter>().mesh = subdiviedMesh;
        //var obj = new GameObject("newMesh");
        //obj.transform.SetParent(this.transform);
        //obj.transform.position = this.transform.position;
        //obj.AddComponent<MeshFilter>().sharedMesh = newMesh;
        //obj.AddComponent<MeshRenderer>().material = this.GetComponent<MeshRenderer>().material;
    }



    //private void DrawAt(float x, float y, float r)
    //{

    //    drawMat.SetTexture("_SourceTex", currentRT);
    //    drawMat.SetVector("_Pos", new Vector4(x, y, r));
    //    Graphics.Blit(null, tempRT, drawMat);
    //    //swap
    //    RenderTexture rt = tempRT;
    //    tempRT = currentRT;
    //    currentRT = rt;
    //}


    // Update is called once per frame
    void Update()
    {

        // add masked/interactive texture into currentRT
        interactiveMat.SetTexture("_Tex1", interactiveRT);
        interactiveMat.SetTexture("_Tex2", currentRT);
        Graphics.Blit(null, tempRT, interactiveMat);
        RenderTexture rt0 = tempRT;
        tempRT = currentRT;
        currentRT = rt0;

        // calculate ripple based on currentRT
        rippleMat.SetTexture("_PrevRT", prevRT);
        rippleMat.SetTexture("_CurrentRT", currentRT);
        // put current rt into tempRT
        Graphics.Blit(null, tempRT, rippleMat);
        // put tempRT into prevRT
        Graphics.Blit(tempRT, prevRT);
        RenderTexture rt1 = prevRT;
        prevRT = currentRT;
        currentRT = rt1;
    }

    public RenderTexture CreateRTR()
    {
        RenderTexture rt = new RenderTexture(textureSize, textureSize, 0, RenderTextureFormat.RFloat);
        rt.Create();
        return rt;
    }

    public RenderTexture CreateRTRGBA()
    {
        RenderTexture rt = new RenderTexture(textureSize, textureSize, 0, RenderTextureFormat.ARGBFloat);
        rt.Create();
        return rt;
    }

}
