using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;   // Canvasta varten

/// <summary>
/// Luokka pit‰‰ kirjaa vihollisen (susi) tilatiedoista, kuten terveys
/// Liitet‰‰n luokka viholliseen
/// </summary>
public class EnemyHealthManager : MonoBehaviour
{
    [SerializeField]
    private string enemyName;           // Vihollisen nimi
    [SerializeField]
    private float enemyMaxHP = 100f;    // Maksimiterveys
    [SerializeField]
    private float enemyCurrentHP;       // Nykyinen tevreys

    // Slideri
    public Image enemyHealthbar;    // Terveyspalkki
    public float lerpSpeed;         // Palkin nopeuden s‰‰din

    // Start is called before the first frame update
    void Start()
    {
        // Asetetaan pelin alussa nykyiset terveyspisteet maksimiin
        enemyCurrentHP = enemyMaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        // J-painike v‰hent‰‰ terveytt‰
        if (Input.GetKeyDown(KeyCode.J))
        {
            HurtEnemy(5);
        }

        // Tilapalkin p‰ivitys
        CheckEnemyStatus();

    }

    /// <summary>
    /// Tarkistaa pelihahmon tilan ja toimii sen mukaisesti
    /// </summary>
    private void CheckEnemyStatus()
    {
        // Onko HP muuttunut?
        if (enemyCurrentHP != enemyHealthbar.fillAmount)
        {
            // Kyll‰ on, joten p‰ivitet‰‰n tilapalkki (terveys)
            enemyHealthbar.fillAmount = Mathf.Lerp(enemyHealthbar.fillAmount,
                enemyCurrentHP / enemyMaxHP, Time.deltaTime * lerpSpeed);
        }
    }

    /// <summary>
    /// Jokin aiheuttaa viholliselle vahinkoa
    /// </summary>
    /// <param name="damageToTake"></param>
    public void HurtEnemy(int damageToTake)
    {
        // V‰hennet‰‰n terveytt‰
        enemyCurrentHP -= damageToTake;

        // Kuoliko vihollinen?
        if (enemyCurrentHP <= 0)
        {
            // Kyll‰ kuoli, joten aseta nykyinen terveys nollaan
            // Ja suorita vihollisen kuolinmetodi
            enemyCurrentHP = 0;
            Die();
        }

    }

    /// <summary>
    /// Vihollisen kuolinmetodi
    /// </summary>
    private void Die()
    {
        // Tulosta konsoliin ett‰ vihollinen jonka nimi on enemyName kuoli!
        print(enemyName + " kuoli");

        // Odotetaan 0,5 sekunttia ja tuhotaan vihollinen
        Destroy(gameObject, 0.5f);
    }


}   // EnemyHealthManager.cs p‰‰ttyy
