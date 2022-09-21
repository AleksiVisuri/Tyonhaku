using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
public class Enemy : MonoBehaviour
{
    // GameOver paneli
    [SerializeField]
    private GameObject gameOverPanel;

    public LayerMask whatIsGround;      // Kerros, jossa vihollinen liikkuu
    public float speed = 1;             // Vihollisen nopeus

    private Rigidbody2D myBody;         // Vihollisen fysiikkamoottori
    private Transform myTrans;          // Vihollisen sijainti
    private float myWidth;              // Vihollisen leveys

    private bool isGrounded;

    // Start is called before the first frame update
    private void Start()
    {
        // Luodaan yhteys vihollisen transformiin
        myTrans = transform;
        // Luodaan ythteys vihollisen fysiikkamoottoriin
        myBody = GetComponent<Rigidbody2D>();
        // Otetaan talteen vihollisen piirtokomponentin leveys
        myWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
    }
    /// <summary>
    /// Vihollinen liikkuu tasolla edestakas.
    /// </summary>
    // Update is called once per frame
    void FixedUpdate()
    {
        // Vihollinen tarkistaa, onko edessä maata (isGrounded = true) ennen kuin liikkuu eteenpäin.
        Vector2 lineCastPos = myTrans.position - myTrans.right * myWidth;
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
        isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, whatIsGround);

        // Jos edessä ei ole maata (isGrounded = false), vihollinen kääntyy ympäri
        if (!isGrounded)
        {
            Vector3 currRot = myTrans.eulerAngles;
            currRot.y += 180;
            myTrans.eulerAngles = currRot;
        }

        // Nämä koodin pätkät pitävät huolen siitä että vihollinen menee aina eteenpäin.
        Vector2 myVel = myBody.velocity;
        myVel.x = -myTrans.right.x * speed;
        myBody.velocity = myVel;
    }

    // Kun pelaaja osuu viholliseen, tulee ääni ja peli loppuu.
    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Kuolemis-ääni
            AudioManager.instance.Play("Death");
            //scoreManager.AddResepie();
            // Näytetään GameOver paneli
            gameOverPanel.SetActive(true);
            // Odotetaan 2 sekunttia
            yield return new WaitForSeconds(2);
            // Piilotetaan
            gameOverPanel.SetActive(false);
            // Aloitetaan peli alusta
            SceneManager.LoadScene(0);
            Destroy(gameObject);
        }
    }
}
