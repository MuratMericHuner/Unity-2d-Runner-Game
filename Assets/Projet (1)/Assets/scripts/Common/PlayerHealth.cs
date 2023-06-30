using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 3;      //nombre de point de vie initial du joueur
    public static int currentHealth;    //variable gérant le nombre de point de vie du joueur
    public Image heart1;                //on récupére l'image d'un des coeur pour pouvoir agir dessus
    public Image heart2;                //on récupére l'image d'un des coeur pour pouvoir agir dessus
    public Image heart3;                //on récupére l'image d'un des coeur pour pouvoir agir dessus
    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth == 3)  //si le personnage a 3 pv on laisse les 3 images a leur résolution d'origine
        {
            heart1.rectTransform.sizeDelta = new Vector2(55, 55);
            heart2.rectTransform.sizeDelta = new Vector2(55, 55);
            heart3.rectTransform.sizeDelta = new Vector2(55, 55);
        }
        else if (currentHealth == 2)    //si le personnage n'a plus que 2 pv on réduit la taille du 3ème coeur a 5 de hauteur et 5 de largeur
        {
            heart1.rectTransform.sizeDelta = new Vector2(55, 55);
            heart2.rectTransform.sizeDelta = new Vector2(55, 55);
            heart3.rectTransform.sizeDelta = new Vector2(5, 5);
        }
        else if (currentHealth == 1)    // si le personnage n'a plus que 1 pv on réduit la taille du 2ème coeur et 3ème coeur a 5 de hauteur et 5 de hauteur
        {
            heart1.rectTransform.sizeDelta = new Vector2(55, 55);
            heart2.rectTransform.sizeDelta = new Vector2(5, 5);
            heart3.rectTransform.sizeDelta = new Vector2(5, 5);
        }
        else if (currentHealth == 0)    //si le personnage n'a plus de pv on réduit tout les images a 5 de hauteur et 5 de largeur puis on décrémente de 1 sa vie.
        {
            heart1.rectTransform.sizeDelta = new Vector2(5, 5);
            heart2.rectTransform.sizeDelta = new Vector2(5, 5);
            heart3.rectTransform.sizeDelta = new Vector2(5, 5);
            PlayerMovement.lifeCount--;
            currentHealth = 3;                                  // réinitialisation des pv pour la prochaine réapparition du joueur
            PlayerMovement.isDied = true;                       // passage de la variable isDied a True afin que la mort soit detecter et que les routines soient correctement effectué
        }
    }
}
