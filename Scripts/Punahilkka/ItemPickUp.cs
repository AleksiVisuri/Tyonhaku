using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Luokka edustaa ker‰tt‰v‰‰ esinett‰, joka vaikuttaa pelihahmon tilatietoihin (mana, hp, jne)
/// Luokka k‰ytt‰‰ PlayerHealthManager luokan metodeja
/// </summary>
public class ItemPickUp : MonoBehaviour
{
    [SerializeField]
    private int healthAmmount;      // terveyspisteet (esim. 0, 3, 5, 7...)
    [SerializeField]
    private int manaAmmount;        // manapisteet (esim. 0, 3, 7, jne)
    [SerializeField]
    private int damageToGive;       // vahinkopisteet

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Tˆrm‰sikˆ pelihahmo t‰h‰n objektiin (esim. kakku, powerups, jne.)
        if (collision.CompareTag("Player"))
        {
            PlayerHealthManager.instance.AddPlayerEXP(20);
            // Vaikuttaako ker‰tt‰v‰ objekti terveyspisteisiin (osittain)?
            if (gameObject.CompareTag("HP"))
            {
                // Kyll‰ vaikuttaa, joten pyydet‰‰n PlayerHealthManager:ia lis‰‰m‰‰n pelaajaan terveytt‰
                PlayerHealthManager.instance.AddPlayerHealth(healthAmmount);
            }

            // Vaikuttaako ker‰tt‰v‰ objekti manapisteisiin (osittain)?
            if (gameObject.CompareTag("MP"))
            {
                // Kyll‰ vaikuttaa, joten pyydet‰‰n PlayerHealthManager:ia lis‰‰m‰‰n manaa
                PlayerHealthManager.instance.AddPlayerMana(manaAmmount);
            }

            // Vaikuttaako ker‰tt‰v‰ objekti terveyspisteisiin (t‰ysi)?
            if (gameObject.CompareTag("fullHP"))
            {
                // Kyll‰ vaikuttaa, joten pyydet‰‰n PlayerHealthManager:ia lis‰‰m‰‰n t‰ydet HP-pisteet
                PlayerHealthManager.instance.SetMaxHP();    // Luodaan SetMaxHP-metodi
            }

            // Vaikuttaako ker‰tt‰v‰ objekti manapisteisiin (t‰ysi)?
            if (gameObject.CompareTag("fullMana"))
            {
                // Kyll‰ vaikuttaa, joten pyydet‰‰n PlayerHealthManager:ia lis‰‰m‰‰n t‰ydet manat
                PlayerHealthManager.instance.SetMaxMP();    // Luodaan SetMaxMP-metodi
            }

            // Vaikuttaako ker‰tt‰v‰ objekti terveyspisteisiin negatiivisesti
            if (gameObject.CompareTag("damageHP"))
            {
                // Kyll‰ vaikuttaa, joten pyydet‰‰n PlayerHealthManager:ia v‰hent‰m‰‰n pelaajan terveytt‰
                PlayerHealthManager.instance.HurtPlayer(damageToGive);
            }

            // Vaikuttaako ker‰tt‰v‰ objekti manapisteisiin negatiivisesti
            if (gameObject.CompareTag("damageMP"))
            {
                // Kyll‰ vaikuttaa, joten pyydet‰‰n PlayerHealthManager:ia v‰hent‰m‰‰n pelaajan manaa
                PlayerHealthManager.instance.HurtPlayerMana(damageToGive);
            }
        }

        // Poista esine, kun se on ker‰tty
        Destroy(gameObject);
    }

} // ItemPickUp.cs luokka p‰‰ttyy
