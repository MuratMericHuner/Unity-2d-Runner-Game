using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventMenu : MonoBehaviour
{
    #region
    public Button buttonPlay;
    public Button buttonOption;
    public Button buttonExit;
    public Button buttonAchievements;   
    public Button buttonlvl;
    public Canvas MenuPrincipal;
    public Canvas Options;
    public Canvas Niveaux;

    #endregion

    // Méthode qui cocnerne l'ouverture de l'onglet option
    private void EventOption()
    {
        // Les buttons du menu principal sont rendus transparent
        MenuPrincipal.gameObject.SetActive(false);

        // Les buttons de l'onglet option sont rendus visible
        Options.gameObject.SetActive(true);

    }
    // Méthode qui nous permet d'acceder a l'onglet niveaux 
    private void EventNiveaux()
    {
        MenuPrincipal.gameObject.SetActive(false);

        Niveaux.gameObject.SetActive(true);
 
    }

    // Méthode concernant la fermeture du jeu
    private void EventExit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
            // On attribue à chaque bouton sa méthode
            //buttonPlay.onClick.AddListener(EventPlay);
            buttonOption.onClick.AddListener(EventOption);
            buttonExit.onClick.AddListener(EventExit);
            buttonlvl.onClick.AddListener(EventNiveaux);        
    }
}