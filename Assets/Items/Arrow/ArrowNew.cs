using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowNew : MonoBehaviour
{
    public Rigidbody tip;
    public Rigidbody shaft;
    public Rigidbody feathers;

    public Transform arrow;

    private bool released = false;

    public void release()
    {
        tip.useGravity = true;
        shaft.useGravity = true;
        feathers.useGravity = true;

        released = true;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(tip);
        Debug.Log(shaft);
        Debug.Log(feathers);
    }

    // Update is called once per frame
    void Update()
    {
        if (released)
        {
            Vector3 velocity = shaft.velocity.normalized;
            Debug.Log(velocity);
            Quaternion targetRotation = Quaternion.LookRotation(velocity);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }
}
