using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject Character;
    public Vector3 offset;
    public float smooth = 10;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - Character.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float x = Mathf.Clamp(Character.transform.position.x + offset.x, 0, 31);
        float y = Mathf.Clamp(Character.transform.position.y + offset.y, 5, 7.65f);
        Vector3 targetPosition = new Vector3(Character.transform.position.x, Character.transform.position.y, -10);
        transform.position = Vector3.Lerp(transform.position, targetPosition+offset, Time.deltaTime * smooth);
    }
    
}
