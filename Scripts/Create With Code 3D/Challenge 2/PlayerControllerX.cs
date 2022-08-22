using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    public float delay = 1f;
    public float sendTime = 0;
    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > sendTime)
        {
            sendTime = Time.time + delay;
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
        }
    }
}
