using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { };

public class MouseManager : MonoBehaviour
{
    public Texture2D cursor;
    public EventVector3 onMouseClicked;

    RaycastHit hitInfo;

    void Start()
    {
        //Cursor.SetCursor(null, Vector3.zero, CursorMode.Auto);
        Cursor.visible = false;
    }

    void Update()
    {
        
    }

    void SetCursorTexture()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hitInfo))
        {
            // change cursor texture
        }
    }

    void MouseControl()
    {
        if (hitInfo.collider.gameObject.CompareTag("Floor"))
        {
            onMouseClicked?.Invoke(hitInfo.point);
        }
    }


}
