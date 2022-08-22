using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    // Käynnissä oleva aika
    private float currentTime = 0f;

    // Aloitus aika
    [SerializeField]
    private float startingTime = 0f;

    // Tekstilaatikko, jossa aika näytetään
    [SerializeField]
    private Text countdownTimerText;

    // Tekstin väri
    private Color color = new Color(1, 1, 1, 1);

    // GameOver paneli
    [SerializeField]
    private GameObject gameOverPanel;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        StartCounting();
    }

    void StartCounting()
    {
        // ALoitetaan vähentämään aikaa
        currentTime -= Time.deltaTime;

        // Varmistetaan että aika ei mene alle 0:00
        currentTime = Mathf.Clamp(currentTime, 0f, Mathf.Infinity);

        // Päivitetään aika
        countdownTimerText.text = DisplayTime(currentTime);

        // Ajastimen väri
        countdownTimerText.color = color;

        // Vaihdetaan väriä kun aikaa on enää 10 sekunttia
        if (currentTime <= 10)
        {
            // Väri vaihtuu
            color = new Color(255, 0, 0, 1);

            // Tähän lisätään Beep ääni

            // Loppuiko aika?
            if (currentTime <= 0)
            {
                // Nollataan laskuri
                currentTime = 0;

                // Tähän tulee GameOver kooodi
                StartCoroutine(GameOver());
            }
        }
    }

    // Metodi muuttaa sekunnit minuuteiksi ja sekunneiksi ja palautttaa ajan kutsujalle
    private string DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        return "Aikaa " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Näyttää GameOver panelia 2 sekunttia, jonka jälkeen peli alkaa alusta
    private IEnumerator GameOver()
    {
        // Näytetään GameOver paneli
        gameOverPanel.SetActive(true);
        // Odotetaan 2 sekunttia
        yield return new WaitForSeconds(2);
        // Piilotetaan
        gameOverPanel.SetActive(false);
        // Aloitetaan peli alusta
        SceneManager.LoadScene(0);
    }
}
