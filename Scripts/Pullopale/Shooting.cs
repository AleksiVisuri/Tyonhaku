using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour

{
    public GameObject Bullet;

    public KeyCode Attack;

    public int EnergyAmmount;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Shoot();
    }
    void Shoot()
    {


        if (Input.GetKey(Attack) && 
            EnergyManager.instance.GetCurrentEnergy() != 0 )
        {
            
            Instantiate(Bullet, transform.position +
            new Vector3(0, 0, 0), transform.rotation);

            EnergyManager.instance.HurtPlayerEnergy(EnergyAmmount);          
        
        }
    }
}
