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
    public GameObject debug_circle_prefab;
    GameObject debug_circle;

    public float range = 5f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")){
            left_hook_start_position = new Vector2(left_hook_start_pos_obj.transform.position.x + -transform.right.x * range, left_hook_start_pos_obj.transform.position.y + -transform.right.y * range);
            left_hook_target_position = Find_hook_target(left_hook_start_position, new Vector2(-transform.right.x, -transform.right.y),range);
            if(debug_circle == null)
            {
                debug_circle = Instantiate(debug_circle_prefab, new Vector3(left_hook_start_position.x, left_hook_start_position.y, 0f), Quaternion.identity);
                debug_circle.GetComponent<Scr_debug_circle>().Set_Radius(range);
            }
            else
            {
                debug_circle.GetComponent<Scr_debug_circle>().Set_Position(new Vector3(left_hook_start_position.x, left_hook_start_position.y, 0f));
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            right_hook_start_position = new Vector2(right_hook_start_pos_obj.transform.position.x + transform.right.x * range, right_hook_start_pos_obj.transform.position.y + transform.right.y * range);
            right_hook_target_position = Find_hook_target(right_hook_start_position, new Vector2(transform.right.x, transform.right.y), range);
            if (debug_circle == null)
            {
                debug_circle = Instantiate(debug_circle_prefab, new Vector3(right_hook_start_position.x, right_hook_start_position.y, 0f), Quaternion.identity);
                debug_circle.GetComponent<Scr_debug_circle>().Set_Radius(range);
            }
            else
            {
                debug_circle.GetComponent<Scr_debug_circle>().Set_Position(new Vector3(right_hook_start_position.x, right_hook_start_position.y, 0f));
            }
        }

    }

    Vector3 Find_hook_target(Vector2 origin, Vector2 direction, float range)
    {
        Vector3 target = Vector3.positiveInfinity;
        Collider2D[] potential_targets = Physics2D.OverlapCircleAll(origin, range, 1 << 6);
        Debug.Log("Direction to check:" + direction);
        foreach (Collider2D potential_target in potential_targets)
        {
            Debug.Log("Meteor - this position:" + (potential_target.transform.position - transform.position));
            if(Vector3.Magnitude(potential_target.transform.position - transform.position) < Vector3.Magnitude(target - transform.position))
            {
                target = potential_target.transform.position;
            }
        }
        Debug.Log(target);
        return target;
    }

}
