using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cargaEnemigos : MonoBehaviour
{
    public int numEnemigos;
    public GameObject enemigos;
    public GameObject spawnEnemigos;
    Vector3 rdmEnemy;
    static public bool dentro;
    Collider col;



    void Start()
    {
        dentro = false;
        col = gameObject.GetComponent<Collider>();

    }

    
    void Update()
    {
        if (!dentro)
        {
            col.enabled = true;

        }

    }

    private void OnTriggerEnter(Collider other)
    {
       // if (!dentro)
        //{

            if (other.transform.tag == "Player")
            {


                for (int i = 0; i < numEnemigos; i++)
                {
                    
                    Instantiate(enemigos, spawnEnemigos.transform.position + new Vector3(Random.Range(-4.0f+i, 4.0f), 0, Random.Range(-4.0f, 4.0f)), Quaternion.identity);
                    print("enemies");
                }

                    dentro = true;
            }

        //}
        //if (dentro)
        //{

           // col.enabled = false;
       // }
    }
}
