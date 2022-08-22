using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Luokka liitet‰‰n ker‰tt‰v‰‰n esineeseen
/// </summary>
public class QuestItem : MonoBehaviour
{
    // Teht‰v‰numero
    public int questNumber;

    // Referenssi QuestManageriin
    private QuestManager questManager;

    // Esineen nimi
    public string itemName;

    // Start is called before the first frame update
    void Start()
    {
        // Luodaan yhteys QuestManageriin, jotta voidaan k‰ytt‰‰ sen metodeja
        questManager = FindObjectOfType<QuestManager>();
    }

    /// <summary>
    /// Metodi pyyt‰‰ QuestManageria merkkaan ker‰ysteht‰v‰n suoritetuksi.
    /// Metodi suoritetaan kun pelihahmo osuu esineeseen.
    /// Metodiin voidaan koodata muita esineeseen liittyvi‰ toimintoja, kuten ansaitut EXP-pisteet
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Osuiko pelihahmo esineeseen?
        if (collision.CompareTag("Player"))
        {
            // Kyll‰, joten tarkistetaan ettei teht‰v‰‰ ole viel‰ tehty?
            if (!questManager.questCompleted[questNumber] &&
                questManager.quests[questNumber].gameObject.activeSelf)
            {
                // Ei ole, joten kerrotaan Questmanagerille esineen nimi
                questManager.itemCollected = itemName;

                // Deaktivoidaan esine
                gameObject.SetActive(false);

                // Ansaitut kullat tai EXP-pisteet koodataan t‰h‰n

                // Tehd‰‰nkˆ esineelle jotain? Ehk‰ aiirto inventoriin, tai otetaan se k‰yttˆˆn, vai mit‰??

                // Myˆs ‰‰niefekti koodataan t‰h‰n
            }
        }
    }
}   // QuestItem.cs p‰‰ttyy
