using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpResepie : MonoBehaviour
{
    [SerializeField] ScoreManager scoreManager;

    /// <summary>
    ///  Jos pelihahmo törmäsi reseptiin, kasvaettaan laskuria ja tuhotaan resepti lopuksi.
    ///  Scoremanager huolehtii pisteiden kasvattamisesta
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Reseptin palan keräys ääni
            AudioManager.instance.Play("PickupResepie");
            scoreManager.AddResepie();
            Destroy(gameObject);
        }
    }
}
