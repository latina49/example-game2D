using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ex : MonoBehaviour
{
    public Rigidbody2D r;
    public float speed = 2;
    GameObject Line;
    public GameObject line_prefab;
    private bool hit_ball;
    public float V_0 = 1;
    public GameObject ball;
    private bool isBall;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        Time.fixedDeltaTime = 1 / 60f;
        Ball();
    }
    private void FixedUpdate()
    {
        //float x = Input.GetAxis("Horizontal");
        //float y = Input.GetAxis("Vertical");
        //r.velocity = new Vector2(x * speed, r.velocity.y);

    }
    // Update is called once per frame
    void Update()
    {
        Input_1();
    }

    void Ball()
    {
        GameObject b = GameObject.Instantiate(ball, null);
        b.transform.position = new Vector3(-5.5f, -2.5f, 0);
        r = b.GetComponent<Rigidbody2D>();
        isBall = true;
    }
    void Input_1()
    {
        if (!isBall) return;
        Vector2 screen_position = Input.mousePosition;
        Vector3 world_position = Camera.main.ScreenToWorldPoint(new Vector3(screen_position.x, screen_position.y, -Camera.main.transform.position.z));
        
        if (Input.GetMouseButtonDown(0))
        {
            float distance = Vector3.Distance(r.transform.position, world_position);
            if (distance <= 0.5)
            { 
                r.transform.localScale = Vector3.one * 1.2f;
                hit_ball = true;
            }
        }

        else if (Input.GetMouseButton(0))
        {
            if (hit_ball)
            {
                if (Line == null)
                {
                    Line = Instantiate(line_prefab, null);
                }
                else
                {
                    Line.transform.position = (r.transform.position + world_position) /2;
                    float angle_0 = Vector3.SignedAngle(Vector3.right, r.transform.position - world_position, Vector3.forward);
                    Line.transform.localEulerAngles = new Vector3(0, 0, angle_0);
                    float distance = Vector3.Distance(r.transform.position, world_position);
                    Line.transform.localScale = new Vector3(distance, 0.1f, 1);
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            r.transform.localScale = Vector3.one;
            if (Line != null)
            {
                Destroy(Line);
            }
            if(hit_ball)
            {
                r.velocity = (r.transform.position - world_position).normalized * V_0 ;
                hit_ball = false;
                isBall = false;
                Invoke(nameof(Ball), 1);
            }

        }


    }




}
