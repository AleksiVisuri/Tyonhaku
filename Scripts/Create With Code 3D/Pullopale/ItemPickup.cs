using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{

    [SerializeField]
    private int EnergyAmmount;

    private int CurrentEnergy2;

    // Start is called before the first frame update
    void Awake()
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

            if (gameObject.CompareTag("Energy"))
            {
                if (EnergyManager.instance.CurrentEnergy < 1)
                {


                    EnergyManager.instance.AddPlayerEnergy(EnergyAmmount);

                    Destroy(gameObject);

                }
            }
            
            
            
        }



    }






}
