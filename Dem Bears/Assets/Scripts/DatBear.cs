using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DatBear : MonoBehaviour
{
    public GameObject DisBear;
    public GameObject BlackBall;
    public GameObject meat;
    private GameObject fire;
    public enum color {Brown,Black,White};
    public color type;
    public float speed;
    public int shiftIn;
    private int shiftPoint;
    private bool turn = false;
    public int HP;
    private int daze;
    public float targetRange;

    public enum state {Normal,Damaged,Frozen};
    public state heIs;

    public Sprite norm;
    public Sprite stun;
    public Sprite dmg;

    // Start is called before the first frame update
    void Start()
    {
        heIs = state.Normal;
        shiftPoint = shiftIn;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(heIs == state.Normal||heIs == state.Damaged)
        {
            if(type == color.Brown)
            {
                transform.position += transform.right * speed;
                shiftIn--;

                if (shiftIn <= 0)
                {
                    if (turn) { DisBear.transform.eulerAngles = new Vector3(DisBear.transform.eulerAngles.x, DisBear.transform.eulerAngles.y - 180, DisBear.transform.eulerAngles.z); turn = false; }
                    else { DisBear.transform.eulerAngles = new Vector3(DisBear.transform.eulerAngles.x, DisBear.transform.eulerAngles.y + 180, DisBear.transform.eulerAngles.z); turn = true; }

                    shiftIn = shiftPoint;
                }
            }
            else if(type == color.Black)
            {
                shiftIn--;

                if (shiftIn <= 0)
                {
                    fire = Instantiate<GameObject>(BlackBall, DisBear.transform.position, DisBear.transform.rotation);

                    shiftIn = shiftPoint;
                }
            }
            else
            {
                if(Vector3.Distance(DisBear.transform.position,meat.transform.position) < targetRange)
                {
                    transform.position = transform.position + new Vector3((meat.transform.position.x - DisBear.transform.position.x)/speed,(meat.transform.position.y - DisBear.transform.position.y) /speed,(meat.transform.position.z - DisBear.transform.position.z) /speed);
                }
            }

            if(daze > 0)
            {
                daze--;
                if(daze <=0)
                {
                    heIs = state.Normal; DisBear.gameObject.GetComponent<SpriteRenderer>().sprite = norm;
                }
            }
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            Debug.Log("Hit");
            heIs = state.Damaged;
            HP--;
            if (HP <= 0)
            {
                Destroy(DisBear);
                switch (type)
                {
                    case (color.Brown):
                        GameManager.points(10);
                        break;
                    case (color.Black):
                        GameManager.points(30);
                        break;
                    case (color.White):
                        GameManager.points(50);
                        break;
                }
            }
            DisBear.gameObject.GetComponent<SpriteRenderer>().sprite = dmg;
            daze = 25;
        }
        else if(other.gameObject.tag == "Stun")
        {
            Debug.Log("Stunned");
            heIs = state.Frozen;
            DisBear.gameObject.GetComponent<SpriteRenderer>().sprite = stun;
        }
    }
}
