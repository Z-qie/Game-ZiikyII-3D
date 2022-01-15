using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlendShape : MonoBehaviour
{
    public SkinnedMeshRenderer cloakRenderer;
    public SkinnedMeshRenderer headRenderer;
    //public SkinnedMeshRenderer bodyRenderer;
    public float blendSpeed = 1f;

    Mesh skinnedMesh;
    float blendValue = 0f;
    bool isDecreasing = false;

    void Start()
    {

    }

    void Update()
    {
        if (blendValue >= 100f)
            isDecreasing = true;
        else if (blendValue <= 0f)
            isDecreasing = false;

        if (!isDecreasing)
            blendValue += blendSpeed * Time.deltaTime;
        else
            blendValue -= blendSpeed * Time.deltaTime;

        headRenderer.SetBlendShapeWeight(0, blendValue);
        cloakRenderer.SetBlendShapeWeight(0, blendValue);

    }
}
