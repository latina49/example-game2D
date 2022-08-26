using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    public void Init(Vector3 position, Vector3 rotate, float speed, float lifetime)
    {
        transform.position = position;
        transform.eulerAngles = rotate;
        Invoke(nameof(DestroyBullet), lifetime);
        this.speed = speed;
    }

    void DestroyBullet()
    {
        GameObject.Destroy(this.gameObject);
    }
}
