using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    public GameObject GameObject;

    public string MainMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


     }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {

            //gameOverPanel.SetActive(true);

            //SceneManager.LoadScene(MainMenu);

            StartCoroutine(gameOver());

        }

        if (collision.CompareTag("Ammus"))
        {

        Destroy(GameObject);

        }
    }

    private IEnumerator gameOver()
    {

        gameOverPanel.SetActive(true);

        yield return new WaitForSeconds(2);

        gameOverPanel.SetActive(false);

        SceneManager.LoadScene("MainMenu");

    }









}
