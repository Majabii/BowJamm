using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class ArrowNew : MonoBehaviour
{
    public Rigidbody body;

    public Transform arrow;

    private bool released = false;

    public void release()
    {
        body.useGravity = true;

        released = true;
    }
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (released)
        {
            Vector3 velocity = body.velocity.normalized;
            Quaternion targetRotation = Quaternion.LookRotation(velocity);
            transform.rotation = targetRotation;
        }
    }
}
