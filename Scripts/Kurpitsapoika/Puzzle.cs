using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Unityn tapahtumien hallinta
using UnityEngine.Events;

/// <summary>
/// Puzzle luokan tethäviä on
/// *** deaktivoida kytkin ja käynnistää pulma
/// *** suorittaa kytkin animaatio sekä portin aukaisu animaatio
/// </summary>
public class Puzzle : MonoBehaviour
{
    // Tapahtuma, joka käynnistyy kun törmätään porttiin
    public UnityEvent OnPuzzle;

    // Animaattorit
    private Animator switchAnim;    // Kytkimen animaattori
    [SerializeField]
    private Animator gateAnim;      // Portin animaatio
    // Start is called before the first frame update
    void Start()
    {
        // Kytkimen animaattori otetaan käyttöön
        switchAnim = GetComponent<Animator>();
    }
    /// <summary>
    /// Pulmaan liitetty triggeri, joka käynnistyy pelaajan toimesta
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Deaktivoi kytkimen collaiderin
            GetComponent<BoxCollider2D>().enabled = false;
            // Tapahtuma (pulman kysymys) on käynnistetty. Pulmakontrolleri voi nyt käsitellä pulman.
            OnPuzzle?.Invoke();
        }
    }
    /// <summary>
    /// Suorittaa tiettyyn pulmaan liittyvät animaatio. Pulmat on numeroitu 1,2,3, ...
    /// </summary>
    /// <param name="_puzzleID"></param>
    public void HandleAnimations(int _puzzleID)
    {

        //Käynnistää pulmaan liittyvät animaatiot
        switch (_puzzleID)
        {
            // Pulma 1
            case 1:
                switchAnim.SetTrigger("SwitchLaserOn");
                // Portin aukaisu ääni
                AudioManager.instance.Play("SwitchAnim");
                gateAnim.SetTrigger("GateDown1");
                break;
            // Pulma 2
            case 2:
                switchAnim.SetTrigger("SwitchLaserOn");
                // Portin aukaisu ääni
                AudioManager.instance.Play("SwitchAnim");
                gateAnim.SetTrigger("GateDown1");
                break;
            // Pulma 3
            case 3:
                switchAnim.SetTrigger("SwitchLaserOn");
                // Portin aukaisu ääni
                AudioManager.instance.Play("SwitchAnim");
                gateAnim.SetTrigger("GateDown1");
                break;
            // Pulma 4
            case 4:
                switchAnim.SetTrigger("SwitchLaserOn");
                // Portin aukaisu ääni
                AudioManager.instance.Play("SwitchAnim");
                gateAnim.SetTrigger("GateDown1");
                break;
            default:
                // Ei tehdä mitään
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
