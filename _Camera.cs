using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Camera : MonoBehaviour
{

    Vector2 directionOffset = new Vector2(0, 0); //cambio de posicion
    Vector2 currentPosition = new Vector2(0, 0);
    GameObject body;

    public float sesibility = 5.0f;
    public float smoothness = 5.0f;

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // para usar el raton
        body = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //getaxis
        //getaxisraw, para configurar y cambiar valores
        //captura el movimiento del raton
        Vector2 mouseDirection = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouseDirection = Vector2.Scale(mouseDirection, new Vector2(sesibility * smoothness, sesibility * smoothness));

        //Mathf.Lerp: intepola entre a y b, y crea los puntos intermedios
        directionOffset.x = Mathf.Lerp(directionOffset.x, mouseDirection.x, 1.0f / smoothness);
        directionOffset.y = Mathf.Lerp(directionOffset.y, mouseDirection.y, 1.0f / smoothness);

        //posicion actual
        currentPosition += directionOffset;


        //para que no de la vuelta
        if (currentPosition.y > -90 && currentPosition.y < 90)
        {
            transform.localRotation = Quaternion.AngleAxis(-1.0f * currentPosition.y, Vector3.right);
            body.transform.localRotation = Quaternion.AngleAxis(currentPosition.x, Vector3.up);
        }


        //Debug.Log(currentPosition);

    }
}


