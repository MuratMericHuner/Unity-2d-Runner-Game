using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    // 0: Vérification de la deadzone au-dessus de y 
    // 1: au-dessous de y
    public int updown;
    // Objet du joueur
    public GameObject player;

    /**
      * Commentaire: Si la deadzone est initialisé comme étant au-dessus de y alors on vérifie si le joueur dépassé la deadzone on le tue, sinon s'il est
      * au-dessous de la deadzone on suit la même démarche.
      **/
    void Update()
    {
        if (updown == 0)
        {
            if(player.transform.position.y >= transform.position.y)
            {
                PlayerMovement.isDied = true;
            }
        }
        else if(updown == 1)
        {
            if (player.transform.position.y <= transform.position.y)
            {
                PlayerMovement.isDied = true;
            }
        }
    }
}
