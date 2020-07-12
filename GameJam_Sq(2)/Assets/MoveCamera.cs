using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Const.CameraPositions cameraPosition = Const.CameraPositions.TABLE;
    public float speed = 1;

    private Vector3 tableTargetPosition = new Vector3(0, 0, 0);
    private Quaternion tableTargetRotation = new Quaternion(0, 0, 0, 0);

    private Vector3 furnanceTargetPosition = new Vector3(0.3f, 1.2f, 1.8f);
    private Quaternion furnanceTargetRotation = new Quaternion(0.2f, 0, 0, 1);

    private void Start()
    {
        tableTargetPosition = transform.position;
        tableTargetRotation = transform.localRotation;
        //Debug.Log(transform.localRotation);
    }

    // Update is called once per frame
    void Update()
    {
        switch (cameraPosition)
        {
            case Const.CameraPositions.TABLE:
                transform.position = Vector3.Lerp(transform.position, tableTargetPosition, Time.deltaTime * speed);
                transform.localRotation = Quaternion.Lerp(transform.localRotation, tableTargetRotation, Time.deltaTime * speed);
                break;

            case Const.CameraPositions.FURNANCE:
                transform.position = Vector3.Lerp(transform.position, furnanceTargetPosition, Time.deltaTime * speed);
                transform.localRotation = Quaternion.Lerp(transform.localRotation, furnanceTargetRotation, Time.deltaTime * speed);
                break;
        }
    }
}
