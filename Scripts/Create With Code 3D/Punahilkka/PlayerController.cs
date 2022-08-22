using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Pelihahmon nopeus
    [SerializeField] public float speed = 4f;

    // Referenssi pelihahmon fysiikkamoottoriin
    private Rigidbody2D rb;

    // Referenssi liikevektoriin
    private Vector2 mov;

    // Vain yksi esiintym� pelihahmosta sallitaan
    public static PlayerController instance;

    [SerializeField] private GameObject initialMap;

    // Lippu, joka ilmoittaa voiko pelihahmo liikkua (esim. dialogin aikana ei voi)
    public bool canMove;

    // Referenssi animaattoriin
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        // Onko pelihahmo olemassa
        if (instance == null)
        {
            // Ei ole. Kiinnitet��n pelihahmo
            instance = this;

            // pelihahmo voi liikkua pelin alussa
            canMove = true;

            // Otetaan pelihahmon fysiikkamoottori k�ytt��n
            rb = GetComponent<Rigidbody2D>();

            // Asetetaan kameran n�kem� aloitus alue. T�m� m��ritell��n metodissa SetBound
            Camera.main.GetComponent<MainCamera>().SetBound(initialMap);

            // Otetaan pelihahmon animaattori k�ytt��n
            anim = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movements();
        Animations();
    }

    private void Movements()
    {
        // Otetaaan pelihahmon suuntavektori talteen
        mov = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void Animations()
    {
        // Voiko pelihahmo liikkua?
        if (canMove && mov.magnitude != 0)
        {
            // Kyll�, joten animoidaan liike
            anim.SetFloat("MoveX", mov.x);
            anim.SetFloat("MoveY", mov.y);
            anim.SetBool("Walking", true);
        }
        else
        {
            //Ei voi, joten pys�ytet��n liikeanimaatio
            anim.SetBool("Walking", false);
        }
    }
    private void FixedUpdate()
    {
        // Voiko pelihahmo liikkua?
        if (canMove && mov.magnitude != 0)
        { 
        rb.MovePosition(rb.position + mov * speed * Time.deltaTime);
        }
    }
}
