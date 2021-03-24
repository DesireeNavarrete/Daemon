using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemigoInfierno : MonoBehaviour
{
    NavMeshAgent agent;
    public float velocidad;
    Transform target;
    Animator anim;
    int cont;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = GameObject.FindWithTag("Player").transform; //target the player
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("popeoEnemigo", true);


    }

    void Update()
    {
      
       StartCoroutine(Espera());

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, velocidad * Time.deltaTime);
        agent.SetDestination(target.transform.position);
   
    }

    private IEnumerator Espera()
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(0.5f);
        agent.isStopped = false;

    }
}
