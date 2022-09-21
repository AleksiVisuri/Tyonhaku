using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

/// <summary>
/// Tämän luokan tehtäviä on
/// *** näyttää puzzleen liittyvä kysymys
/// *** käsitellä kysymyksen vastaus
/// *** Estää pelihamhon liikkuminen puzzlen aikana
/// </summary>
public class PuzzleController : MonoBehaviour
{
    // Tehdään luokasta staattinen, jotta sitä voidaan käyttää muista koodeista
    public static PuzzleController instance;

    // Pulman käynnistävät kytkimet talletetaan taulukkoon
    [SerializeField] private Puzzle[] puzzles;

    // Canvas - Matemaattinen tehtävä
    [SerializeField] private GameObject puzzlePanel; // Paneli
    [SerializeField] private Text questionText;      // Matemaattinen tehtävä
    [SerializeField] private Button answer1Button;   // Vastauspainike 1
    [SerializeField] private Button answer2Button;   // VAstauspainike 2
    [SerializeField] private Button answer3Button;   // Vastauspainike 3
    [SerializeField] private Text answer1Text;       // Painikkee 1 teksti
    [SerializeField] private Text answer2Text;       // Painikkee 2 teksti
    [SerializeField] private Text answer3Text;       // PAinikkee 3 teksti
    // Start is called before the first frame update
    void Start()
    {
        // Otetaan staattinen luokka käyttöön
        instance = this;
    }

    /// <summary>
    /// Käynnistää pulman käsittelyn.
    /// </summary>
    /// <param name="_puzzleID"></param>
    public void HandlePuzzle(int _puzzleID)
    {
        // Estetään pelihahmon liikkuminen
        PlayerController.instance.MyCanMove = false;

        // SIirrytään Idle-animaatioon
        PlayerController.instance.MyAnim.SetFloat("Speed", 0);
        PlayerController.instance.MyAnim.SetBool("Grounded", true);

        // NÄytä matemaattinen tehtävä
        puzzlePanel.SetActive(true);
        switch (_puzzleID)
        {
            case 1:
                // Pelihahmo on osunut kytkimeen 1, joten käynnistetään puzzle 1.
                //puzzles[1].HandleAnimations(1);
                ShowProblem1();
                break;
            case 2:
                // Pelihahmo on osunut kytkimeen 2, joten käynnistetään puzzle 2.
                //puzzles[2].HandleAnimations(2);
                ShowProblem2();
                break;
            case 3:
                ShowProblem3();
                break;
            case 4:
                ShowProblem4();
                break;
            default:
                // Ei tehdä mitään
                break;
        }
    }
    /// <summary>
    /// Näytetään 1. laskutoimitus: 4 - 0 = _
    /// </summary>
    
    public void ShowProblem1()
    {
        questionText.text = "4 - 0 = _";    // Matemaattinen tehtävä

        answer1Button.name = "1";           // Oikea painike
        answer1Text.text = "4";             // Oikea vastaus

        answer2Button.name = "Wrong";       // Väärä painike
        answer2Text.text = "3";

        answer3Button.name = "Wrong";        // Väärä painike
        answer3Text.text = "8";
    }
    public void ShowProblem2()
    {
        questionText.text = "8 * 4 = _";    // Matemaattinen tehtävä

        answer1Button.name = "2";           // Oikea painike
        answer1Text.text = "32";             // Oikea vastaus

        answer2Button.name = "Wrong";       // Väärä painike
        answer2Text.text = "24";

        answer3Button.name = "Wrong";        // Väärä painike
        answer3Text.text = "40";
    }
    public void ShowProblem3()
    {
        questionText.text = "40 - 32 = _";    // Matemaattinen tehtävä

        answer1Button.name = "Wrong";           // Väärä painike
        answer1Text.text = "4";             

        answer2Button.name = "Wrong";       // Väärä painike
        answer2Text.text = "3";

        answer3Button.name = "3";         // Oikea painike
        answer3Text.text = "8";             // Oikea vastaus
    }
    public void ShowProblem4()
    {
        questionText.text = "3 * 3 - 3 * 2 = _";    // Matemaattinen tehtävä

        answer1Button.name = "Wrong";           // Väärä painike
        answer1Text.text = "4";             

        answer2Button.name = "4";        // Oikea painike
        answer2Text.text = "3";         // Oikea vastaus

        answer3Button.name = "Wrong";        // Väärä painike
        answer3Text.text = "12";
    }
        /// <summary>
        /// Pelaaja painaa vastauspainiketta.
        /// Oikea vastaus painike tunnistetaan painikkeen nimen perusteella.
        /// Painikkeet on nimetty 1, 2, 3, jne
        /// </summary>
        /// <param name="button"></param>
        public void HandleCorrectAnswer(Button button)
    {
        switch (button.name)
        {
            case "1":
                // OIkea vastaus tehtävässä 1
                StartCoroutine(CheckAnswerCO(1));
                break;
            case "2":
                // Oikea vastaus tehtävässä 2
                StartCoroutine(CheckAnswerCO(2));
                break;
            case "3":
                // Oikea vastaus tehtävässä 3
                StartCoroutine(CheckAnswerCO(3));
                break;
            case "4":
                // Oikea vastaus tehtävässä 4
                StartCoroutine(CheckAnswerCO(4));
                break;
            default:
                // Väärä vastaus tehtävään
                StartCoroutine(CheckAnswerCO(0));
                break;
        }
    }
    /// <summary>
    /// Alairutiini näyttää 2 sekunnin ajan onko vastaus oikein vai väärin.
    /// Vastaus on väärin jos _puzzleID = 0
    /// </summary>
    /// <param name="_puzzleID"></param>
    /// <returns></returns>
    private IEnumerator CheckAnswerCO(int _puzzleID)
    {
        answer1Button.enabled = false;
        answer2Button.enabled = false;
        answer3Button.enabled = false;

        if (_puzzleID == 0)
        {
            string text = questionText.text;
            questionText.text = "VÄÄRIN!";
            yield return new WaitForSeconds(2f);
            questionText.text = text;
            answer1Button.enabled = true;
            answer2Button.enabled = true;
            answer3Button.enabled = true;
        }
        else
        {
            questionText.text = "OIKEIN!";
            //Käännetään kytkin 1 auki
            puzzles[_puzzleID].HandleAnimations(_puzzleID);
            // Odotetaan 2 sekunttia
            yield return new WaitForSeconds(2f);
            // Piilotetaan paneli
            puzzlePanel.SetActive(false);
            // Pelihahmo voi liikkua
            PlayerController.instance.MyCanMove = true;

            answer1Button.enabled = true;
            answer2Button.enabled = true;
            answer3Button.enabled = true;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
