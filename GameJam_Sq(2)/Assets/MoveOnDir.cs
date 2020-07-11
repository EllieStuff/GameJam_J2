using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnDir : MonoBehaviour
{
    //public Vector3 direction;
    public Vector3 position;

    //private Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        //body = GetComponent<Rigidbody>();
        position = transform.position;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //body.MovePosition(transform.position +  direction * Time.deltaTime);
        transform.position = position;
    }
}
