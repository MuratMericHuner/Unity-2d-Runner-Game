using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventNiveaux : MonoBehaviour
{
    public Button buttonMenu;
    public Canvas MenuPrincipal;
    public Canvas lvl;
    public static bool[] unlockedLevels;
    public Button[] levelsButton;
    public Sprite unlocked;
    // si on a debloque un niveau on peut choisir le niveau pour jouer
    void Awake()
    {
        unlockedLevels = new bool[levelsButton.Length];
        int i = 0;
        foreach(Button b in levelsButton)
        {
            if(i == 0)
            {
                unlockedLevels[i] = true;
            }
            else
            {
                unlockedLevels[i] = false;
            }
            i++;
        }
        unlockedLevels[1] = true;
    }
    // le niveau d'avant est debloque 
    public static void setUnlockedLevel(int level)
    {
        unlockedLevels[level - 1] = true;
    }

    private void EventMenu()
    {
        // On rend invisible l'onglet option
        lvl.gameObject.SetActive(false);

        // On rend visible le menu principal
        MenuPrincipal.gameObject.SetActive(true);
    }

    private void LevelLoader(int level)
    {
        SceneManager.LoadScene("Level"+level);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EventMenu();
        }
        // On attribut l'événement pour activer le menu
        buttonMenu.onClick.AddListener(EventMenu);

        int i = 0;
        foreach(Button b in levelsButton)
        {
            if (unlockedLevels[i])
            {
                b.interactable = true;
                b.transition = Selectable.Transition.ColorTint;
                b.image.sprite = unlocked;
                b.onClick.AddListener(() => { LevelLoader(i + 1); });
            }
            i++;
        }
    }
}
