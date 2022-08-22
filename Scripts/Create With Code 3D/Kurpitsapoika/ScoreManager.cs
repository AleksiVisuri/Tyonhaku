using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Reseptipalojen lukumäärä
    private int resepieAmmount;

    // Canvaksessa oleva tekstilaatikko, joka näyttää kerätyt respetipalat
    [SerializeField]
    private Text resepieCounterText;

    // Oven suojacollideri
    public GameObject wallCollider;
    // Maalin suojacollideri
    public GameObject wallCollider2;

    private void Awake()
    {
        // Nollataan reseptipala laskuri pelin alussa
        resepieAmmount = 0;
    }

    private void Update()
    {
        // Tulostetaan kerättyjen reseptipalojen lukumäärä konsoliin
        resepieCounterText.text = resepieAmmount.ToString() + " / 8";

        // Tutkitaan onko reseptinpaloja kerätty tarpeeksi
        if (resepieAmmount == 4)
        {
            // Poistetaan oven suojacollider
            wallCollider.SetActive(false);
        }
        else if (resepieAmmount == 8)
        {
            // Poistetaan oven suojacollider
            wallCollider2.SetActive(false);
        }
    }

    /// <summary>
    /// Netodi kasvattaa reseptipala laskuria yhdellä
    /// </summary>
    
    public void AddResepie()
    {
        resepieAmmount++;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

}
