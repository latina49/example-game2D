using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Control : MonoBehaviour
{
    private Rigidbody2D rigid;
    public float V_Run = 5;
    private Animator animator;
    public GameObject platform;

    public enum State
    {
        MoveLeft,MoveRight,Stand, Attack
    }
    State state;
    public void Init()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
        state = State.MoveLeft;
        isLive = true;
    }

    public float state_time;
    float current_time;
    public bool isLive;
    public void UpdateMovement(Vector3 c_position)
    {
        if (!isLive)
            return;
        float scale = platform.transform.localScale.x;
        float off = platform.GetComponent<BoxCollider2D>().offset.x*scale;
        float width = platform.GetComponent<BoxCollider2D>().size.x*scale;
        switch (state)
        {
            case State.MoveLeft:
                rigid.velocity = new Vector2(- V_Run, rigid.velocity.y);
                if (transform.position.x < platform.transform.position.x + off - width * 0.45f)
                {
                    state = State.Stand;
                    current_time = state_time;
                    rigid.transform.eulerAngles = new Vector3(0, 180, 0);

                }
                break;
            case State.Stand:
                rigid.velocity = new Vector2(0, rigid.velocity.y);
                if (current_time > 0)
                {
                    current_time -= Time.deltaTime;
                }
                else
                {
                    if (transform.position.x > platform.transform.position.x+off)
                        state = State.MoveLeft;
                    else
                        state = State.MoveRight;
                }
                break;
            case State.MoveRight:
                rigid.velocity = new Vector2(V_Run, rigid.velocity.y);
                if (transform.position.x > platform.transform.position.x+ off + width * 0.45f)
                {
                    state = State.Stand;
                    current_time = state_time;
                    rigid.transform.eulerAngles = new Vector3(0, 0, 0);
                }
                break;
        }
        CheckPlayPosition(c_position);
    }
    public Vector2 hit_range = new Vector2(10, 1);
    public bool shoot = false;
    void CheckPlayPosition(Vector3 c_position)
    {
        switch (state)
        {
            case State.MoveLeft:
                if (rigid.transform.position.x - c_position.x > 0 && rigid.transform.position.x - c_position.x < hit_range.x)
                    shoot = true;
                break;
        }
    }

    public void Die()
    {
        isLive = false;
        Destroy(gameObject);
    }
}
