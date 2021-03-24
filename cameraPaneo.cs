using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraPaneo : MonoBehaviour
{
    //La posicion de la camara a lo que va a seguir y la velocidad de la misma
    public GameObject targetPj;
    public float smoothing = 15f;

    //El desplazamiento inicial de la camara
    Vector3 offset1;
    Vector3 offset2;
    
    public float xRot;
    public float yRot;
    [HideInInspector]
    public float hor;
    [HideInInspector]
    public float ver;
    public float time = 0.1f;
    float sacudida;

    void Start()
    {
        //Calcula el desplazamiento inicial
        offset1 = transform.position - targetPj.transform.position;
    }

    private void FixedUpdate()
    {
        
            //shake it shake it
            if (sacudida > .01f)
            {
            //StopCoroutine(cam());
            transform.rotation = Quaternion.Euler(xRot + 26.49f, yRot + Random.Range(-sacudida, sacudida), 0f * Time.deltaTime * 5f);
            sacudida *= 0.9f;//va bajando la rotacion

           /* if (transform.rotation.y == 26.49f)
            {
                sacudida = sacudida * 1;
            }*/
                //print(sacudida);
                print("shake");
            }
       // print(sacudida);

        Vector3 targetCamPos1 = targetPj.transform.position + offset1;
        //interpola la posicion con el pj
        transform.position = Vector3.Lerp(transform.position, targetPj.transform.position + offset1, smoothing);

    }

    private void Update()
    {
        if (Time.timeScale == 1)
        {
            StartCoroutine(cam());
        }
        if (Time.timeScale == 0)
        {
            StopCoroutine(cam());
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(sacudirCam(40f));
            //StopCoroutine(cam());
        }
    }

    IEnumerator cam()
    {
        //cogemos los ejes de joystik izquierdo
        hor = Input.GetAxis("Horizontal2");
        ver = Input.GetAxis("Vertical2");

        //suma la rotacion
        yRot += Input.GetAxis("Horizontal2");
        xRot += Input.GetAxis("Vertical2");

        //delimitamos la rotacion de la camara
        xRot = Mathf.Clamp(xRot, -20, 20);
        yRot = Mathf.Clamp(yRot, -35, 35);


        transform.rotation = Quaternion.Euler(xRot + 26.49f, yRot, 0f);
        
       

      
        yield return null;

    }

    IEnumerator sacudirCam(float max)
    {
        yield return sacudida = max;
    }
}
