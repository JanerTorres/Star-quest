using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanZoom : MonoBehaviour
{
    public Vector3 touchStart;
    public Vector3 direccion;
    public Camera camara;

    public float zoomVelocity;
    public float zoomMin;
    public float zoomMax;
    public Vector2 firstTouchPrevPos;
    public Vector2 secondTouchPrevPos;

    public float prevMagnitude;
    public float currentMagnitude;

    public float zoomModifier;



    public void Update()
    {
        MoverCamara();

        if(Input.touchCount == 2)
        {
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
            secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

            prevMagnitude = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
            currentMagnitude = (firstTouch.position - secondTouch.position).magnitude;

            zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * zoomVelocity;

            if(prevMagnitude > currentMagnitude)
            {
                // decreció, aumenta su fieldView
                camara.fieldOfView += zoomModifier;
            }
            if(prevMagnitude < currentMagnitude)
            {
                // Incrementó, disminuye su fieldOfView
                camara.fieldOfView -= zoomModifier;
            }

            camara.fieldOfView = Mathf.Clamp(camara.fieldOfView, zoomMin, zoomMax);
            


        }

    }


    public void MoverCamara()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = (Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            direccion = touchStart - (Input.mousePosition);
            Camera.main.transform.position += direccion * Time.deltaTime;
            print(Input.mousePosition);
            touchStart = (Input.mousePosition);

        }
    }

    // No se usa de momento
    public void CambiarZoom(float increment)
    {
        camara.fieldOfView = Mathf.Clamp(camara.orthographicSize - increment, zoomMin, zoomMax);
        print("increment" + increment);

    }

}
