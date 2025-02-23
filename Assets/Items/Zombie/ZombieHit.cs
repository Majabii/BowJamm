using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using static UnityEngine.EventSystems.EventTrigger;

public class ZombieHit : MonoBehaviour
{
    Animator anim;
    NavMeshAgent agent;
    private ScoreManager scoreManager;
    private bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arrow") && !dead)
        {
            Destroy(collision.gameObject);

            anim.SetTrigger("Die");

            agent.speed = 0;

            StartCoroutine(DestroyAfterDelay());

            scoreManager.AddScore(1);

            dead = true;
        }
    }
    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(5);

        Destroy(gameObject);
    }
}
