using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_meteor : MonoBehaviour
{

    Vector3 RotSpeed = Vector3.zero;
    public float rotationSpeedRange = 50f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(RotSpeed.x * Time.deltaTime, RotSpeed.y * Time.deltaTime, RotSpeed.z * Time.deltaTime);
    }


    public void set_spin(Vector3 tempRotSpeed)
    {
        RotSpeed = tempRotSpeed;
    }
}
