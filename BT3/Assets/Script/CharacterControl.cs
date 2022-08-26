using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterControl : MonoBehaviour
{
    private Rigidbody2D rigid;
    public float V_Run;
    public Animator animator;
    public GameObject Bullet;
    public GameObject BulletSpawn;
    public bool dead;
    Collider2D Collider;
    public ParticleSystem[] particle;
    public GameObject ButtonMenu;
    public Text Name;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        Time.fixedDeltaTime = 1f / 60f;
        Physics2D.IgnoreLayerCollision(9, 10, true);
        rigid = GetComponent<Rigidbody2D>();
        Name.text = DataStore.instance.playerModel[DataStore.instance.currentPlayerSelected].name;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool isGrounded;
    public bool shooting;
    private void FixedUpdate()
    {
        if (dead) return;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.1f, Layers.CanJump);
        isGrounded = hit.collider != null;
        float x = Input.GetAxis("Horizontal");
        float gound_vec = 0;
        if (isGrounded)
        {
            if (hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                gound_vec = hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity.x;
            }
        }
        
        if (Input.GetMouseButton(0))
        {
            if (!shooting)
            {
                shooting = true;
                InvokeRepeating(nameof(Fire), 0.1f, 0.25f);

            }
        }
        else
        {
            CancelInvoke();
            shooting = false;
        }
        if (isGrounded)
        {
            rigid.velocity = new Vector2(x * V_Run+ gound_vec, rigid.velocity.y);
            //if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            //    rigid.transform.eulerAngles = new Vector3(0, 180, 0);
            //else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            //    rigid.transform.eulerAngles = new Vector3(0, 0, 0);
            transform.eulerAngles = new Vector3(0, x > 0 ? 0 : (x < 0 ? 180 : transform.eulerAngles.y), 0);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (hit.collider.tag != "Stress")
                {
                    Collider = hit.collider;
                    Physics2D.IgnoreCollision(hit.collider, GetComponent<CapsuleCollider2D>(), true);
                }
                    
            }
            if (Mathf.Abs(x) > 0)
            {
                PlayAnimation(shooting ? "RunShoot" : "Run", 0, 0.5f);
            }
            else
            {
                PlayAnimation(shooting ? "IdleShoot" : "Idle", 0, 0.5f);
            }

        }
        else
        {
            PlayAnimation(shooting ? "JumpShoot" : "Jump", 0, 0.5f);
        }
        if (Collider != null && hit.collider != Collider)
        {
            Physics2D.IgnoreCollision(Collider, GetComponent<CapsuleCollider2D>(), false);
        }

    }

    public float jump_force = 600;
    void Jump()
    {
        rigid.AddForce(Vector2.up * jump_force);
    }

    public static class Layers
    {
        public const int Ground = 1 << 6;
        public const int Enemy = 1 << 7;
        public const int Character = 1 << 8;
        public const int Stress = 1 << 9;
        public const int dmgBall = 1 << 10;
        public const int CanJump = Stress | dmgBall;
        public const int AllLayers = Ground | Enemy | Character | Stress;
    }
    private string anim_name;
    private void PlayAnimation(string name, int fram_delay, float transition_time)
    {
        if (name != anim_name)
        {
            animator.Play(name, fram_delay, transition_time);
            anim_name = name;
        }
    }
    void Fire()
    {
        GameObject g = GameObject.Instantiate(Bullet, null);
        g.transform.position = BulletSpawn.transform.position;
        g.transform.eulerAngles = BulletSpawn.transform.eulerAngles;
        shoot b = g.GetComponent<shoot>();
        b.Init(50, 1f);
        for (int i=0; i < particle.Length; i++)
        {
            particle[i].Play();
        }
        AudioControl.instance.Play_Shoot();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "CheckDie")
        {
            SetLife(0);
        }
    }
    int Life = 3;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 10)
        {
            int l = Life - 1;
            SetLife(l);
            Physics2D.IgnoreCollision(col.collider, GetComponent<CapsuleCollider2D>(), true);
        }

    }
    private void SetLife(int life)
    {
        Life = life;
        if (Life == 0)
        {
            dead = true;
            rigid.simulated=false;
            ButtonMenu.SetActive(true);
        }
        UiControl.instance.SetHeart(Life);
    }

}
