using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCamera : MonoBehaviour
{
    [SerializeField] private GameObject virtualCamera1;
    [SerializeField] private GameObject virtualCamera2;

    // Start is called before the first frame update
    void Start()
    {
        // virtuaalikamera 1 aktiivinen
        virtualCamera1.SetActive(true);
        // virtuaalikamera 2 pois käytöstä
        virtualCamera2.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Törmäsikö kurpitsapoika oveen 1
        if (collision.CompareTag("Player"))
        {
            // Kyllä, joten virtuaalikamera 2 aktivoituu
            virtualCamera1.SetActive(false);
            virtualCamera2.SetActive(true);

            // Taustamusa vaihtuu
            AudioManager.instance.StopPlay("Background");
            AudioManager.instance.Play("Background2");
        }
    }
}
