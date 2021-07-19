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
    public GameObject debug_box_prefab;
    public GameObject tractor_beam_particles_prefab;
    public GameObject repulse_beam_particles_prefab;
    GameObject tractor_beam;
    GameObject repulse_beam;
    GameObject debug_circle;
    GameObject debug_box;
    
    [SerializeField] private Scr_camera cam;

    [SerializeField] private Rigidbody2D rb;

    public float range = 5f;
    public float pull_force = 1f;
    public float rotate_force = .5f;


    float prev_vel = 0f;
    float current_vel = 0f;
    float vel_delta = 0f;
    float impact_threshold = 10f;

    // Start is called before the first frame update
    void Start()
    {
        impact_threshold = pull_force * 3f;
    }

    // Update is called once per frame
    void Update()
    {
        current_vel = rb.velocity.magnitude;
        vel_delta = Mathf.Abs(current_vel - prev_vel) / Time.deltaTime;
        if (Input.GetButton("Fire1"))
        {
            //left_hook_start_position = new Vector2(left_hook_start_pos_obj.transform.position.x + -transform.right.x * range, left_hook_start_pos_obj.transform.position.y + -transform.right.y * range);
            //left_hook_target_position = Find_auto_hook_target(left_hook_start_position, new Vector2(-transform.right.x, -transform.right.y), range);
            left_hook_target_position = Find_hook_target(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position, range);
            if (left_hook_target_position !=  new Vector2(transform.position.x,transform.position.y))
            {
                Pull_self_Towards(left_hook_target_position);
                Stabalize_Rotation(rotate_force * Time.deltaTime);
                if (tractor_beam == null)
                {
                    tractor_beam = Instantiate(tractor_beam_particles_prefab);
                }
                tractor_beam.transform.LookAt(left_hook_target_position);
                tractor_beam.transform.position = transform.position;
            }
            else
            {
                Destroy(tractor_beam);
            }
            
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            if(tractor_beam != null)
            {
                Destroy(tractor_beam);
            }
            
        }
        if (Input.GetButton("Fire2"))
        {

            right_hook_target_position = Find_hook_target(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position, range);
            if (right_hook_target_position != new Vector2(transform.position.x, transform.position.y))
            {
                Push_self_Away(right_hook_target_position);
                if (repulse_beam == null)
                {
                    repulse_beam = Instantiate(repulse_beam_particles_prefab);
                }
                repulse_beam.transform.LookAt(transform.position);
                repulse_beam.transform.position = right_hook_target_position;
                //new Vector3(right_hook_target_position.x, right_hook_target_position.y, 0f)
            }
            else
            {
                Destroy(repulse_beam);
            }
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            if (repulse_beam != null)
            {
                Destroy(repulse_beam);
            }

        }

        if(vel_delta > impact_threshold)
        {
            Debug.Log("I've had an impact of: " + (vel_delta));
            if(vel_delta > impact_threshold * 2f)
            {
                Debug.Log("OW! FUCK!");
                cam.Disable_gyrolock();
            }
        }

        prev_vel = current_vel;
    }


    Vector3 Find_hook_target(Vector2 target, float range)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, target, range, 1 << 6);
        Debug.DrawRay(transform.position, target, Color.red);
        if(hit.collider != null)
        { 
            Debug.DrawLine(transform.position, hit.collider.gameObject.transform.position, Color.green);
            return hit.collider.gameObject.transform.position;
        }
        else
        {
            return transform.position;
        }
    }

    Vector3 Find_auto_hook_target(Vector2 origin, Vector2 direction, float range)
    {
        Vector3 target = Vector3.positiveInfinity;
        Collider2D[] potential_targets = Physics2D.OverlapBoxAll(origin, new Vector2 (range*2,range*2), transform.rotation.z, 1 << 6);
        //Debug.Log("Direction to check:" + direction);

        if (debug_box == null)
        {
            debug_box = Instantiate(debug_box_prefab, new Vector3(origin.x, origin.y, 0f), transform.rotation);
            debug_box.GetComponent<Scr_debug_circle>().Set_Radius(range);
        }
        else
        {
            debug_box.GetComponent<Scr_debug_circle>().Set_Position(new Vector3(origin.x, origin.y, 0f));
            debug_box.GetComponent<Scr_debug_circle>().Set_Rotation(transform.rotation);
        }


        float temp_mag = Mathf.Infinity;
        foreach (Collider2D potential_target in potential_targets)
        {
            //Debug.Log("Meteor - this position:" + (potential_target.transform.position - transform.position));
            float temp_potential_mag = Vector3.Magnitude(potential_target.transform.position - transform.position);
            Debug.Log("looking at a potential item at position = " + potential_target.transform.position);
            if ( temp_potential_mag < temp_mag && temp_potential_mag <= range)
            {
                target = potential_target.transform.position;
                temp_mag = temp_potential_mag;
                Debug.Log("Found new closer target with distance = " + range);
            }
        }
        Debug.Log("Target: " +target);
        return target;
    }



    void Pull_self_Towards(Vector3 target)
    {
        rb.AddForce((target - transform.position) * pull_force * Time.deltaTime);
        
    }


    void Push_self_Away(Vector3 target)
    {
        rb.AddForce((transform.position - target) * pull_force * Time.deltaTime);
    }


    void Stabalize_Rotation(float delta)
    {
        rb.angularVelocity = Mathf.Lerp(rb.angularVelocity, 0f, delta);
    }
}
