using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundAttach : MonoBehaviour
{

    public GameObject target; // La cible sur laquelle on va attaché l'image de fond

    /**
      * Commentaire: L'image suit les mouvements de la cible en question
      **/
    void Update()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y,0);
    }
}
