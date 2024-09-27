using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    private Vector3 offSet;

    void Start()
    {
        offSet = transform.position - target.position;
    }


    void LateUpdate()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, offSet.z + target.position.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, 10 * Time.deltaTime);
    }
}
