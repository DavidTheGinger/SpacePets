using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Hook{

    public Vector3 start_position;
    public Vector3 target_position;



}

public class Scr_player_movement : MonoBehaviour
{


    Vector2 left_hook_start_position;
    Vector2 left_hook_target_position;
    Vector2 right_hook_start_position;
    Vector2 right_hook_target_position;
    public GameObject left_hook_obj;
    public GameObject right_hook_obj;
    public GameObject left_hook_start_pos_obj;
    public GameObject right_hook_start_pos_obj;
    public float range = 5f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")){
            left_hook_start_position = new Vector2(left_hook_start_pos_obj.transform.position.x, left_hook_start_pos_obj.transform.position.y);
            left_hook_target_position = Find_hook_target(left_hook_start_position, new Vector2(-transform.right.x, -transform.right.y),range);
        }
    }

    Vector3 Find_hook_target(Vector2 origin, Vector2 direction, float range)
    {
        Vector3 target = Vector3.zero;
        Collider2D[] potential_targets = Physics2D.OverlapCircleAll(origin, range, 1 << 6);
        foreach(Collider2D potential_target in potential_targets)
        {
            Debug.Log(potential_target.transform.position);

        }

        return target;
    }

}
