using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Luokka edustaa teht‰v‰n kohdetta.
/// Jokaisella teht‰v‰ll‰ on ID sek‰ aloitus- ja lopetustekstit
/// Ker‰tt‰v‰ll‰ tai tuhottavalla kohteella on myˆs lukum‰‰r‰
/// </summary>
public class QuestObject : MonoBehaviour
{
    // Teht‰v‰numero
    public int questNumber;

    // Aloitus- ja lopetustekstit
    public string[] lines;

    // Referenssi QuestManager-luokkaan
    public QuestManager questManager;

    // Lippu, joka kertoo onko kyseess‰ ker‰ysteht‰v‰
    public bool isItemQuest;

    // Ker‰tt‰v‰n esineen nimi
    public string targetItem;
    // Ker‰tt‰vien esineiden lukum‰‰r‰
    public int itemToCollect;
    // Laskuri, joka laskee ker‰tyt esineet
    public int itemCollectCount;

    // Teht‰v‰st‰ saatavat kokemuspisteet (EXP)
    [SerializeField]
    private int EXPammount;
    // Update is called once per frame
    void Update()
    {
        // Ker‰ysteht‰v‰?
        if (isItemQuest)
        {
            //Kyll‰ on, joten tarkistetaan QuestManagerilta onko se tietoinen ker‰yksen kohteesta
            if (questManager.itemCollected == targetItem)
            {
                // Kyll‰ on, joten kasvatetaan esinelaskuria
                questManager.itemCollected = null;
                // Kasvatetaan laskuri
                itemCollectCount++;
            }
            // Onko esineit‰ ker‰tty tarpeeksi?
            if (itemCollectCount >= itemToCollect)
            {
                // Kyll‰ on, joten teht‰v‰ p‰‰ttyy
                EndQuest();
            }
        }
    }

    /// <summary>
    /// Teht‰v‰n aloitustekstit
    /// </summary>
    public void StartQuest()
    {
        gameObject.SetActive(true);
        // Pyydet‰‰n QuestManageria n‰ytt‰m‰‰n aloitusteksti
        questManager.ShowQuestText(lines[0]);
    }

    ///<summary>
    /// Teht‰v‰n lopetustekstit, jonka j‰lkeen Questmanager merkkaa teht‰v‰n suoritetuksi
    /// </summary>
    public void EndQuest()
    {
        // Pyydet‰‰n QuestManageria n‰ytt‰m‰‰n lopetusteksti
        questManager.ShowQuestText(lines[1]);
        // Pyydet‰‰n QuestManageria merkkaamaan teht‰v‰ suoritetuksi
        questManager.questCompleted[questNumber] = true;
        // Pyydet‰‰n PlayerHealthManageria lis‰‰m‰‰n kokemuspisteit‰ (EXP)
        PlayerHealthManager.instance.AddPlayerEXP(EXPammount);
        // Deaktivoidaan teht‰v‰ kun se on tehty
        gameObject.SetActive(false);
    }
}   // QuestObject.cs p‰‰ttyy
