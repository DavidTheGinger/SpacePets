using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meteor_spawner : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject meteor;
    public int meteor_ring_amt = 20;
    public int max_rot_speed = 90;
    public float spacing = 2f;


    void Start()
    {
        //Debug.Log("we're here at least");
        for(int i = 0; i < meteor_ring_amt; i++)
        {
            float d0 = 360f / (i * 6f);
            for (int j = 0; j < i * 6; j++)
            {
                
                float x_pos = transform.position.x + Mathf.Cos(j * Mathf.Deg2Rad * d0) * i * Random.Range(1, spacing) * spacing;
                float y_pos = transform.position.y + Mathf.Sin(j * Mathf.Deg2Rad * d0) * i * Random.Range(1, spacing) * spacing;
                //Debug.Log("I'm doing a thing :" + x_pos + ", " + y_pos );
                GameObject temp_meteor = Instantiate(meteor, new Vector3(x_pos, y_pos, 0f), Quaternion.identity);
                temp_meteor.GetComponent<scr_meteor>().set_spin(coordinate_spin(x_pos, y_pos));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 coordinate_spin(float x, float y)
    {
        return new Vector3(x % max_rot_speed, y % max_rot_speed, (x * y) % max_rot_speed);
    }
    
}
