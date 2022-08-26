using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UiControl : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] heart;
    public static UiControl instance;
    void Start()
    {
        instance = this;
        heart =new GameObject [transform.Find("Heart").transform.childCount];
        int i = 0;
        foreach (Transform g in transform.Find("Heart").transform)
        {
            heart[i] = g.gameObject;
            i++;
        }
        
    }

    public void SetHeart(int life)
    {
        for(int i = 0; i < heart.Length; i++)
        {
            if (i < life) heart[i].SetActive(true);
            else heart[i].SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
