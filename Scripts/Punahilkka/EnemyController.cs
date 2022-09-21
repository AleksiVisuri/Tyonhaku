using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Vihollisen tilat, joissa se voi olla
public enum EnemyState
{
    idle,
    walk,
    run,
    attack
}

public class EnemyController : MonoBehaviour
{
    // Muuttujat
    [SerializeField]
    private EnemyState currentState;    // Nykyinen tila
    public float moveSpeed;             // Liikenopeus
    private Rigidbody2D myRigidbody;    // Fysiikkamoottori
    public Transform target;            // Hy�kk�yksen kohde
    public float chaseRadius;           // Havaintoalue
    public float attackRadius;          // Hy�kk�ysalue
    public Animator anim;               // Animaattori
    [SerializeField]
    private int damageToGive;           // Vahinko, jonka vihollinen aiheuttaa

    // Start is called before the first frame update
    void Start()
    {
        // Vihollisen tila pelin alussa on idle
        currentState = EnemyState.idle;

        // Referenssi fysiikkamoottoriin
        myRigidbody = GetComponent<Rigidbody2D>();

        // Referenssi animaattoriin
        anim = GetComponent<Animator>();

        // Hy�kk�yksen kohde
        target = GameObject.FindWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        // TransformDirection laskee ensin suunnan, jossa kohde on
        // ja piirt�� sitten punaisen viivan vihollisesta kohteeseen (n�kyy vain Sceness�)
        Vector3 forward = transform.TransformDirection(target.transform.position - transform.position);
        Debug.DrawRay(transform.position, forward, Color.red);

        // Tarkistetaan onko punahilkka tullut liian l�helle vihollista
        CheckDistance();
    }

    /// <summary>
    /// Tarkistaa onko pelihahmo (punahilkka) vihollisen (susi) havainto- tai hy�kk�ysalueella
    /// </summary>
    private void CheckDistance()
    {
        // Lasketaan et�isyys vihollisen ja pelihahmon v�lill�
        float distance = Vector3.Distance(target.position, transform.position);

        // Onko punahilkka havaintoalueella?
        if (distance <= chaseRadius && distance > attackRadius)
        {
            // Kyll� on. Tarkistetaan onko vihollisen tila idle?
            if (currentState == EnemyState.idle || currentState == EnemyState.run)
            {
                // Kyll� on, joten liikutetaan vihollista kohti pelihahmoa
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position,
                    moveSpeed * Time.deltaTime);
                // Vaihda animaation suuntaa
                changeAnim(temp - transform.position);
                // Liikutetaan vihollista kohti pelihahmoa
                myRigidbody.MovePosition(temp);

                // Vaihda vihollisen tila juoksuun
                ChangeState(EnemyState.run);

                // Animoidaan liike eli kutsutaan Blend Tree tilaa
                anim.SetBool("Running", true);

            }
        }
        // Onko punahilkka havaintoalueen ulkopuolella
        else if (distance > chaseRadius)
        {
            // kutsutaan WakeUp tilaa
            anim.SetBool("Running", false);

            // Nykyinen tila vaihdetaan idle
            ChangeState(EnemyState.idle);
        }
        // Onko punahilkka hy�kk�ysalueella
        else if (distance < attackRadius)
        {
            // Kyll� on, joten vihollinen hy�kk�� ja aiheutta vahinkoa
            PlayerHealthManager.instance.HurtPlayer(damageToGive);
        }
    }

    /// <summary>
    /// Kertoo animaattorille mik� animaatio suoritetaan
    /// </summary>
    /// <param name="setVecktor"></param>
    private void SetAnimFloat(Vector2 setVector)
    {
        anim.SetFloat("MoveX", setVector.x);
        anim.SetFloat("MoveY", setVector.y);
    }

    /// <summary>
    /// Vaihtaa animaation (alas, yl�s, vasen tai oikea) pleihahmon sijainnin perusteella
    /// </summary>
    /// <param name="direction"></param>
    private void changeAnim(Vector2 direction)
    {
        // Onko pelihahmo vihollisen vasemmalla tai oikealla puolella?
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            // Onko pelihahmo oikealla puolella?
            if (direction.x > 0)
            {
                print("Oikealla");
                // Suoritetaan oikealle menev� animaatio (1,0)
                SetAnimFloat(Vector2.right);
            }
            // Onko pelihahmo vasemmalla puolella?
            else if (direction.x < 0)
            {
                print("Vassemmalla");
                // Suoritetaan vasemmalle menev� animaatio (-1,0)
                SetAnimFloat(Vector2.left);
            }
        }
        // Onko pelihahmo vihollisen ala tai yl� puolella?
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            // Onko pelihahmo yl�puolella?
            if (direction.y > 0)
            {
                // Suoritetaan yl�s menev� animaatio (0, 1)
                SetAnimFloat(Vector2.up);
            }
            // Onko pelihahmo alapuolella?
            else if (direction.y < 0)
            {
                // Suoritetaan alas menev� animaatio (0,-1)
                SetAnimFloat(Vector2.down);
            }
        }

    }

    /// <summary>
    /// Vaihtaa vihollisen tilaa. Esimerkiksi idlesta --> walking ja p�in vastoin
    /// </summary>
    /// <param name="newState"></param>
    private void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }

    /// <summary>
    /// Piirt�� vihollisen ymp�rille havainto- ja hy�kk�ysalueet (vain Sceness�)
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);     // Havaintoalue (uloin rengas)

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRadius);    // Hy�kk�ysalue (sisempi rengas)
    }

}   // EnemyController.cs p��ttyy
