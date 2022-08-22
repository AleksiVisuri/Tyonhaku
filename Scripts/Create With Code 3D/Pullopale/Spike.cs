using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Spike : MonoBehaviour
{

    [SerializeField]
    private GameObject gameOverPanel;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {

            gameOverPanel.SetActive(true);

            SceneManager.LoadScene("MainMenu");

        }





    }














}
