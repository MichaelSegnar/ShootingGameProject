using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    public GameObject gold;
    static private Gold G;

    void Start()
    {
        G = this;
    }

    public static void GetGold()
    {
        Destroy(G.gold);
    }
}
