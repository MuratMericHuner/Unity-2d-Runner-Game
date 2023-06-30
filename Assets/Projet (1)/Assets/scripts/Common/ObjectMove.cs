using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    public bool MoveBetween2dots; // Déplacement entre 2 points
    public Transform firstDot; // Premier point
    public Transform secondDot; // Second point
    public bool isDetectionBox; // activer le déplacement de l'objet que au contact d'une boite de detection
    public bool isOnlyDetectOneTime; // même chose sauf qu'on vérifie qu'une seule fois
    public bool isRotateMoving; // mouvement par rotation
    public bool setStatic; // est en statique
    public DetectionBox box; // boite de détection
    public float velocityX = -1; // vitesse horizontale
    public float velocityY = 0; // vitesse verticale
    public float RotationSpeed = 0; // Vitesse de rotation

    /**
      * Commentaire: On récupère l'objet attaché à la boite. En loccurence ici c'est le joueur
      **/
    void Update() {
      Rigidbody2D body = box.getBox().attachedRigidbody;
      
      if (isDetectionBox) // S'il y a une boite de detection
       {
            if (box.isTouchingBody()) // Si le joueur est bien rentrer en collision avec celle-ci
            {
                if (setStatic) // si l'objet est statique
                    body.bodyType = RigidbodyType2D.Dynamic; // on le met en dynamique afin qu'il ne continue à pas tomber
                else
                    DetectMoving(body); // sinon on active les déplacements
            }
            else
            {
                if(setStatic) // si le statique est actif
                    body.bodyType = RigidbodyType2D.Static; // on le met le corps en statique
            }
        }
      else if (isOnlyDetectOneTime) // Si la detection s'est déjà faite
       {
            if (box.isAlreadyTouchedBody()) // On vérifie si la boite a bien détecté le corps
            {
                // On fait la même chose qu'au dessus

                if (setStatic) 
                    body.bodyType = RigidbodyType2D.Dynamic; 
                else
                    DetectMoving(body);
            }
            else
            {
                // Si le statique est actif
                if (setStatic)
                    body.bodyType = RigidbodyType2D.Static; // on met le corps en statique
            }
      }
      else
      {
            // On effectue les déplacements
                DetectMoving(body);

      }
    }

    /**
      * Commentaire: Méthode effectuant les déplacements de l'objet
      **/
    private void Moving(Rigidbody2D body)
    {
        if (isRotateMoving) // si le déplacement par rotation est actif
        {
            body.transform.localEulerAngles += new Vector3(0, 0, RotationSpeed * Time.deltaTime); // on effectue la rotation
        }
        body.transform.position += new Vector3(velocityX * Time.deltaTime, velocityY * Time.deltaTime, 0); // on effectue les déplacements horizontales et verticales
    }

    /**
      * Commentaire: On effectue les déplacements. Toutefois, s'il y a le déplacement entre 2 points on modifie la vitesse horizontale et verticale
      **/
    private void DetectMoving(Rigidbody2D body)
    {
        if (MoveBetween2dots)
        {
            // Si on se situe à la limite du premier point, on inverse la vitesse horizontale
            if (body.transform.localPosition.x <= firstDot.localPosition.x) 
            {
                if (velocityX < 0)
                {
                    velocityX *= -1;
                    RotationSpeed *= -1;
                }
            }
            // Si on se situe à l'autre extrémité du points càd au second point, on inverse la vitesse horizontale
            if (body.transform.localPosition.x >= secondDot.localPosition.x) 
            {
                if(velocityX > 0)
                {
                    velocityX *= -1;
                    RotationSpeed *= -1;
                }
            }
            // Si les 2 points ne sont pas à la même position verticale
            if (firstDot.localPosition.y != secondDot.localPosition.y)
            {
                // On fait la même chose pour cette fois-ci en verticale
                if (body.transform.localPosition.y <= firstDot.localPosition.y)
                {
                    if (velocityY < 0)
                    {
                        velocityY *= -1;
                    }
                }

                if (body.transform.localPosition.y >= secondDot.localPosition.y)
                {
                    if (velocityY > 0)
                    {
                        velocityY *= -1;
                    }
                }
            }
            else
            {
                velocityY = 0;
            }
            // on effectue les déplacements
            Moving(body);
        }
        else
        {
            // on effectue les déplacements
            Moving(body);
        }
    }

    /**
     * Commentaire: On bloque les déplacements horizontale et verticale dans le cas ou l'objet rentrerait en collision avec le sol par exemple
     * -Collision2D collision: C'est la boite de collision du joueur
     **/
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (setStatic) // Si l'objet est en statique
        {
            foreach(Collider2D coll in this.GetComponents<Collider2D>()){ // On parcours la liste de ses collisions
                if(coll.GetType() != typeof(BoxCollider2D)) // On vérifie que c'est pas la collision principale
                {
                    // On freeze le corps.
                    Rigidbody2D body = box.getBox().attachedRigidbody;
                    if(body.constraints != RigidbodyConstraints2D.FreezeAll)
                        body.constraints = RigidbodyConstraints2D.FreezeAll;
                    return;
                }
            }
        }
    }
}
