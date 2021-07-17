using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MeteorRing
{
    public GameObject[] meteors;
    public int max_rot_speed = 90;
    public int rot_disadvantage_amt = 10;
    public float spacing = 2f;
    public Vector3 location;
    public int ring_layer_start = 0;
    public int ring_layer_finish = 20;

    public MeteorRing()
    {
        max_rot_speed = 90;
        rot_disadvantage_amt = 10;
        spacing = 2f;
        ring_layer_start = 0;
        ring_layer_finish = 20;
    }

}

public class meteor_spawner : MonoBehaviour
{
    // Start is called before the first frame update

    
    

    

    public MeteorRing[] meteor_rings;

    void Start()
    {
        //Debug.Log("we're here at least");
        foreach(MeteorRing meteor_ring in meteor_rings)
        {
            create_meteor_ring(meteor_ring);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void create_meteor_ring(MeteorRing meteor_ring)
    {
        for (int i = meteor_ring.ring_layer_start; i < meteor_ring.ring_layer_finish; i++)
        {
            float d0 = 360f / (i * 6f);
            for (int j = 0; j < i * 6; j++)
            {

                float x_pos = meteor_ring.location.x + Mathf.Cos(j * Mathf.Deg2Rad * d0) * i * meteor_ring.spacing + Random.Range(-meteor_ring.spacing / 3f, meteor_ring.spacing/3f);
                float y_pos = meteor_ring.location.y + Mathf.Sin(j * Mathf.Deg2Rad * d0) * i * meteor_ring.spacing + Random.Range(-meteor_ring.spacing / 3f, meteor_ring.spacing/3f);
                //Debug.Log("I'm doing a thing :" + x_pos + ", " + y_pos );
                GameObject temp_meteor = Instantiate(meteor_ring.meteors[j % meteor_ring.meteors.Length], new Vector3(x_pos, y_pos, 0f), Quaternion.identity);
                temp_meteor.GetComponent<scr_meteor>().set_spin(random_spin(meteor_ring.max_rot_speed, meteor_ring.rot_disadvantage_amt));
                temp_meteor.GetComponent<scr_meteor>().set_rotation(random_rotation());
            }
        }
    }

    private Vector3 coordinate_spin(float x, float y, float max_rot_speed)
    {
        float x_rot = Mathf.Cos(x) * x % max_rot_speed;
        float y_rot = Mathf.Sin(y) * y % max_rot_speed;
        float z_rot = Mathf.Sin(x * y) * (x * y) % max_rot_speed;

        return new Vector3(x_rot, y_rot, z_rot);
    }

    private Vector3 random_spin(float max_rot_speed, int rot_disadvantage_amt)
    {
        float x_rot = random_float_disadvantage(max_rot_speed, rot_disadvantage_amt);
        float y_rot = random_float_disadvantage(max_rot_speed, rot_disadvantage_amt);
        float z_rot = random_float_disadvantage(max_rot_speed, rot_disadvantage_amt);

        return new Vector3(x_rot, y_rot, z_rot);
    }

    private float random_float_disadvantage(float range, int rolls)
    {
        float result = Random.Range(-range, range);
        for(int i = 1; i < rolls; i++)
        {
            float temp = Random.Range(-range, range);
            if(Mathf.Abs(temp) < Mathf.Abs(result))
            {
                result = temp;
            }
        }
        return result;
    }

    private Vector3 random_rotation()
    {
        return new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
    }


}
