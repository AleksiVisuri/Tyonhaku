using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Tehdään pelihahmosta Singelton
    public static PlayerController instance;

    // Liikemuuttujat
    [SerializeField] private float moveSpeed;           // pelihahmon nopeus x-akselin suunnassa
    [SerializeField] private float jumpForce;           // pelihahmon hyppynopeus

    // Näppäinmuuttujat
    public KeyCode left;     // näppäin, joka liikuttaa pelihahmoa vasemmalle
    public KeyCode right;    // näppäin, joka liikuttaa pelihahmoa oikealle
    public KeyCode jump;     // näppäin, joka hypyttää pelihahmoa

    // Referenssi fysiikkamoottoriin
    private Rigidbody2D rb2d;

    // Referenssi piirtokomponenttiin
    private SpriteRenderer spriteRenderer;


    //Referenssi animaattoriin
    //private Animator anim
    public Animator MyAnim { get; set; }

    // Voiko pelihahmo liikkua
    public bool MyCanMove { get; set; }

    // Parempi hyppy
    private float fallMultiplier = 4f; // Mitä suurempi arvo sen nopeammin tullaan alas (1=normitila)
    private float lowJumpMultiplier = 2f; // Mitä suurempi arvo sitä pienempi hyppy (1=normitila)

    // Hyppyyn littyvät muuttujat
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool isGround;                  // Jos IsGround=true, niin että ollaan maassa
    
    // Tomupilvi
    public ParticleSystem footsteps;
    private ParticleSystem.EmissionModule footEmission;

    // Start is called before the first frame update
    void Start()
    {
        // Otetaan Singelton käyttöön
        instance = this;

        // Luodaan yhteys pelihahmon fysiikkamoottoriin
        rb2d = GetComponent<Rigidbody2D>();

        // Luodaan yhteys pelihahmon piirtokomponenttiin
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Luodaan yhteys animaattoriin
        //anim = GetComponent<Animator>();
        MyAnim = GetComponent<Animator>();

        // Pelihahmo voi liikkua oletuksena
        MyCanMove = true;

        // Luodaan yhteys Dust-efektin partikkelisysteemiin (emissio)
        footEmission = footsteps.emission;
    }

    // Update is called once per frame
    void Update()
    {
        // Voiko pelihahmo liikkua? false = ei voi, tämä tieto saadaan PuzzleControllerilta
        if (MyCanMove == false)
        {
            // Ei voi, joten estetään pelihahmon liike ja hypätään Update-metodista pois
            rb2d.velocity = Vector2.zero;
            return;
        }
        // Tutkitaan onko kurpitsapoika maassa vai ilmmassa.
        // Ilmassa silloin kun isGround = false ja maassa kun isGround = true
        isGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);

        // Liikutetaan pelihahmoa
        MovePlayer();

        // Käsittele animaatiot
        HandleAnimation();
    }

    /// <summary>
    ///  Käsittelee kurpitsapojan animaatiot
    /// </summary>
    private void HandleAnimation()
    {
        // Kävely animaation kutsu
        //anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x)); // Mathf.Abs() varmistaa että x eli nopeus on aina positiivinen
        MyAnim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x)); // Mathf.Abs() varmistaa että x eli nopeus on aina positiivinen
        // Hyppy animaation kutsu
        //anim.SetBool("Grounded", isGround);
        MyAnim.SetBool("Grounded", isGround);
    }

    private void MovePlayer()
    {
        // Liikkuuko pelihahmo vasemmalle?
        if (Input.GetKey(left))
        {
            // Kyllä, joten suoritetaan liike
            rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
            // Varmistetaan että pelihahmo katsoo menosuuntaan
            spriteRenderer.flipX = true;
        }
        // Liikutaanko oikealle?
        else if (Input.GetKey(right))
        {
            // Kyllä, joten suoritetaan liike
            rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
            // Varmistetaan että pelihahmo katsoo menosuuntaan
            spriteRenderer.flipX = false;
        }
        else
        {

            // Onko vielä ilmassa?
            if (rb2d.velocity.y != 0)
            {
                // Kyllä, joten liike jatkuu
                rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y);
            }
            else
            {
                // Ei, joten liike päättyy
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
        }

        // Painettiinko hyppypainiketta?
        if (Input.GetKeyDown(jump))
        {
            // Hyppyääni
            AudioManager.instance.Play("Jump");

            // Kyllä, joten pelihahmo hyppää
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        }
        // Parannetaan hyppyä
        BetterJump();

        if (isGround && (Input.GetKey(right) || Input.GetKey(left)))
        {
                footEmission.rateOverTime = 35f;
        }

        else
        {
            footEmission.rateOverTime = 0f;
        }

    }
    /// <summary>
    /// Kún hyppy painiketta painetaan nopeasti pelihahmop hyppää lyhyemmän matkan
    /// Kun hyppypainiketta pidetään alhaalla pelihahmo hyppää korkeammalle ja
    /// hyppy kestää pidempään
    /// </summary>
    private void BetterJump()
    {
        // Ollaanko tulossa alaspäin?
        if (rb2d.velocity.y < 0)
        {
            // Näppäintä painetaan --> korkeampi hyppy
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;    
        }
        // Ollaanko ilmassa ja hyppypainiketta ei paineta?
        else if (rb2d.velocity.y > 0 && !Input.GetKey(jump))
        {
            // Kyllä joten --> matalampi hyppy
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;   
        }
    }
}
