using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Luokka liitet‰‰n QuestManager-GameObjektiin.
/// Luokka tiet‰‰ jokaisen suoritettavan teht‰v‰n sek‰ k‰ynnist‰‰ teht‰v‰‰n liittyv‰n dialogin.
/// Teht‰v‰tyyppej‰ ovat: ker‰ys- tuhoa- ja etsityypit
/// </summary>
public class QuestManager : MonoBehaviour
{
    /// <summary>
    /// Teht‰v‰taulukko. Taulukon alkioina on teht‰v‰olio (QuestObject)
    /// </summary>
    public QuestObject[] quests;

    /// <summary>
    /// Kun teht‰v‰ on suoritettu se merkit‰‰n questCompleted-taulukkoon (true = teht‰v‰ suoritettu)
    /// </summary>
    public bool[] questCompleted;

    /// <summary>
    ///  Ker‰tt‰v‰n esineen nimi
    /// </summary>
    public string itemCollected;

    // Start is called before the first frame update
    void Start()
    {
        // Pelin alissa varataan questCompleted-taulukkoon jokaista teht‰v‰‰ varten oma paikka
        questCompleted = new bool[questCompleted.Length];
    }

    /// <summary>
    /// Metodi pyyt‰‰ DialogManageria n‰ytt‰m‰‰n dialogin, jossa on teht‰v‰n kuvaus
    /// </summary>
    /// <param name="questTask"></param>
    public void ShowQuestText(string questTask)
    {
        // Teht‰v‰n kuvaus (questTask) talletetaan taulukkoon (oneLine)
        string[] oneLine = new string[1];
        oneLine[0] = questTask;

        // Pyydet‰‰n sitten DialogManageria n‰ytt‰m‰‰n teht‰v‰n kuvaus
        DialogManager.instance.ShowDialog(oneLine, false);      // false = dialogissa ei ole otsikkoa
    }

}   // QuestManager-p‰‰ttyy
