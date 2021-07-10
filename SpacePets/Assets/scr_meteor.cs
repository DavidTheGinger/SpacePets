using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_meteor : MonoBehaviour
{

    float xRotSpeed = 0f;
    float yRotSpeed = 0f;
    float zRotSpeed = 0f;
    public float rotationSpeedRange = 50f;

    // Start is called before the first frame update
    void Start()
    {
        xRotSpeed = Random.Range(-1f * rotationSpeedRange, rotationSpeedRange);
        yRotSpeed = Random.Range(-1f * rotationSpeedRange, rotationSpeedRange);
        zRotSpeed = Random.Range(-1f * rotationSpeedRange, rotationSpeedRange);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(xRotSpeed * Time.deltaTime, yRotSpeed * Time.deltaTime, zRotSpeed * Time.deltaTime);
    }
}
