using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BDFFolow : MonoBehaviour
{
    public GameObject target;
    float offset_x;
    float target_offset_x;
    public float follow_factor = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        offset_x = transform.position.x;
        target_offset_x = target.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float move_x = target.transform.position.x - target_offset_x;
        float target_x = offset_x + move_x* follow_factor;
        transform.position = new Vector3(target_x, transform.position.y, transform.position.z);
    }
}
