using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    private bool isRight = true; // Boolean de vérification

    /**
     * Commentaire: On retire une vie au joueur s'il rentre en collision avec l'ennemis
     * Arguments: 
     * -Collision2D collision: L'objet qui rentre en collision avec "obj"
     **/
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Player") // si la collisio s'effectue avec le joueur
        {
            foreach(Collider2D collider in GetComponents<Collider2D>()) // On parcours tous les types de collisions attachées aux joueurs
            {
                if(collider.GetType() != typeof(BoxCollider2D)) // si le type de collision est différent de l'ennemis
                {
                    // SI le joueur n'est pas mort est que le boolean est actif
                    if (!PlayerMovement.isDied && isRight) {
                        PlayerHealth.currentHealth--; // on lui retire une vie
                        StartCoroutine(SlowDetection()); // on met un cooldown

                    }
                }
            }
        }
    }

    /**
      * Commentaire: Un simple cooldown pour ralentir la vérification d'un éventuel collision 
      **/
    IEnumerator SlowDetection()
    {
        isRight = false;
        yield return new WaitForSeconds(1.5f);
        isRight = true;
    }
}
