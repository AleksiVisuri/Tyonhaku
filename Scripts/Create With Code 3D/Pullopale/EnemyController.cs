using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
     [SerializeField]
     private Rigidbody2D PlayerRB2D;

     public GameObject GameObject;

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
        
        if (collision.CompareTag("Ammus"))
        {
            
            Destroy(GameObject);
        
        }
        
        if (collision.CompareTag("Player") && (PlayerRB2D.velocity.y <= -0.1))
        {


            Destroy(GameObject);



        }

    }


}
