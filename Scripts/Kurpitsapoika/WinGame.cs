using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Tämä luokka lisätään RightWall-GameObjektiin. Tämä koodi suoritetaan kun kaikki reseptit on kerätty
/// </summary>
public class WinGame : MonoBehaviour
{
    // Voittopaneeli
    public GameObject winPanel;

    // Pause
    public static bool gameIsPaused;

    // Update is called once per frame
    private void Update()
    {
        // ESCillä pääsee päävalikkoon
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Pysäytetään kaikki musa
            AudioManager.instance.StopAll();
            // Peli käynnistyy
            Time.timeScale = 1;
            // Aloitetaan uusi peli
            SceneManager.LoadScene("Mainmenu");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Pysäytetään musat
        AudioManager.instance.StopAll();

        // Soitetaan loppufanfaari
        AudioManager.instance.Play("GameWin");

        // Näytetään pelihahmon 2 voittopaneli
        winPanel.SetActive(true);

        // Pysäytetään peli
        gameIsPaused = !gameIsPaused;
    }
    
    // Tämä metodi pysäyttää pelin ja käynnistää pelin tarvittaessa

    void PauseGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
