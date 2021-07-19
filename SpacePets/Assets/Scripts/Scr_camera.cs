using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_camera : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private Camera cam;
    [SerializeField] private bool gyrolock = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        if (!gyrolock)
        {
            transform.rotation = player.transform.rotation;
        }
    }

    public void Set_size(float size)
    {
        cam.orthographicSize = size;
    }

    public void Set_rotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }

    public void Set_gyrolock(bool gyro)
    {
        gyrolock = gyro;
    }

    public void Disable_gyrolock()
    {
        transform.SetParent(player.transform);
    }

    public void Enable_gyrolock()
    {
        transform.SetParent(null);
    }
}
