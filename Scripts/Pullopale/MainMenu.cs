using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string PlayGame1;

    public GameObject PlayButton;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {


        SceneManager.LoadScene(PlayGame1);


    }

    public void QuitGame()
    {

        Application.Quit();

    }








}
