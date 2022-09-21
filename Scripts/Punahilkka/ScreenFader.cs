using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    // referenssi animaattoriin
    private Animator anim;

    // Lippu, joka kertoo että animaation on suoritettu loppuun
    private bool isFading = false;

    // Start is called before the first frame update
    void Start()
    {
        // Otetaan animaattori käyttöön
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Nostaa esiripun (valaistus-animaatio)
    /// </summary>

    public IEnumerator FadeToClear()
    {
        isFading = true;
        anim.SetTrigger("FadeIn");

        // Silmukkaa suoritetaan niin kauan, kunnes isFading = false
        while (isFading)
            yield return null;
    }

    // Laskee esiripun (pimennys-animaatio)

    public IEnumerator FadeToBlack()
    {
        isFading = true;
        anim.SetTrigger("FadeOut");

        // Silmukkaa suoritetaan niin kauan, kunnes isFading = false
        while (isFading)
            yield return null;
    }
    void AnimationComplete()
    {
        isFading = false;
    }
}
