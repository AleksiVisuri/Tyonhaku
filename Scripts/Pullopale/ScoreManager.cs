using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    private int TrashAmmount;

    public static ScoreManager instance;

    [SerializeField]
    private Text trashCounterText;
    public Collider2D winCollider;

    [SerializeField]
    private GameObject winPanel;

    private bool debounce;
    private bool playerFreeze;
    private Vector2 pPos;
    private void Awake()
    {

        instance = this;

        TrashAmmount = 0;
    }


    private void Update()
    {

        trashCounterText.text = " Kerätyt roskat: " + TrashAmmount.ToString() + " / 4";
        if (playerFreeze)
        {
            transform.position = pPos;
            if (Input.GetKey(KeyCode.Space))
            {
                playerFreeze = false;
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    public void AddTrash()
    {

        TrashAmmount++;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision == winCollider)
        {
            if (!debounce)
            {
                debounce = true;
                winPanel.SetActive(true);
                pPos = transform.position;
                playerFreeze = true;

            }


        }
    }



}
