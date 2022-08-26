using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ex2 : MonoBehaviour
{
    public SpriteRenderer tankbody;
    public SpriteRenderer tankcanon;
    public GameObject Bullet;
    public GameObject BulletSpawn;
    public GameObject missle;
    public GameObject MissleSpawn;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Time.deltaTime);
        //InputDemo1();
        //InputDemo2();
        //InputDemo3();
        InputDemo4();

    }

    //void InputDemo1()
    //{
    //    if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
    //    {
    //        tank.color = Color.red;
    //    }

    //    else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
    //    {
    //        tank.transform.position += new Vector3(-1 * Time.deltaTime, 0, 0);
    //    }

    //    else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
    //    {
    //        tank.color = Color.white;
    //    }

    //    if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
    //    {
    //        tank.color = Color.red;
    //    }

    //    else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
    //    {
    //        tank.transform.position += new Vector3(1 * Time.deltaTime, 0, 0);
    //    }

    //    else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
    //    {
    //        tank.color = Color.white;
    //    }
    //}

    public float speed = 1;
    public float smooth = 10;
    void InputDemo2()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        tankbody.transform.position += Time.deltaTime * speed * new Vector3(x, y, 0).normalized * Mathf.Clamp01(new Vector3(x, y, 0).magnitude);
        float width = 5f * 16f / 9f - 0.5f;
        float height = 5 - 0.5f;
        tankbody.transform.position = new Vector3(Mathf.Clamp(tankbody.transform.position.x, -width, width), Mathf.Clamp(tankbody.transform.position.y, -height, height), 0);
        float angle_0 = Vector3.SignedAngle(Vector3.up, new Vector3(x, y, 0), Vector3.forward);
        float angle_1 = Mathf.LerpAngle(tankbody.transform.localEulerAngles.z, angle_0, Time.deltaTime * smooth);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
            tankbody.transform.eulerAngles = new Vector3(0, 0, angle_1);
    }

    public float move_smooth = 1;
    public float rotate_smooth = 10;
    void InputDemo3()
    {
        Vector2 screen_position = Input.mousePosition;
        Vector3 world_position = Camera.main.ScreenToWorldPoint(new Vector3(screen_position.x, screen_position.y, -Camera.main.transform.position.z));
        if (Input.GetMouseButton(0))
        {
            tankbody.transform.position = Vector3.Lerp(tankbody.transform.position, world_position, move_smooth);
            Vector3 diection = world_position - tankbody.transform.position;
            float angle_0 = Vector3.SignedAngle(Vector3.up, diection, Vector3.forward);
            float angle_1 = Mathf.LerpAngle(tankcanon.transform.localEulerAngles.z, angle_0, Time.deltaTime * rotate_smooth);
            tankcanon.transform.localEulerAngles = new Vector3(0, 0, angle_1);
            tankbody.transform.eulerAngles = new Vector3(0, 0, 45);
        }

    }

    public bool autoFire;
    public bool autoFire2;
    void InputDemo4()
    {
        InputDemo2();
        Vector2 screen_position = Input.mousePosition;
        Vector3 world_position = Camera.main.ScreenToWorldPoint(new Vector3(screen_position.x, screen_position.y, -Camera.main.transform.position.z));
        if (Input.GetMouseButton(0))
        {
            Vector3 diection = world_position - tankbody.transform.position;
            float angle_0 = Vector3.SignedAngle(Vector3.up, diection, Vector3.forward);
            float angle_1 = Mathf.LerpAngle(tankcanon.transform.eulerAngles.z, angle_0, Time.deltaTime * rotate_smooth);
            tankcanon.transform.eulerAngles = new Vector3(0, 0, angle_1);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (autoFire)
            {
                autoFire = false;
                CancelInvoke();
            }
            else
            {
                autoFire = true;
                InvokeRepeating(nameof(CanonFire), 0, 0.2f);
            }    
                
            
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (autoFire2)
            {
                autoFire2 = false;
                CancelInvoke();
            }
            else
            {
                autoFire2 = true;
                InvokeRepeating(nameof(CanonFire2), 0, 2f);
            }


        }
    }
    void CanonFire()
    {
        GameObject g = GameObject.Instantiate(Bullet, null);
        Bullet b = g.GetComponent<Bullet>();
        b.Init(BulletSpawn.transform.position, BulletSpawn.transform.eulerAngles, 10, 2);
    }
    
    void CanonFire2()
    {
        GameObject g = GameObject.Instantiate(missle, null);
        Bullet b = g.GetComponent<Bullet>();
        b.Init(MissleSpawn.transform.position, MissleSpawn.transform.eulerAngles, 15, 3);
    }
}
