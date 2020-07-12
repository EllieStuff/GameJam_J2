using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public Vector3 edge = Vector3.zero;
    public float speed = 1;

    const float MAX_X = 0.3f;
    const float MIN_X = -1f;
    const float MAX_Z = 1;
    const float MIN_Z = -1.9f;

    private Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPos.x + edge.x, transform.position.y + edge.y, newPos.z + edge.z);

        if (transform.position.x > MAX_X) transform.position = new Vector3(MAX_X, transform.position.y, transform.position.z);
        else if (transform.position.x < MIN_X) transform.position = new Vector3(MIN_X, transform.position.y, transform.position.z);

        if (transform.position.z > MAX_Z) transform.position = new Vector3(transform.position.x, transform.position.y, MAX_Z);
        else if (transform.position.z < MIN_Z) transform.position = new Vector3(transform.position.x, transform.position.y, MIN_Z);

    }
    
}
