using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    public GameObject[] checks;
    float posX, posY, posZ, posCamX, posCamY, posCamZ;
    public Canvas canvasTransicion;
    public Camera cam;
    public static bool caidita;
    //particulas de checks
    public GameObject particulita;
    GameObject clone,clone1,clone2,clone3;

    private void Awake()
    {

        Score_Manager.score = PlayerPrefs.GetInt("score");

        //posicion de la camara
        PlayerPrefs.SetFloat("PosCamX", cam.transform.position.x);
        PlayerPrefs.SetFloat("PosCamY", cam.transform.position.y);
        PlayerPrefs.SetFloat("PosCamZ", cam.transform.position.z);



        //recoge la pos del pj
        posX = PlayerPrefs.GetFloat("PosPjX");
        posY = PlayerPrefs.GetFloat("PosPjY");
        posZ = PlayerPrefs.GetFloat("PosPjZ");

        //recoge la pos de la camara
        posCamX = PlayerPrefs.GetFloat("PosCamX");
        posCamY = PlayerPrefs.GetFloat("PosCamY");
        posCamZ = PlayerPrefs.GetFloat("PosCamZ");

        //posicion del personaje por playerpref, para poder continuar desde el menu

        transform.position = new Vector3(posX, posY, posZ);
        cam.transform.position = new Vector3(posCamX, posCamY, posCamZ);

    }
    void Start()
    {

        //canvasTransicion.GetComponent<Animator>().SetBool("desaparece", true);

        for (int i = 0; i < checks.Length; i++)
        {
            checks[i].GetComponent<Collider>().enabled = true;


        }
        caidita = false;
    }


    void Update()
    {

        if (Input.GetKey(KeyCode.T))
        {
           StartCoroutine(fade());
        }


        if (PlayerHealthNode.vida<=0)
        {
            
            
            StartCoroutine(fade());
            print("fade");
        }

        if (Input.GetKey(KeyCode.Z))
        {
            PlayerPrefs.DeleteAll();
        }

        print(PlayerHealthNode.fade);
        CameraFollowFinal.smoothingMov = 5;

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "checkpoint")
        {
            posX = PlayerPrefs.GetFloat("PosPjX");
            posY = PlayerPrefs.GetFloat("PosPjY");
            posZ = PlayerPrefs.GetFloat("PosPjZ");

            posCamX = PlayerPrefs.GetFloat("PosCamX");
            posCamY = PlayerPrefs.GetFloat("PosCamY");
            posCamZ = PlayerPrefs.GetFloat("PosCamZ");


        }

        //caida al vacio

        if (other.transform.tag=="caida")
        {
            StartCoroutine(fade());
            caidita = true;

        }
        if (other.transform.name=="0")
        {
            checks[0].GetComponent<Animator>().SetBool("aparece", true);
            saveData();

            //desactiva el shader de los otros y el trigger
            for (int z = 0; z < checks.Length; z++)
            {
                checks[z].GetComponent<Animator>().SetBool("aparece", false);
                checks[z].GetComponent<Animator>().SetBool("desaparece", true);

                if (z == 0)
                {
                    checks[z].GetComponent<Animator>().SetBool("aparece", true);
                    checks[z].GetComponent<Collider>().enabled = false;

                    clone = Instantiate(particulita, checks[0].transform.position, Quaternion.identity) as GameObject;
                    clone.SetActive(true);
                    //particulita.transform.parent = checks[0].transform;
                }

            }

        }

        if (other.transform.name == "1")
        {
            checks[1].GetComponent<Animator>().SetBool("aparece", true);
            saveData();


            //desactiva el shader de los otros y el trigger
            for (int i = 0; i < checks.Length; i++)
            {
                checks[i].GetComponent<Animator>().SetBool("aparece", false);
                checks[i].GetComponent<Animator>().SetBool("desaparece", true);
                Destroy(clone);

                if (i == 1)
                {
                    checks[i].GetComponent<Animator>().SetBool("aparece", true);
                    checks[i].GetComponent<Collider>().enabled = false;

                    clone1 = Instantiate(particulita, checks[1].transform.position, Quaternion.identity) as GameObject;
                    clone1.SetActive(true);
                    //particulita.transform.parent = checks[1].transform;
                }

            }


        }

        if (other.transform.name == "2")
        {
            checks[2].GetComponent<Animator>().SetBool("aparece", true);
            saveData();

            //desactiva el shader de los otros y el trigger
            for (int y = 0; y < checks.Length; y++)
            {
                checks[y].GetComponent<Animator>().SetBool("aparece", false);
                checks[y].GetComponent<Animator>().SetBool("desaparece", true);
                Destroy(clone1);


                if (y == 2)
                {
                    checks[y].GetComponent<Animator>().SetBool("aparece", true);
                    checks[y].GetComponent<Collider>().enabled = false;
                    checks[1].GetComponent<Collider>().enabled = true;

                    clone2 = Instantiate(particulita, checks[2].transform.position, Quaternion.identity) as GameObject;
                    clone2.SetActive(true);
                    //particulita.transform.parent = checks[2].transform;
                }

            }


        }

        if (other.transform.name == "3")
        {
            checks[3].GetComponent<Animator>().SetBool("aparece", true);
            saveData();
            //desactiva las particulas de los otros
            //desactiva el shader de los otros y el trigger
            for (int t = 0; t < checks.Length; t++)
            {
                checks[t].GetComponent<Animator>().SetBool("aparece", false);
                checks[t].GetComponent<Animator>().SetBool("desaparece", true);
                Destroy(clone2);


                if (t == 3)
                {
                    checks[t].GetComponent<Animator>().SetBool("aparece", true);
                    checks[t].GetComponent<Collider>().enabled = false;

                    clone3 = Instantiate(particulita, checks[3].transform.position, Quaternion.identity) as GameObject;
                    clone3.SetActive(true);
                    //particulita.transform.parent = checks[3].transform;
                }

            }


        }



    }






    void saveData()
    {
        //posicion del pj
        PlayerPrefs.SetFloat("PosPjX", this.transform.position.x);
        PlayerPrefs.SetFloat("PosPjY", this.transform.position.y);
        PlayerPrefs.SetFloat("PosPjZ", this.transform.position.z);
        

        //coins
        PlayerPrefs.SetInt("score", Score_Manager.score);



        //vida al 100% para reaparecer
        PlayerPrefs.SetInt("vidaPj", 100);


        //cam
        PlayerPrefs.SetFloat("PosCamX", cam.transform.position.x);
        PlayerPrefs.SetFloat("PosCamY", cam.transform.position.y);
        PlayerPrefs.SetFloat("PosCamZ", cam.transform.position.z);



    }

    IEnumerator fade()
    {
       
        CameraFollowFinal.smoothingMov = 1000;
        //PlayerHealthNode.Revive = true;
        caidita = false;
        //fadeIn
        canvasTransicion.GetComponent<Animator>().SetBool("aparece", true);
       yield return new WaitForSeconds(2f);


        transform.position = new Vector3(posX, posY, posZ);
        

        posX = PlayerPrefs.GetFloat("PosPjX");
        posY = PlayerPrefs.GetFloat("PosPjY");
        posZ = PlayerPrefs.GetFloat("PosPjZ");
        

        posCamX = PlayerPrefs.GetFloat("PosCamX");
        posCamY = PlayerPrefs.GetFloat("PosCamY");
        posCamZ = PlayerPrefs.GetFloat("PosCamZ");

        cam.transform.position = new Vector3(posCamX, posCamY, posCamZ);
        
        CameraFollowFinal.smoothingMov = 10000;

        //cuando la posicion de la cam sea la del playerpref que haga al fade out, para no ver el movimiento de camara
        if (cam.transform.position == new Vector3(posCamX, posCamY, posCamZ))
        {
            
            //FadeOut
            canvasTransicion.GetComponent<Animator>().SetBool("aparece", false);
            yield return new WaitForSeconds(4f);
            canvasTransicion.GetComponent<Animator>().SetBool("desaparece", true);
            
        }

        //canvasTransicion.GetComponent<Animator>().SetBool("desaparece", true);
        yield return false;

    }

}
