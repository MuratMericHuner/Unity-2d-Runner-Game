using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    public float velocity; // vitesse
    public Transform firstDot; // premier point
    public Transform secondDot; // Second point
    private Transform[] dots; // Liste des points
    private int currentDot; // point à un instant donné

    /**
     * Commentaire: On charge les composants
     **/
    void Start()
    {
        dots = new Transform[]{ firstDot, secondDot };
        currentDot = 0;
    }

    /**
     * Commentaire: On déplace la plateforme au point voulu à chaque frame
     **/
    void Update()
    {
        // On déplace la plateforme à la position donnée
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, dots[currentDot].position, velocity * Time.deltaTime);
        // Si l'objet est arrivé à destination
        if (gameObject.transform.position == dots[currentDot].position)
        {
            // On incrémente le point si n'a pas dépassé la taille de la liste sinon on le remet à 0
            if(currentDot+1 == dots.Length)
            {
                currentDot = 0;
            }
            else
            {
                currentDot++;
            }
        }
    }
}
