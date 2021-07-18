using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_debug_circle : MonoBehaviour
{

    [SerializeField] private CircleCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Set_Position(Vector3 position)
    {
        transform.position = position;
    }

    public void Set_Radius(float radius)
    {
        transform.localScale = new Vector3(radius * 2, radius * 2, radius * 2);
    }
}
