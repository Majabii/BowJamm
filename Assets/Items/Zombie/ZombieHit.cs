using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.EventSystems.EventTrigger;

public class ZombieHit : MonoBehaviour
{
    Animator anim;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
            {
            Destroy(collision.gameObject);

            anim.SetTrigger("Die");

            agent.speed = 0;

            StartCoroutine(DestroyAfterDelay());
        }
    }
    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(5);

        Destroy(gameObject);
    }
}
