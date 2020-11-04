using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public string HorzAxis = "Horizontal";
    public string VertAxis = "Vertical";
    public string FireAxis = "Fire1";
    public float speeder = 10f;
    private Rigidbody ThisBody = null;
    private enum facing {Up,Down,Left,Right};
    public enum weapon {Shotgun, Bomber, Stungun};
    private weapon armed;
    private facing direction;
    public GameObject still;
    public GameObject gun;
    public GameObject bullet1;
    public GameObject bullet2;
    public GameObject bullet3;
    private GameObject fire;
    public Text wName;
    public int cooldown = 0;
    public int Bigcooldown = 0;
    public bool Biginplay = false;

    // Start is called before the first frame update
    void Awake()
    {
        direction = facing.Right;
        armed = weapon.Shotgun;
        ThisBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && !Biginplay)
        {
            if(armed == weapon.Shotgun)
            {
                armed = weapon.Bomber;
                wName.text = "Weapon: Bomber";
            }
            else if(armed == weapon.Bomber)
            {
                armed = weapon.Stungun;
                wName.text = "Weapon: Stungun";
            }
            else if(armed == weapon.Stungun)
            {
                armed = weapon.Shotgun;
                wName.text = "Weapon: Shotgun";
            }
        }
        else if (Input.GetKeyDown(KeyCode.Z) && !Biginplay)
        {
            if (armed == weapon.Shotgun)
            {
                armed = weapon.Stungun;
                wName.text = "Weapon: Stungun";
            }
            else if (armed == weapon.Bomber)
            {
                armed = weapon.Shotgun;
                wName.text = "Weapon: Shotgun";
            }
            else if (armed == weapon.Stungun)
            {
                armed = weapon.Bomber;
                wName.text = "Weapon: Bomber";
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && cooldown <= 0)
        {
            if(armed == weapon.Shotgun)
            {
                fire = Instantiate<GameObject>(bullet1, gun.transform.position, gun.transform.rotation);
                cooldown = 10;
            }
            else if(armed == weapon.Stungun)
            {
                fire = Instantiate<GameObject>(bullet2, gun.transform.position, gun.transform.rotation);
                cooldown = 10;
            }
            else if (armed == weapon.Bomber)
            {
                if(!Biginplay)
                {
                    fire = Instantiate<GameObject>(bullet3, gun.transform.position, gun.transform.rotation);
                    cooldown = 10;
                    Bigcooldown = 30;
                    Biginplay = true;
                }
                else
                {
                    Biginplay = false;
                }               
            }

        }
        else if(cooldown > 0)
        {
            cooldown--;
        }
        
        if(Bigcooldown > 0)
        {
            Bigcooldown--;
        }
        else if(Biginplay == true)
        {
            Biginplay = false;
        }

        float Horz = Input.GetAxis(HorzAxis);
        float Vert = Input.GetAxis(VertAxis);

        transform.position = transform.position + new Vector3(Horz * speeder * Time.deltaTime, 0, Vert * speeder * Time.deltaTime);

        if(Vert > 0 && direction != facing.Up)
        {
            if(direction == facing.Down)
            {
                still.transform.eulerAngles = new Vector3(still.transform.eulerAngles.x, still.transform.eulerAngles.y - 180, still.transform.eulerAngles.z);
            }
            else
            {
                still.transform.eulerAngles = new Vector3(still.transform.eulerAngles.x, still.transform.eulerAngles.y - 90, still.transform.eulerAngles.z);

                if(direction == facing.Left)
                {
                    still.transform.eulerAngles = new Vector3(still.transform.eulerAngles.x - 180, still.transform.eulerAngles.y - 180, still.transform.eulerAngles.z);
                }
            }
            direction = facing.Up;

        }
        else if(Vert < 0 && direction != facing.Down)
        {
            if (direction == facing.Up)
            {
                still.transform.eulerAngles = new Vector3(still.transform.eulerAngles.x, still.transform.eulerAngles.y + 180, still.transform.eulerAngles.z);
            }
            else
            {
                still.transform.eulerAngles = new Vector3(still.transform.eulerAngles.x, still.transform.eulerAngles.y + 90, still.transform.eulerAngles.z);
                if (direction == facing.Left)
                {
                    still.transform.eulerAngles = new Vector3(still.transform.eulerAngles.x - 180, still.transform.eulerAngles.y - 180, still.transform.eulerAngles.z);
                }
            }
            direction = facing.Down;
        }

        if (Horz > 0 && direction != facing.Right)
        {
            if(direction == facing.Left)
            {
                still.transform.eulerAngles = new Vector3(still.transform.eulerAngles.x - 180, still.transform.eulerAngles.y - 180, still.transform.eulerAngles.z);
            }
            else if(direction == facing.Up)
            {
                still.transform.eulerAngles = new Vector3(still.transform.eulerAngles.x, still.transform.eulerAngles.y + 90, still.transform.eulerAngles.z);
            }
            else//Down
            {
                still.transform.eulerAngles = new Vector3(still.transform.eulerAngles.x, still.transform.eulerAngles.y - 90, still.transform.eulerAngles.z);
            }
            direction = facing.Right;
        }
        else if (Horz < 0 && direction != facing.Left)
        {
            still.transform.eulerAngles = new Vector3(still.transform.eulerAngles.x + 180, still.transform.eulerAngles.y + 180, still.transform.eulerAngles.z);

            if (direction == facing.Up)
            {
                still.transform.eulerAngles = new Vector3(still.transform.eulerAngles.x, still.transform.eulerAngles.y + 90, still.transform.eulerAngles.z);
            }
            else if(direction == facing.Down)
            {
                still.transform.eulerAngles = new Vector3(still.transform.eulerAngles.x, still.transform.eulerAngles.y - 90, still.transform.eulerAngles.z);
            }

            direction = facing.Left;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Goal")
        {
            GameManager.NextLevel();
        }
        if (other.gameObject.tag == "Bear")
        {
            GameManager.Reset(true);
        }
        if (other.gameObject.tag == "Gold")
        {
            Gold.GetGold();
            GameManager.points(100);
        }
    }
}  
