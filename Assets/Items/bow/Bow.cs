using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class Bow : MonoBehaviour
{
    public LineRenderer string_;
    public Transform pivot_top, pivot_bot;
    public Transform attach_top, attach_bot;

    public InputAction string_pull;
    public InputAction bow_hold;
    public InputAction switch_hand;
    private bool right_handed = true;

    public Transform bow;
    public Transform handle_hand;
    public Transform string_hand;
    public Transform grab_location;
    public Transform front_of_bow;

    public GameObject arrowPrefab;
    private GameObject arrow;

    private bool string_is_held = false;
    private bool bow_is_held = false;

    // used for haptic feedback
    private XRNode leftHandNode = XRNode.LeftHand;
    private XRNode rightHandNode = XRNode.RightHand;

    private UnityEngine.XR.InputDevice leftHandDevice;
    private UnityEngine.XR.InputDevice rightHandDevice;

    // Start is called before the first frame update
    void Start()
    {
        // Allow the input to be detected
        string_pull.Enable();
        string_pull.performed += String_pull_performed;
        string_pull.canceled += String_pull_cancelled;

        bow_hold.Enable();
        bow_hold.performed += Bow_hold_performed;
        bow_hold.canceled += Bow_hold_cancelled;

        switch_hand.Enable();

        leftHandDevice = InputDevices.GetDeviceAtXRNode(leftHandNode);
        rightHandDevice = InputDevices.GetDeviceAtXRNode(rightHandNode);
    }

    
    // Update is called once per frame
    void Update()
    {
        if (switch_hand.triggered)
        {
            Debug.Log("Hand Switch");
            SwitchHand();
        }

        if (bow_is_held)
        {
            MoveBow(handle_hand.position, handle_hand.rotation);
            if (string_is_held)
            {
                MoveString(attach_top.position, string_hand.position, attach_bot.position);
                HoldArrow();

                // drop arrow if closer to the front of the bow than the string (and thus scuffed)
                float front_distance = Vector3.Distance(front_of_bow.position, string_hand.position);
                float string_distance = Vector3.Distance(grab_location.position, string_hand.position);

                if (front_distance < string_distance)
                {
                    drop_arrow();
                    // TODO FIX
                }

                // haptic feedback
                if (string_distance > 1f) { string_distance = 1f; }
                if (right_handed)
                {
                    rightHandDevice.SendHapticImpulse(0, string_distance);
                }
                else
                {
                    leftHandDevice.SendHapticImpulse(0, string_distance);
                }
            } 
            else
            {
                ResetString();
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
        Debug.Log("Bow Hold Start");
        bow_is_held = true;
    }

    private void Bow_hold_cancelled(InputAction.CallbackContext obj)
    {
        Debug.Log("Bow Hold End");
        bow_is_held = false;
        drop_arrow();
    }

    private void String_pull_cancelled(InputAction.CallbackContext obj)
    {
        Debug.Log("String Held Stopped");

        if (bow_is_held & string_is_held)
        {
            Fire_arrow();
        }
        ResetString();
        string_is_held = false;
    }

    private void String_pull_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("String Held Started");

        // try to grab the string (and create an arrow)
        // only do it if a) you are holding the bow
        // and b) you are close enough to the grab location
        if (bow_is_held & !string_is_held)
        {
            float distance = Vector3.Distance(string_hand.position, grab_location.position);
            if (distance < 0.1)
            {
                string_is_held = true;
                arrow = Instantiate(arrowPrefab, string_hand.position, string_hand.rotation);
                Debug.Log("Instantiated Arrow");
            }
        }
    }

    void Fire_arrow()
    {
        if (arrow != null)
        {
            ArrowNew arrow_script = arrow.GetComponent<ArrowNew>();
            arrow_script.release();

            Vector3 arrow_direction = handle_hand.position - string_hand.position;

            float distance = Vector3.Distance(handle_hand.position, string_hand.position);

            float force;
            if (distance > 0.3f)
            {
                force = 0.25f;
            }
            else
            {
                force = 0.1f;
            }

            arrow_script.body.AddForce(arrow_direction * force, ForceMode.Impulse);

            arrow = null;

            ResetString();
        }
    }

    void drop_arrow()
    {
        if (arrow != null)
        {
            ArrowNew arrow_script = arrow.GetComponent<ArrowNew>();
            arrow_script.release();
            arrow = null;
            ResetString();
        }
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

    void ResetString()
    {
        Vector3 top = attach_top.position;
        Vector3 bot = attach_bot.position;
        MoveString(top, (top + bot) / 2, bot);
    }

    void SwitchHand()
    {
        // Swap hand locations
        Transform tmp = handle_hand;
        handle_hand = string_hand;
        string_hand = tmp;


        // Swap hotkeys
        bow_hold.Disable();
        string_pull.Disable();

        string_pull.performed -= String_pull_performed;
        string_pull.canceled -= String_pull_cancelled;
        bow_hold.performed -= Bow_hold_performed;
        bow_hold.canceled -= Bow_hold_cancelled;

        InputAction tmp2 = bow_hold;
        bow_hold = string_pull;
        string_pull = tmp2;

        string_pull.performed += String_pull_performed;
        string_pull.canceled += String_pull_cancelled;
        bow_hold.performed += Bow_hold_performed;
        bow_hold.canceled += Bow_hold_cancelled;

        bow_hold.Enable();
        string_pull.Enable();

        // update bool
        right_handed = !right_handed;
    }

    void HoldArrow()
    {
        Transform arrow_position = arrow.GetComponent<Transform>();
        Vector3 direction = handle_hand.position - string_hand.position;
        arrow_position.SetPositionAndRotation(string_hand.position, Quaternion.LookRotation(direction));
        Debug.Log(arrow_position);
    }
}
