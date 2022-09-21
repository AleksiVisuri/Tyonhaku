using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

/// <summary>
/// Luokka pit‰‰ kirjaa pelihahmon tilatiedoista: nimi, taso, EXP, HP, MP, kakkujen m‰‰r‰, ...
/// Luokka liitet‰‰n pelihahmoon
/// </summary>
public class PlayerHealthManager : MonoBehaviour
{
    // Singelton
    public static PlayerHealthManager instance;

    // Pelihahmon nimi, taso, kokemuspisteet, terveyspisteet ja manapisteet
    public string charName;         // pelihahmon nimi
    public int playerLevel = 1;     // aloitustaso
    public float currentEXP;        // nykyiset kokemuspisteet
    public float maxEXP = 0;        // kokemuspisteet, mitk‰ pit‰‰ saavuttaa seuraavaan tasoon
    public float currentHP = 0;     // nykyiset terveyspisteet
    [SerializeField]
    private float maxHP = 100;      // maksimi terveyspisteet, jotka pelihahmo voi saavuttaa
    public float currentMP = 0;     // nykyiset manapisteet
    [SerializeField]
    private float maxMP = 100;      // Maksimi manapisteet, jotka pelihahmo voi saavuttaa

    public float currentCake;       // kakkujen m‰‰r‰

    // Sliderit
    public Image playerHealthbar;   // Terveyspalkki
    public Text HPText;             // Terveyspalkin teksti
    public Image playerManabar;     // Manapalkki
    public Text MPText;             // Manapalkin teksti
    public float lerpSpeed;         // palkin nopeuden s‰‰din

    // Kokemuspisteisiin liittyv‰t
    public Image EXPbar;            //EXP-palkki
    public Text EXPText;            //EXP-palkin teksti
    public Text playerLevelText;    //Tasolaskurin teksti

    // Start is called before the first frame update
    void Start()
    {
        // Singelton asetettu
        instance = this;
        // MP ja HP pelin alussa
        currentHP = maxHP;
        currentMP = maxMP;
        // EXP pelin alussa
        maxEXP = Mathf.Floor(100 * playerLevel * Mathf.Pow(playerLevel, 0.5f));
    }

    // Update is called once per frame
    void Update()
    {
        // Tarkista pelihahmon tila ja toimi sen mukaan
        CheckPlayerStatus();
    }

    /// <summary>
    /// Tarkistaa pelihahmon tilan ja toimii sen mukaisesti
    /// </summary>
    private void CheckPlayerStatus()
    {
        // Onko HP muuttunut?
        if (currentHP != playerHealthbar.fillAmount)
        {
            // Kyll‰ on, joten p‰ivitet‰‰n tilapalkki (terveys)
            playerHealthbar.fillAmount = Mathf.Lerp(playerHealthbar.fillAmount,
                currentHP / maxHP, Time.deltaTime * lerpSpeed);
            // P‰ivitet‰‰n terveyspisteet (pyˆristettyn‰ kokonaislukuun) myˆs tekstilaatikkoon
            HPText.text = "HP: " + Mathf.Round(playerHealthbar.fillAmount * 100) + " / "
                + maxHP;

        }
        // Onko MP muuttunut?
        if (currentMP != playerManabar.fillAmount)
        {
            // Kyll‰ on, joten p‰ivitet‰‰n tilapalkki (Mana)
            playerManabar.fillAmount = Mathf.Lerp(playerManabar.fillAmount,
                currentMP / maxMP, Time.deltaTime * lerpSpeed);
            // P‰ivitet‰‰n terveyspisteet (pyˆristys) myˆs tekstilaatikkoon
            MPText.text = "MP: " + Mathf.Round(playerManabar.fillAmount * 100) + " / "
                + maxMP;
        }
        // Onko EXP muuttunut?
        if (currentEXP != EXPbar.fillAmount)
        {
            // Kyll‰ on joten p‰ivitet‰‰n tilapalkki (EXP)
            EXPbar.fillAmount = Mathf.Lerp(EXPbar.fillAmount,
                currentEXP / maxEXP, Time.deltaTime * lerpSpeed);
            // P‰ivitet‰‰n EXP-pisteet myˆs tekstilaatikkoon
            EXPText.text = "EXP: " + currentEXP + " / " + maxEXP;
        }
        // Nousiko taso
        if (currentEXP >= maxEXP)
        {
            // Kyll‰ nousi, joten:

            // Siirryt‰‰n seuraavalle tasolle
            playerLevel += 1;
            // P‰ivitet‰‰n taso potrettiin
            playerLevelText.text = playerLevel.ToString();
            // Lasketaan tarvittavat kokemuspisteet seuraavaan tasoon
            maxEXP = Mathf.Floor(100 * playerLevel * Mathf.Pow(playerLevel, 0.5f));
            // Nollataan kokemuspisteet
           // currentEXP = 0;

            // TESTI: Ilmoitus konsoliin
            print("JEELEVELUP!");

        }
    }
    /// <summary>
    /// Jokin lis‰‰ kokemuspisteist‰.
    /// </summary>
    /// <param name="EXPammount"></param>
    public void AddPlayerEXP(int EXPammount)
    {
        currentEXP += EXPammount;
    }

    /// <summary>
    /// Jokin lis‰‰ pelihahmon terveytt‰.
    /// healthAmmount on terveyden lis‰ys
    /// </summary>
    /// <param name="healthAmmount"></param>
    public void AddPlayerHealth(int healthAmmount)
    {
        currentHP += healthAmmount;
        // Jos terveys menee yli maksimin, niin estet‰‰n se
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }
    /// <summary>
    /// Jokin v‰hent‰‰ pelihahmon terveytt‰.
    /// </summary>
    /// <param name="damageToGive"></param>
    public void HurtPlayer(int damageToGive)
    {
        currentHP -= damageToGive;
        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }  
    }
    private void Die()
    {
        print(charName + " kuoli");
    }
    /// <summary>
    /// Jokin lis‰‰ pelihahmon manaa
    /// </summary>
    /// <param name="manaAmmount"></param>
    public void AddPlayerMana(int manaAmmount)
    {
        currentMP += manaAmmount;
        if (currentMP > maxMP)
        {
            currentMP = maxMP;
        }
    }
    /// <summary>
    /// Jokin v‰hent‰‰ pelihahmon manaa.
    /// </summary>
    /// <param name="damageToGive"></param>
    public void HurtPlayerMana(int damageToGive)
    {
        currentMP -= damageToGive;

        // Jos mana menee alle nollan, se estet‰‰n
        if (currentMP <= 0)
        {
            currentMP = 0;
            Die();
        }
    }
    /// <summary>
    /// Nykyiset terveyspisteet nostetaan maksimiin.
    /// </summary>
    public void SetMaxHP()
    {
        currentHP = maxHP;
    }
    /// <summary>
    /// Nykyiset manapisteet nostetaan maksimiin.
    /// </summary>
    public void SetMaxMP()
    {
        currentMP = maxMP;
    }
} // PlayerHealthManager p‰‰ttyy
