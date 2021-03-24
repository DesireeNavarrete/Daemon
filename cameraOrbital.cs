using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraOrbital : MonoBehaviour
{
    public GameObject target;
    //[HideInInspector]
    public float hor;
    //[HideInInspector]
    public float ver;
    public bool mov;
    public float x;
    public float y;
    public float suavidad = 2;
    //private float speedCamRot = 0.05f;

    //El desplazamiento inicial de la camara
    Vector3 desplazamiento;

    void Start()
    {
        desplazamiento = transform.position - target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(CameraOrb());

        
    }

    IEnumerator CameraOrb()
    {


        Vector3 targetCamPos1 = target.transform.position + desplazamiento;
        //interpola la posicion con el pj
        transform.position = Vector3.Lerp(transform.position, target.transform.position + desplazamiento, suavidad * Time.deltaTime);
        //transform.RotateAround(target.transform.position, Vector3.up, 30 * Time.deltaTime);

        hor = Input.GetAxis("Horizontal2");
        ver = Input.GetAxis("Vertical2");

        x += Input.GetAxis("Vertical2") * suavidad;
        y += Input.GetAxis("Horizontal2") * suavidad;

        //x = Mathf.Clamp(x, -10, 10);
        // y = Mathf.Clamp(y, -15, 15);



        /*if (hor >= 0.1f && ver >= 0.1f)
        {
            transform.RotateAround(target.transform.position, new Vector3(0, y, 0), 30 * Time.deltaTime);

        }*/
        //angulo muerto
        if (hor >= -0.1f && hor <= 0.1f)
        {
            y = 0;
        }

        if (hor<=1f && hor > 0.1f)
        {
            transform.RotateAround(target.transform.position, new Vector3(0, y, 0), 30 * Time.deltaTime);

        }

        if (hor >= -1f && hor < -0.1f)
        {
            transform.RotateAround(target.transform.position, new Vector3(0, y, 0), 30 * Time.deltaTime);

        }

        

        //transform.rotation = Quaternion.Slerp(target.transform.rotation, transform.rotation, Time.deltaTime * suavidad);
        //transform.rotation = Quaternion.Euler(x + 26.49f, 0f, 0f);


        yield return null;

    }
}
