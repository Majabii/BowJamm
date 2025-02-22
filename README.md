using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bow : MonoBehaviour
{
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
            // check of vast
            // string object naar achter trekken (positie staat op lijn tussen start on end, closest point on line segment 
            // hand.position, string_start.position
            // string_object.position
            // lijn op basis van vector van boog
        }
    }
}


private float CalculatePull(Vector3 pullPosition)
    {
        Vector3 pullDirection = pullPosition - start.position;
        Vector3 targetDirection = end.position - start.position;
        float maxLength = targetDirection.magnitude;

        targetDirection.Normalize();
        float pullValue = Vector3.Dot(pullDirection, targetDirection) / maxLength;
        return Mathf.Clamp(pullValue, 0, 1);
    }

    private void UpdateString() 
    {
        Vector3 linePosition = Vector3.forward * Mathf.Lerp(start.transform.localPosition.z, end.transform.localPosition.z, pullAmount);
        notch.transform.localPosition = new Vector3(notch.transform.localPosition.x, notch.transform.localPosition.y, linePosition.z + .2f);
        _string.SetPosition(1, linePosition);
    }
