using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sidescroller : MonoBehaviour
{
    public GameObject player;
    private Vector3 move = new Vector3(0, 400, 0);
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + move;
    }
}
