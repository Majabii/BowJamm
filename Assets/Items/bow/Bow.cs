using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class Bow : MonoBehaviour
{
    public LineRenderer string_;
    public Transform pivot_top, pivot_bot;
    public Transform attach_top, attach_bot;

    public InputAction string_pull;
    public InputAction bow_hold;

    public Transform bow;
    public Transform handle_hand;
    public Transform string_hand;

    private bool string_is_held = false;
    private bool bow_is_held = false;

    // Start is called before the first frame update
    void Start()
    {
        // Allow the input to be detected
        string_pull.Enable();
        string_pull.performed += String_pull_performed;
        string_pull.canceled += String_pull_canceled;

        bow_hold.Enable();
        bow_hold.performed += Bow_hold_performed;
    }


    // Update is called once per frame
    void Update()
    {
        if (bow_is_held)
        {
            MoveBow(handle_hand.position, handle_hand.rotation);
            if (string_is_held)
            {
                MoveString(attach_top.position, string_hand.position, attach_bot.position);
            } 
            else
            {
                ResetString(attach_top.position, attach_bot.position);
            }
        }

        // check of vast
        // string object naar achter trekken (positie staat op lijn tussen start on end, closest point on line segment 
        // hand.position, string_start.position
        // string_object.position
        // lijn op basis van vector van boog
    }

    private void Bow_hold_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Bow Hold Triggered");
        bow_is_held = !bow_is_held;
    }

    private void String_pull_canceled(InputAction.CallbackContext obj)
    {
        Debug.Log("String Held Stopped");
        ResetString(attach_top.position, attach_bot.position);
        // fire
        string_is_held = false;
    }

    private void String_pull_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("String Held Started");
        string_is_held = true;
    }

    void MoveBow(Vector3 pos, Quaternion rot)
    {
        bow.SetPositionAndRotation(pos, rot);
    }

    void MoveString(Vector3 top, Vector3 mid, Vector3 bot)
    {
        string_.SetPosition(0, top);
        string_.SetPosition(1, mid);
        string_.SetPosition(2, bot);
    }

    void ResetString(Vector3 top, Vector3 bot)
    {
        MoveString(top, (top + bot) / 2, bot);
    }
}
