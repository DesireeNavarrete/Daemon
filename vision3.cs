using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class vision3 : MonoBehaviour
{
    public GameObject[] vs;
    int cont, cd;
    public bool cooldown;//para controlar cuando se ha ejecutado la vision
    public GameObject planoVision;
    GameObject trgVision;
    //pospo
    public PostProcessingProfile pospo;

    public Animator Daemon;
    bool vision;
    void Start()
    {
        trgVision = GameObject.FindGameObjectWithTag("trigVision");

        //busca los objetos
        vs = GameObject.FindGameObjectsWithTag("vision");

        Daemon.GetComponent<Animator>().SetBool("Vision", vision);
        
        //desactiva la mesh renderer
        for (int i = 0; i < vs.Length; i++)
        {
            vs[i].GetComponent<MeshRenderer>().enabled = false;
            vs[i].GetComponentInParent<Collider>().enabled = false;
            vs[i].GetComponent<Animator>();
        }
        cont = 10;
        cd = 2;
        cooldown = false;

        //aberracion cromatica
        ChromaticAberrationModel.Settings aberrracion = pospo.chromaticAberration.settings;
        aberrracion.intensity = 0;
        pospo.chromaticAberration.settings = aberrracion;

        vision = false;

    }

    void Update()
    {
        Daemon.GetComponent<Animator>().SetBool("Vision", vision);

        if (Input.GetButton("R1") && cooldown == false || Input.GetKeyDown(KeyCode.V) && cooldown == false)
        {
            StartCoroutine(visionDemon());
            //vision = true;
            

            StopCoroutine(stop());
        }

        if (cooldown)//si se ha ejecutado la vision, se inicia el cd y se para la corutina hasta que cuente el cd
        {
            StartCoroutine(stop());
            //vision = false;
            

        }

        //si mueres con la vision activada, se desactiva
        if (PlayerHealthNode.vida <= 0 && cooldown==false)
        {
            cooldown = true;
            StartCoroutine(stop());


        }

        if (checkpoint.caidita == true)
        {
            cooldown = true;
            StartCoroutine(stop());

        }
        print(vision);
    }


    IEnumerator visionDemon()
    {
        planoVision.GetComponent<Animator>().SetBool("aparece", true);
        vision = true;


        for (int i = 0; i < vs.Length; i++)
        {
            vs[i].GetComponent<MeshRenderer>().enabled = true;
            vs[i].GetComponentInParent<Collider>().enabled = true;
            //vs[i].GetComponent<Animator>().SetBool("aparece", true);
            vs[i].GetComponent<Animator>().SetBool("aparece",true);

        }
        for (float i = 0; i <= 1; i += 0.01f)
        {
            //aberracion cromatica
            ChromaticAberrationModel.Settings aberrracion = pospo.chromaticAberration.settings;
            aberrracion.intensity = Mathf.Clamp(i,0.16f,1);
            pospo.chromaticAberration.settings = aberrracion;
        }


        //cooldown
        yield return new WaitForSeconds(cont);
        StartCoroutine(visionFuera());
    }

    IEnumerator visionFuera()
    {
       
        //desaparecen los objetos con el shader
        for (int i = 0; i < vs.Length; i++)
        {
            print("visionFuera");
            vs[i].GetComponentInParent<Collider>().enabled = false;
            vs[i].GetComponent<Animator>().SetBool("aparece",false);
            vs[i].GetComponent<Animator>().SetBool("desaparece",true);

        }
        for (float z = 1; z >= 0; z -= 0.01f)
        {
            //aberracion cromatica
            ChromaticAberrationModel.Settings aberrracion = pospo.chromaticAberration.settings;
            aberrracion.intensity = Mathf.Clamp(z, 0.16f, 1);
            pospo.chromaticAberration.settings = aberrracion;
        }

        planoVision.GetComponent<Animator>().SetBool("aparece", false);
        planoVision.GetComponent<Animator>().SetBool("desaparece", true);
        vision = false;


        yield return false;
        cooldown = true;

        //vision = true;

    }


    IEnumerator stop()
    {
        StopCoroutine(visionDemon());
        StopCoroutine(visionFuera());

        yield return new WaitForSeconds(cd);
        cooldown = false;
        //vision = false;

        //vision = false;
        //Daemon.GetComponent<Animator>().SetBool("Vision", false);
        StartCoroutine(cabezaOf());
    }


    private void OnTriggerEnter(Collider other)
    {

        //habilidad vision
        if (other.transform.tag == "trigVision")
        {
            StartCoroutine(desbloqueoVision());
            //StartCoroutine(visionDemon());

        }
    }

    IEnumerator desbloqueoVision()
    {
        //bloqueo mov mientras se ejecuta la vison
        StartCoroutine(visionDemon());
        vision = true;
        yield return new WaitForSeconds(1);
        PlayerController1.stopAllCharacter = true;
        yield return new WaitForSeconds(2);
        PlayerController1.stopAllCharacter = false;
        trgVision.GetComponent<Collider>().enabled = false;

        //desactiva la cabeza
        yield return new WaitForSeconds(10);
        vision = false;
    }

    IEnumerator cabezaOf()
    {
        //desactiva la cabeza
        yield return new WaitForSeconds(.5f);
        //vision = false;
        print("v");

    }
}
