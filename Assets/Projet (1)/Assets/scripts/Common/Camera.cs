using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] Transform Target; // la cible qu'on veut suivre

    private Vector3 moveTemp;

    [SerializeField] float speed = 5; // vitesse de la camera
    private float xDifference;
    private float yDifference;
    private float yminvalue = 3; // valeur de x minimum que la camera peut aller
    private float xminvalue = -5; // valeur de y minimum que la camera peut aller
    // la valeur qu'on utilise avant que la camera commence a suivre le joueur comme ca on peut avoir une dead zone 
    // meme si le joueur bouge la camera ne va pas bouger
    [SerializeField] float movementthreshold = 2; 
   //la methode principale qui permet a la camera de suivre le joueur
    void follow()
    {
        moveTemp = Target.transform.position; 
        moveTemp.z = -10;
        moveTemp.y = Mathf.Clamp(Target.position.y, yminvalue, Target.position.y); // la camera ne peut plus depasser x=-5
        moveTemp.x = Mathf.Clamp(Target.position.x, xminvalue, Target.position.x); // la camera ne peut plus depasser y=3

        if (Target.transform.position.x > transform.position.x) 
        {
            xDifference = Target.transform.position.x - transform.position.x; // on prend la difference entre la position x de camera et de joueur
        } else
        {
            xDifference = transform.position.x - Target.transform.position.x; // pareil qu'avant
        }
        if (Target.transform.position.y > transform.position.y)
        {
            yDifference = Target.transform.position.y - transform.position.y; // on prend la difference entre la position y de camera et de joueur
        }
        else
        {
            yDifference = transform.position.y - Target.transform.position.y; // pareil qu'avant
        }
        // si la difference x et y de camera et le joueur est plus grand que movementtreshold alors on bouge la camera vers le joueur 
        if ((xDifference >= movementthreshold || yDifference >= movementthreshold) && (xDifference <= 10 || yDifference <= 10))
        {        
            transform.position =Vector3.MoveTowards(transform.position, moveTemp, speed * Time.deltaTime); 
        }
        if (xDifference > 10 || yDifference > 10)
        {
            // si la difference est trop grande ( surtour pour les respawn ) alors on deplace la camera directe sur le joueur
            //au lieu de bouger la camera lentement vers le joueur
            transform.position = Vector3.Lerp(transform.position, moveTemp, speed * Time.deltaTime);
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        follow();
    }
}
