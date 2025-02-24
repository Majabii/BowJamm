using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    public GameObject gameview;
    public GameObject ui_play;
    public GameObject ui_tutorial;
    public GameObject ui_start;
    public GameObject death_screen;
    public GameObject zombie_manager;

    public GameObject music_play;
    public GameObject music_start;
        
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("zomb"))
        {
            gameview.SetActive(false);
            //ui_play.SetActive(false);
            ui_tutorial.SetActive(true);
            ui_start.SetActive(true);
            death_screen.SetActive(true);

            GameObject[] gos = GameObject.FindGameObjectsWithTag("zomb");
            foreach (GameObject go in gos)
                Destroy(go);

            GameObject[] gos� = GameObject.FindGameObjectsWithTag("Arrow");
            foreach (GameObject go in gos�)
                Destroy(go);

            ZombieSpawnController zc = zombie_manager.GetComponent<ZombieSpawnController>();
            zc.allowed = false;

            AudioSource mp = music_play.GetComponent<AudioSource>();
            mp.Stop();

            AudioSource ms = music_start.GetComponent<AudioSource>();
            ms.Play();
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("zomb"))
    //    {
    //        gameview.SetActive(false);
    //        ui_play.SetActive(false);
    //        ui_tutorial.SetActive(true);
    //        ui_start.SetActive(true);
    //        // deathscreen.SetActive(true);
    //    }
    //}
}
