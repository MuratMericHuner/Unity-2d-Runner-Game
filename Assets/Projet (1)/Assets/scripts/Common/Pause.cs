using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public static bool isPaused = false;
    public static bool inoptions = false;
    public GameObject pauseMenuUI;
    public GameObject pauseOptionUI;
    public Button resumeButton;
    public Button menuButton;
    public Button optionButton;
    public Button exitButton;
    public Button returnButton;
    public Button buttonControl1;
    public Button buttonControl2;

    void Update()
    {
        // quand on appuie sur escape si on est dans les options on quitte les options
        // si on est dans le canvas pause on resume le jeu si on est dans le jeu on pause le jeu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused && inoptions==false)
            {
                Resume();
            }
            else if(!isPaused && inoptions==false)
            {
                PauseMenu();
            }
            else if (isPaused && inoptions==true)
            {
                Quitoption();
            }
        }
        resumeButton.onClick.AddListener(Resume);
        menuButton.onClick.AddListener(LoadMenu);    
        optionButton.onClick.AddListener(LoadOption);                              
        returnButton.onClick.AddListener(Quitoption);                      
        exitButton.onClick.AddListener(QuitGame);
        buttonControl1.onClick.AddListener(EventControl1);
        buttonControl2.onClick.AddListener(EventControl2);
    }
    // la Méthode qui nous permet de retourner au jeu
    public void Resume()
    {
        isPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;            //on réactive le jeu 
    }
    // Méthode qui pause le jeu 
    public void PauseMenu()
    {
        isPaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;            //on freeze le jeu pour empecher les déplacement du joueur
    }
    // retourner au menu un temps de chargement 
    public void LoadMenu()
    {
        Debug.Log("Loading menu...");
        Time.timeScale = 1f;            //on réactive le jeu 
    }
    // Méthode qui ouvre l'onglet option
    public void LoadOption()
    {
        Debug.Log("Loading option...");
        pauseOptionUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        isPaused = true;
        inoptions = true;
    }
    // Méthode qui quitte l'option
    public void Quitoption()
    {
        isPaused = true;
        inoptions = false;
        pauseOptionUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
    public void QuitGame() //fonction pour quitter le jeu
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    private void EventControl1()
    {
        EventOption.control = 1; //change les controles sur les controles numéro 1
    }

    private void EventControl2()
    {
        EventOption.control = 2; //change les controles sur les controles numéro 2
    }
}
