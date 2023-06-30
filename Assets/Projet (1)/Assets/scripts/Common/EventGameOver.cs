using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class EventGameOver : MonoBehaviour
{
    public Button buttonquit;

  
    public void EventExit()
    {
        Application.Quit();
    }
    
    // Update is called once per frame
    void Update()
    {
        buttonquit.onClick.AddListener(EventExit);
    }
}
