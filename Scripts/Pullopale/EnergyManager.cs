using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnergyManager : MonoBehaviour
{

    public static EnergyManager instance;

    public int CurrentEnergy;

    
    public GameObject EnergyIcon;


    // Start is called before the first frame update
    void Start()
    {

        instance = this;

       
    }

    // Update is called once per frame
    void Update()
    {

        CheckPlayerStatus();

    }

    private void CheckPlayerStatus()
    {

        
        if(CurrentEnergy > 0)
        {
            

            EnergyIcon.SetActive(true);



        }

        else
        {

            EnergyIcon.SetActive(false);

        }
    }

    public void AddPlayerEnergy(int EnergyAmmount)
    {

        CurrentEnergy += EnergyAmmount;



    }

    public void HurtPlayerEnergy(int EnergyAmmount)
    {

        CurrentEnergy -= EnergyAmmount;



    }

    public int GetCurrentEnergy()
    {

        return CurrentEnergy;

    }



}
