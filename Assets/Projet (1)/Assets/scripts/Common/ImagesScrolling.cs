using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImagesScrolling : MonoBehaviour
{

    private List<Image> hills1 = new List<Image>(); // La première liste d'image
    private List<Image> hills2 = new List<Image>(); // La seconde liste d'image
    private int indexHills1 = 1, indexHills2 = 1; // On initialise les indices à 1
    private float scrollingVelocityHills1 = 15f; // Vitesse de déroulement des images de la liste 1
    private float scrollingVelocityHills2 = 10f; // Vitesse de déroulement des images de la liste 2

    // Chargement des composants
    void Start()
    {

        loadImages();
    }

    // Mise à jour des méthodes en temps Delta
    void Update()
    {
        scrollImages();
    }

    /**
      * Commentaire: On scroll les images une par une jusqu'à la limite imposée. Fonctionne comme une pile. On dépile la dernière image et on la réempile à la fin.
      **/
    private void scrollImages()
    {
        // On parcours la liste 1 contenant les images
        foreach(Image img in hills1)
        {
            // Si la position de l'image est supérieur à 0
            if(hills1[indexHills1].transform.localPosition.x > 0)
            {
                // On décrémente sa position
                img.transform.localPosition -= new Vector3(scrollingVelocityHills1,0,0);
            }
            else
            {
                // Si l'indice est supérieur à la taille de la liste 1
                if(indexHills1 >= this.hills1.Count-1)
                {
                    // on le remet à 0
                    indexHills1 = 0;
                }
                else
                {   
                    // dans le cas contraire on continue d'incrémenter l'indice
                    indexHills1++;
                }
                // Dès qu'on a fini de déplacer toutes les images on vérifie à ce que les images soient bien placées dans les positions vouluent sans perte de position 
                // pour éviter les croisements entre les images
                hills1[indexHills1].transform.localPosition = new Vector3(
                    1920-scrollingVelocityHills1, 
                    hills1[indexHills1].transform.localPosition.y,
                    0);
            }
        }

        // on fait la même chose pour la liste 2
        foreach (Image img in hills2)
        {
            if (hills2[indexHills2].transform.localPosition.x > 0)
            {
                img.transform.localPosition -= new Vector3(scrollingVelocityHills2, 0, 0);
            }
            else
            {
                if (indexHills2 >= this.hills2.Count - 1)
                {
                    indexHills2 = 0;
                }
                else
                {
                    indexHills2++;
                }
                hills2[indexHills2].transform.localPosition = new Vector3(
                    1920 -scrollingVelocityHills2,
                    hills2[indexHills2].transform.localPosition.y,
                    0);
            }
        }
    }

    /**
      * Commentaire: On charge toutes les images.
      **/
    private void loadImages()
    {
        hills1.Add(GetComponentsInChildren<Image>()[4]);
        hills1.Add(GetComponentsInChildren<Image>()[5]);
        hills1.Add(GetComponentsInChildren<Image>()[6]);
        hills2.Add(GetComponentsInChildren<Image>()[1]);
        hills2.Add(GetComponentsInChildren<Image>()[2]);
        hills2.Add(GetComponentsInChildren<Image>()[3]);
    }
}
