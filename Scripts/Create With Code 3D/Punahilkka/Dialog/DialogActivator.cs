using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour
{
    // Dialogin tekstit talletetaan taulukkoon
    public string[] lines;

    // Lippu, joka kertoo onko pelihahmo NPC-hahmon alueella
    private bool canActivate; //true = on alueella

    // Lippu, joka kertoo onko NPC-hahmolla nimi eli onko kyseess‰ hahmo vai esim. kyltti
    public bool isPerson = false; // false = NPC-hahmolla on nimi


    // Lippu joka kertoo onko dialogi aloitettu
    private bool dialogActivate;

    // Lippu, joka kertoo onko kyseess‰ QuestTrigger
    public bool isQuest;

    // Referenssi QuestManageriin
    private QuestManager questManager;

    // Teht‰v‰numero
    public int questNumber;

    private void Start()
    {
        // Luodaan yhteys QuestManageriin
        questManager = FindObjectOfType<QuestManager>();
    }

    // Update is called once per frame aukaisee dialogin tarvittaessa
    void Update()
    {
        // Onko pelihahmo dialogi alueella (canActivate=true) ja
        // Onko hiiren vasenta korvaa napautettu ja dialogia ei ole viel‰ aloitettu?
        if (canActivate && Input.GetButtonDown("Fire1") && !dialogActivate)
        {
            // Kyll‰ on, joten pyydet‰‰n DialogManageria n‰ytt‰m‰‰n dialogi-ikkunassa eka repliikki
            DialogManager.instance.ShowDialog(lines, isPerson);
            // Nostetaan lippu merkiksi, ett‰ dialogi k‰ynnistetty (IsTriggerExit-metodi)
            dialogActivate = true;
        }
    }
    // Metodi suoritetaan kun pelihahmo tulee tarpeeksi l‰helle NPC-kohdetta (esim. susi)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Onko pelihahmo alueella?
        if (collision.CompareTag("Player"))
        {
            // On, joten nostetaan merkkilippu.
            canActivate = true; // Update-funktiota varten
        }
    }

    // Metodi suoritetaan kun pelihahmo tulee alueelta pois.
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Tuliko pelihahmo alueelta ulos?
        if (collision.CompareTag("Player"))
        {
            // Kyll‰ tuli, joten lasketaan merkkilippu.
            canActivate = false; // Update-funktiota varten

            // Oliko kyseess‰ teht‰v‰?
            if (isQuest && dialogActivate)
            {
                // Aloittaa teht‰v‰n ja n‰ytt‰‰ aloitustekstin
                StartQuest();
            }
            // Onko dialogi varmasti aloitettu?
            if (dialogActivate)
            {
                // On aloitettu, joten kohde katoaa
                gameObject.SetActive(false);
            }
        }
    }
    void StartQuest()
    {
        // Pyyt‰‰ QUestManageria aktivoimaan teht‰v‰n ja n‰ytt‰m‰‰n aloitusteksti
        questManager.quests[questNumber].gameObject.SetActive(true);
        questManager.quests[questNumber].StartQuest();

        // Poistaa DialogActivatorin
        gameObject.SetActive(false);
    }
}
