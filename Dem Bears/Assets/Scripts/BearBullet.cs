using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearBullet : MonoBehaviour
{
    public GameObject bullet;
    public float fastness;
    public int lifetime;


    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {

        if (lifetime <= 0)
        {
            Destroy(bullet);
        }

        lifetime--;

        transform.position += transform.right * fastness;
    }
}
