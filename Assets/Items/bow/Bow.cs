using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bow : MonoBehaviour
{
    public LineRenderer string_;
    public Transform handle_top;
    public Transform handle_bot;

    public InputAction string_pull;

    public Transform string_pull_start;
    public Transform string_pull_end;

    public Transform bow;
    public Transform hand;
    public Transform string_midpoint;

    private bool is_held = false;

    // Start is called before the first frame update
    void Start()
    {
        // Allow the input to be detected
        string_pull.Enable();
        string_pull.performed += String_pull_performed;
        string_pull.canceled += String_pull_canceled;
    }

    private void String_pull_canceled(InputAction.CallbackContext obj)
    {
        Debug.Log("Bye World");
        is_held = false;
    }

    private void String_pull_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Hello World");
        is_held = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (is_held)
        {
            CalculatePull(hand.localPosition);
            // check of vast
            // string object naar achter trekken (positie staat op lijn tussen start on end, closest point on line segment 
            // hand.position, string_start.position
            // string_object.position
            // lijn op basis van vector van boog
        }
    }

    void CalculatePull(Vector3 hand)
    {
        string_.SetPosition(1, new Vector3(hand.x, 0, 0));
    }
}
