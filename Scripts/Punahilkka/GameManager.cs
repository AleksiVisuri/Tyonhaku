using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Luokka liitet‰‰n saman nimiseen GameObjectiin.
/// T‰m‰ luokka est‰‰ pelihahmon liikkumisen silloin, kun pelihahmo on dialogissa jonkun kanssa.
/// </summary>
public class GameManager : MonoBehaviour
{
    // Sallitaan vain yksi GameManager
    public static GameManager instance;

    // Lippu, joka kertoo onko dialogi k‰ynniss‰. T‰t‰ k‰ytt‰‰‰ mm. DialogiManager-luokka
    public bool dialogActive;

    // Start is called before the first frame update
    void Start()
    {
        // Onko GameManager jo olemassa?
        if (instance == null)
        {
            // Ei ole, joten luodaan GameManager
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Onko dialogi aktiivinen? Tieto tulee DialogManagerilta
        if (dialogActive)
        {
            // DialogManagerin mukaan on, joten estet‰‰n pelihahmon liikkuminen
            PlayerController.instance.canMove = false;
        }
        else
        {
            // DialogManagerin mukaan ei, joten sallitaan pelihahmon liikkuminen
            PlayerController.instance.canMove = true;
        }
    }
}
