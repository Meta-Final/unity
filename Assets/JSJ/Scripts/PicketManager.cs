using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicketManager : MonoBehaviour
{
    public GameObject picketPrefab;
    public GameObject player;

    Vector3 picketPos;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        
    }

    public void MakePicket()
    {
        picketPos = player.transform.position + player.transform.forward * 3f;

        GameObject obj = Instantiate(picketPrefab, picketPos, Quaternion.identity);
        obj.transform.LookAt(player.transform);
    }
}
