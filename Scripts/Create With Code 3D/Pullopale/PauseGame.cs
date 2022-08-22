using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PauseGame : MonoBehaviour
{

    public GameObject Continue;

    public GameObject Quit;

    public string QuitGame;
    
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            Continue.SetActive(true);

            Quit.SetActive(true);

            pauseGame();

        }

    }

    private void pauseGame()
    {

        Time.timeScale = 0f;

    }

    public void ResumeGame()
    {

        Time.timeScale = 1f;

        Continue.SetActive(false);

        Quit.SetActive(false);



    }

    public void quitGame()
    {

        Time.timeScale = 1f;

        SceneManager.LoadScene(QuitGame);


    }







}
