using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public Transform target;
    void LateUpdate()
    {
        transform.position = new Vector3(0.5f, target.transform.position.y, 0);
    }
}
