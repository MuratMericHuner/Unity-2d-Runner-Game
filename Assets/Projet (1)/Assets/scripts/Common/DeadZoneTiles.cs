using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DeadZoneTiles : MonoBehaviour
{
    public string[] tiles;  // liste des noms des blocs
    private Tilemap tilemap; // La map
    private bool isRight = true; // boolean de vérification

    /*
     * Chargement des composants
     * 
    */
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    /**
     * Commentaire: Lorsque le joueur rentre en contact avec un bloc qui figure dans la liste au-dessus, on lui fait perdre une vie.
     * Arguments: 
     * -Collision2D collision: C'est la boite de collision du joueur
     **/
    void OnCollisionStay2D(Collision2D collision)
    {
        Rigidbody2D body = collision.rigidbody;
        Vector3Int cellPosition = tilemap.WorldToCell(new Vector3(body.position.x, body.position.y-1, 0));
        TileBase tile = tilemap.GetTile(cellPosition);
        if (tile != null)
        {
            if (TilesContains(tile.name) && isRight)
            {              
                PlayerMovement.isDied = true;
                PlayerMovement.lifeCount--;
                StartCoroutine(SlowDetection());
            }
        }
    }

    /**
     * Commentaire: Même chose qu'au dessus, excepté le faite qu'ici on utilise un trigger(déclencheur)
     * Arguments: 
     * -Collision2D collision: C'est la boite de collision du joueur
     **/
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Player") {
            Transform pos = collision.transform;
            Vector3Int cellPosition = tilemap.WorldToCell(new Vector3(pos.position.x, pos.position.y, 0));
            TileBase tile = tilemap.GetTile(cellPosition);
            if (tile != null)
            {
                if (TilesContains(tile.name) && isRight)
                {                    
                    PlayerMovement.isDied = true;
                    PlayerMovement.lifeCount--;
                    StartCoroutine(SlowDetection());
                }
            }
        }
    }

    /**
     * Commentaire: Pour éviter les détections successives de collisions on met un cooldown avant de relancer la détection de la collision du joueur
     * ( C'est pour éviter les pertes de vies excessives )
     **/
    IEnumerator SlowDetection()
    {
        isRight = false;
        yield return new WaitForSeconds(1.5f);
        isRight = true;
    }

    /**
     * Commentaire: Vérifie la présence du bloc dans la liste fournie
     * Arguments: 
     * -string name: Nom du bloc
     **/
    public bool TilesContains(string name)
    {
        foreach(string tiles in tiles)
        {
            if (tiles.ToLower() == name.ToLower())
            {
                return true;
            }
        }
        return false;
    }
}
