using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{

    public GameObject LoadingScreen;        //on récupére le canvas du loading screen pour pouvoir l'activer
    public GameObject[] OldCanvas;          // on récupère active précédement pour la désactiver
    public Slider slider;                   // on récupère le slider pour qu'il indique l'état du chargement

    public void LoadLevel (int sceneIndex) //s'occupe d'initialisé et passer a la scène indiqué en paramètre en indice (retrouvable dans buildSettings)
    {
        CoinScore.score = 0;                // initialisation du score lors du changement de niveau
        PlayerMovement.lifeCount = 3;       // initialisation du nombre de vie pour le prochain niveau
        PlayerHealth.currentHealth = 3;     // initialisation du nombre de point de vie pour le prochain niveau
        StartCoroutine(LoadAsynchronously(sceneIndex));     //délaie indiquer pour le temps du chargement de la prochaine scène
    }

    IEnumerator LoadAsynchronously (int sceneIndex)             //enumérateur permettant de géré le chargement
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);     //lancement de l'opération de changement de scène
        LoadingScreen.SetActive(true);                                          //activation du canvas de chargement
        foreach(GameObject canvas in OldCanvas)                                 //désactivation des anciens canvas
            canvas.SetActive(false);

        while (!operation.isDone)                                               //boucle gérant l'état du chargement
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);           //définission de l'état du chargement de 0 à 1 au lieu de 0 a 0.9 comme d'origine avec Unity

            slider.value = progress;                                            //l'état du chargement est indiqué au slider

            yield return null;
        }
    }
}
