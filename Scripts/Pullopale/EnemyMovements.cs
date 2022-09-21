using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovements : MonoBehaviour
{

    [SerializeField]
    private int MoveSpeed;

    private bool Suunta;

    public GameObject Roska;
    
    void Start()
    {

        Suunta = true;

    }

    void Update()
    {

        if (Suunta == true)
        {

            transform.position -= transform.right * Time.deltaTime * MoveSpeed;

        }

        if (Suunta == false)
        {

            transform.position += transform.right * Time.deltaTime * MoveSpeed;

        }



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        

        if (collision.CompareTag("Right"))
        {

            Suunta = false;
     
        }

        if (collision.CompareTag("Left"))
        {

            Suunta = true;

        }




    }
    private void OnDestroy()
    {
        Instantiate(Roska, transform.position, transform.rotation);
    }

    //private void Voidi()
    //{

    //while (Suunta == true) 
    //{

    //transform.position += transform.right * Time.deltaTime * MoveSpeed;

    //}




    //}    












}