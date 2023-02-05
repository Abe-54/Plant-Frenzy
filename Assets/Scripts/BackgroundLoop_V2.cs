using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop_V2 : MonoBehaviour
{
    public float height;
    public float startPos;
    public GameObject cam;

    public float parrallaxEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("CM vcam1");
        startPos = transform.position.y;
        height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = (cam.transform.position.y);
        float distance = (cam.transform.position.y);
        transform.position = new Vector3(transform.position.x, startPos + distance, transform.position.z);

        if (temp > startPos + height)
        {
            startPos += height;
        }else if (temp < startPos - height)
        {
            startPos -= height;
        }
    }
}
