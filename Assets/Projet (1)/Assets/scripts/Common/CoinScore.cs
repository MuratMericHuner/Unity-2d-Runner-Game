using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinScore : MonoBehaviour
{
    public Text scoreText;       // on recupère le composant text du score de l'HUD pour pouvoir agir dessus
    public static int score = 0; // on initialise le score en pièce a 0


    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString(); // on écris dans le composant text le score a chaque changement
    }
}
