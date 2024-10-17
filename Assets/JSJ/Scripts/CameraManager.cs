using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public float followSpeed = 5f;

    Vector3 offset;

    void Start()
    {
        playerPrefab = GameObject.FindWithTag("Player");

        if (playerPrefab != null)
        {
            offset = transform.position - playerPrefab.transform.position;
        }
    }

    void Update()
    {
        CameraMoving();
    }

    public void CameraMoving()
    {
        Vector3 tartgetPos = playerPrefab.transform.position + offset;

        transform.position = Vector3.Lerp(transform.position, tartgetPos, followSpeed * Time.deltaTime);
    }
}
