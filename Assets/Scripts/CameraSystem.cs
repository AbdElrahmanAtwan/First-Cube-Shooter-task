using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{

    public GameObject Cm;
    private float xOffset;
    private float zOffset;
    private Camera cam;

    void Start()
    {
        Cm = GameObject.FindGameObjectWithTag("MainCamera");
        cam = Cm.GetComponent<Camera>();

        xOffset = cam.transform.position.x - transform.position.x;
        zOffset = cam.transform.position.z - transform.position.z;
    }

    void LateUpdate()
    {
        cam.transform.position = new Vector3(transform.position.x + xOffset, cam.transform.position.y, transform.position.z + zOffset);
    }
}
