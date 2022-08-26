using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    Rigidbody2D r;
    
    // Start is called before the first frame update
    void Start()
    {   
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Init(float V_0, float lifetime)
    {
        r = GetComponent<Rigidbody2D>();
        r.velocity = transform.right*V_0;
        Invoke(nameof(DestroyBullet), lifetime);
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
    public GameObject bum;
    public void CreateBum()
    {
        GameObject b = Instantiate(bum, null);
        b.transform.position = transform.position;
        b.transform.eulerAngles = transform.eulerAngles;
    }
    public int ignoreLayer;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer != ignoreLayer)
        {
            CreateBum();
            DestroyBullet();
            if (col.gameObject.layer == 7)
            {
                col.gameObject.GetComponent<Enemy_Control>().Die();
            }
            else if (col.gameObject.layer == 10)
            {
                Destroy(col.gameObject);
            }
                
        }
    }


}
