using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionBox : MonoBehaviour
{
    public int x; // taille horizontale de la boite de detection
    public int y; // taille verticale de la boite de detection
    private BoxCollider2D box; // La boite de collision
    public GameObject obj; // L'objet avec le quel la detection va être réalisée
    private bool isTouching; // Si l'objet est dans la boite de collision
    private bool isAlreadyTouched = false; // Si l'objet est déjà rentré dans la boite de détection

    // Chargement des composants
    void Start()
    {
        box = gameObject.AddComponent<BoxCollider2D>();
        box.size = new Vector2(x, y);
        box.isTrigger = true;
    }

    /**
     * Commentaire: Lorsque l'objet "obj" rentre dans la boite de collision on compare les objets et si la détection n'a pas été faite auparavant,
     * on met les 2 variables "touching" à vrai.
     * Arguments: 
     * -Collision2D collision: L'objet qui rentre en collision avec "obj"
     **/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetInstanceID() == obj.GetInstanceID())
        {
            if (!isAlreadyTouched)
            {
                isAlreadyTouched = true;
            }
            isTouching = true;
        }
    }

    /**
     * Commentaire: méthode "Getter" qui retourne simplement la variable "isTouching"
     **/
    public bool isTouchingBody()
    {
        return isTouching;
    }

    /**
     * Commentaire: méthode "Getter" qui retourne simplement la variable "isAlreadyTouched"
     **/
    public bool isAlreadyTouchedBody()
    {
        return isAlreadyTouched;
    }

    /**
      * Commentaire: Si l'obj quitte la boite de detection on remet la variable "isTouching" à faux.
      * Arguments: 
      * -Collision2D collision: L'objet qui rentre en collision avec "obj"
      **/
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetInstanceID() == obj.GetInstanceID())
        {
            isTouching = false;
        }
    }

    /**
      * Commentaire: méthode "Getter" qui retourne simplement la variable "box"
      **/
    public BoxCollider2D getBox()
    {
        return box;
    }
}
