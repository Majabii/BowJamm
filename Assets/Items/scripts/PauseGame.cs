using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class PauseGame : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject bow;
    public GameObject terrain;

    public InputAction pauze_button;

    // Start is called before the first frame update
    void Start()
    {
        pauze_button.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (pauze_button.triggered)
        {
            Debug.Log("Pauze button pressed");
            pauseMenu.SetActive(true);
            bow.SetActive(false);
            terrain.SetActive(false);
            // TODO pauze other game elements?
        }
    }
}
