using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{
    public GameObject bullet;
    public float fastness;
    public int lifetime;
    public bool bigBullet;

    public GameObject gun;
    public GameObject smallBullet;
    private GameObject fire;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bear")
        {
            Destroy(bullet);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && bigBullet)
        {
            fire = Instantiate<GameObject>(smallBullet, gun.transform.position, gun.transform.rotation);
            gun.transform.eulerAngles = new Vector3(gun.transform.eulerAngles.x, gun.transform.eulerAngles.y - 45, gun.transform.eulerAngles.z);
            fire = Instantiate<GameObject>(smallBullet, gun.transform.position, gun.transform.rotation);
            gun.transform.eulerAngles = new Vector3(gun.transform.eulerAngles.x, gun.transform.eulerAngles.y + 90, gun.transform.eulerAngles.z);
            fire = Instantiate<GameObject>(smallBullet, gun.transform.position, gun.transform.rotation);
            Destroy(bullet);
        }

        if (lifetime <= 0)
        {
            Destroy(bullet);
        }

        lifetime--;

        transform.position += transform.right * fastness;
    }


}
