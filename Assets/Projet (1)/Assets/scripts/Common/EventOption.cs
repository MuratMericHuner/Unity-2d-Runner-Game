using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventOption : MonoBehaviour
{
    #region
    public Button buttonMenu;
    public Canvas MenuPrincipal;
    public Button buttonControl1;
    public Button buttonControl2;
    public static int control = 1;
    public Canvas Option;
    Resolution[] resolutions;
    public Dropdown resolutionDropdown;
    #endregion

    private void Start()
    {
        resolutions = Screen.resolutions;           
        resolutionDropdown.ClearOptions();
 
        List<string> options = new List<string>();      //création d'une liste qui récupérera la liste des résolution du PC
        int currentResolutionIndex = 0;     

        for (int i = 0; i<resolutions.Length;i++)       //boucle stockant les différentes résolution du PC
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);         //ajout des configurations au dropdown
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetFullscreen(bool isFullscreen)        //fonction pour mettre en fullscreen si le checkbutton est sur false ou sur true
    {
        Screen.fullScreen = isFullscreen;
    }

    private void EventMenu()                            //fonction pour activer le bon canvas
    {
        // On rend invisible l'onglet option
        Option.gameObject.SetActive(false);             

        // On rend visible le menu principal
        MenuPrincipal.gameObject.SetActive(true);
    }
    private void EventControl1()                        //event pour le bouton de control 1
    {
        control = 1;                                    // selection du control 1
    }

    private void EventControl2()                        //event pour le bouton de control 2
    {
        control = 2;                                    // selection du control 2
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))           // désactivation de la pause si bouton Escape appuyé
        {
            EventMenu();
        }
            // On attribut l'événement pour activer le menu
        buttonMenu.onClick.AddListener(EventMenu);
        buttonControl1.onClick.AddListener(EventControl1);  //evenement pour les control 1 et 2
        buttonControl2.onClick.AddListener(EventControl2);

    }

    public void SetResolution (int resolutionIndex)         //fonction permettant de changer de résolution
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

}