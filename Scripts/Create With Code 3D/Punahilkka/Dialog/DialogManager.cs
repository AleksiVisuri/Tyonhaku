using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

/// <summary>
/// N‰ytt‰‰‰ ja sulkee dialogin.
/// Luokka liitet‰‰n Canvakseen.
/// Luokka keskustelee GameManagerin kanssa.
/// </summary>
public class DialogManager : MonoBehaviour
{


    // UI-referenssit
    public Text dialogText;         // Dialogin teksti
    public Text nameText;           // Keskustelijan nimi
    public GameObject dialogBox;    // Dialogin rakenne
    public GameObject nameBox;      // Dialogin alla olevan nimialue (Name Box/Text)

    // Dialogi-referenssit
    private string[] dialogLines;       //Dialogin rivien lukum‰‰r‰
    private int currentLine = 0;        // Rivi, jolla ollaan
    private bool justStarted;           // Merkkilippu, joka kertoo voidaanko keskustelua jatkaa
    public float typingSpeed;           // Automaattikirjoituksen nopeus
    private bool isCoroutingRunning;    // Onko alirutiini k‰ynniss‰

    // Dialogi-instanssi
    public static DialogManager instance;

    // Start is called before the first frame update
    void Start()
    {
        // Onko DialogManager jo olemassa?
        if (instance == null)
        {
            // Ei ole, jtoen luodaan DialogManager-esiintym‰
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /* Painettiinko hiiren oikeaa korvaa ja tarkistetaan
         ett‰ alirutiini ei ole k‰ynniss‰ (isCoroutingRunning=false)
         ja ett‰ keskustelua voidaan jatkaa (justStarted=true)
        */ 
        if (Input.GetButtonUp("Fire2") && !isCoroutingRunning && justStarted)
        {
            // Siirryt‰‰n seuraavalle riville
            currentLine++;
            // Onko dialogi jo p‰‰ttynyt?
            if (currentLine >= dialogLines.Length)
            {
                // Dialogi on p‰‰ttynyt, joten suljetaan dialog-ikkuna
                dialogBox.SetActive(false);
                /* Lis‰ksi informoidaan GameManageria ett‰ dialogi
                p‰‰ttyi (pelihahmo voi taas liikku) */
                GameManager.instance.dialogActive = false;
            }
            else
            {
                /* Dialogi ei ole viel‰ p‰‰ttynyt, joten ekaksi selviet‰‰n
                keskustelijan nimi jos sellainen on */
                CheckIfName();
                /* N‰ytet‰‰n dialogiteksti
                 dialogText.text = dialogLines[currentLine]; */
                StartCoroutine(AutoType(dialogLines, currentLine));
            }
        }
    }
    // T‰m‰ metodi aukaisee dialogi-ikkunan ja n‰ytt‰‰ 1. dialogin
    public void ShowDialog(string[] newLines, bool isPerson)
    {
        // Montako tekstirivi‰ dialogissa on
        dialogLines = newLines;

        // Aloitetaan 1. tekstist‰
        currentLine = 0;

        // Tarkistetaan dialogiin osallistuvan nimi, jos se on hahmo
        CheckIfName();

        // N‰ytet‰‰n dialogi-ikkuna
        dialogBox.SetActive(true);

        /* N‰ytet‰‰n dialogin 1. rivi (0. rivi k‰yty, jos oli henkilˆ).
        dialogText.text = dialogLines[currentLine]; */
        StartCoroutine(AutoType(dialogLines, currentLine));     //Automaattikirjoitus

        // Ilmoitetaan Update-funktiolle ett‰ dialogi-ikkuna on aukaistu
        justStarted = true;

        // Aktivoidaan tai deaktivoidaan nimilaatikko
        nameBox.SetActive(isPerson);

        // Informoidaan GameManageria ett‰ dialogi on k‰ynniss‰
        GameManager.instance.dialogActive = true;
    }
    // T‰m‰ metodi tarkistaa onko merkkijonossa n- ja poistaa sen
    private void CheckIfName()
    {
        if (dialogLines[currentLine].StartsWith("n-"))
        {
            nameText.text = dialogLines[currentLine].Replace("n-", "");

            // Hyp‰t‰‰n sueraavalle riville
            currentLine++;
        }
    }
    // Sulkee dialogi-ikkunan.
    public void StopDialog()
    {
        // Suljetaan dialogi-ikkuna
        dialogBox.SetActive(false);

        // Informoidaan GameManageria ett‰ dialogi on p‰‰ttynyt.
        GameManager.instance.dialogActive = false;
    }
    IEnumerator AutoType(string[] newLInes, int _currentLine)
    {
        // Tyhjennet‰‰n dialogi
        dialogText.text = "";

        // Kerrotaan Update-metodille, ett‰ automaattikirjoitus on k‰ynniss‰
        isCoroutingRunning = true;

        // Keskustelua ei voi jatkaa
        justStarted = false;

        // K‰yd‰‰n dialogin rivi l‰pi kirjain kerrallaan
        foreach (char letter in newLInes[_currentLine].ToCharArray())
        {
            // Lis‰t‰‰n seuraava kirjain
            dialogText.text += letter;

            // Odotetaan pieni hetki
            yield return new WaitForSeconds(typingSpeed);
        }
        // Kerrotaan Update-metodille, ett‰ automaattikirjoitus on p‰‰ttynyt
        isCoroutingRunning = false;
        // Keskustelua voidaan jatkaa
        justStarted = true;
    }
}
