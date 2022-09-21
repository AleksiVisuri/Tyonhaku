using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Siirt‰‰  pelihahmon toiselle alueelle. Alueelle on m‰‰ritelty piste (Warp/Exit),
/// johon pelihahmo ilmestyy.
/// Luokka liitet‰‰n Warp GameObjectiin
/// </summary>
public class Warp : MonoBehaviour
{

    // Alue (Tiled-kartta), jonne kamera ja pelihahmo siirret‰‰n.
    public GameObject targetMap;

    // Piste alueella (Warp/Exit) jolle pelihahmo ja kamera siirtyy.
    public GameObject target;

    // Aluetekstiin liittyv‰t muuttujat
    public bool needText;
    public string placeName;
    public GameObject text;
    public TextMeshProUGUI placeText;
    private void Awake()
    {

        //Piilotetaan Warpit n‰kyvist‰
        // Haetaan Warp-objektin SpriteRenderer ja piilotetaan se
        GetComponent<SpriteRenderer>().enabled = false;
        // Haetaan Warp/Exit-objektin SpriteRenderer ja piilotetaan se
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
    }
    IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        // Onko tˆrm‰‰j‰ pelihahmo?
        if (collision.CompareTag("Player"))
        {
            // Piilotetaan pelihahmo
            collision.gameObject.SetActive(false);

            // Etsit‰‰n Screen Fader GameObjekti
            ScreenFader sf = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();

            // Pyydet‰‰n ScreenFader -luokkaa aloittamaan pimennys alirutiini (coroutine)
            yield return StartCoroutine(sf.FadeToBlack());

            // Kyll‰ on, joten siirret‰‰n pelihahmo toiselle alueelle kohtaan Warp/Exit
            // GetChild(0).transform.position palauttaa lapsiobjektin (exit) sijainnin
            collision.transform.position = target.transform.GetChild(0).transform.position;

            // Pyydet‰‰n MainCamera-luokkaa siirret‰‰n myˆs kamera kohdealueelle
            Camera.main.GetComponent<MainCamera>().SetBound(targetMap);

            // Pit‰‰kˆ alueteksti n‰ytt‰‰?
            if (needText)
            {
                // Kyll‰ pit‰‰, joten aloitetaan aluetekstin n‰ytt‰minen alirutiinissa (coroutine)
                StartCoroutine(placeNameCo());
            }
            // N‰ytet‰‰n pelihahmo.
            collision.gameObject.SetActive(true);

            // Pyydet‰‰n ScreenFader -luokkaa aloittamaan valaistus-alirutiini (coroutine)
            yield return StartCoroutine(sf.FadeToClear());
        }
    }

    // Alirutiini n‰ytt‰‰ aluetekstin 4 sekunnin ajan, kun tullaan uudelle alueelle
    
    IEnumerator placeNameCo()
    {
        // N‰ytet‰‰m uuden aluen nimi
        text.SetActive(true);
        placeText.text = placeName;

        // Odotetaan 4 sek.
        yield return new WaitForSeconds(4f);

        // Ja piilotetaan aluenimi, kun 4 sek on kuunut
        text.SetActive(false);
    }

}
