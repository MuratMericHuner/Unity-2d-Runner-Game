using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Soundcontrol : MonoBehaviour
{
    public Sprite soundon;
    public Sprite soundoff;
    public Button buttonSound;

    void Update()
    {
        buttonSound.onClick.AddListener(EventMute); 
    }
    private void EventMute()
    {   // si la musique n'est pas mute on coupe le son 
        if (EventMusic.ismute==false)
        {
            buttonSound.GetComponent<Image>().sprite = soundoff;
            print("ok1");
            FindObjectOfType<EventMusic>().Mute("Theme");
            EventMusic.ismute = true;            
        }
        else
        { // si le son est coupe on resume le son 
            buttonSound.GetComponent<Image>().sprite = soundon;           
            print("ok2");
            FindObjectOfType<EventMusic>().unmute("Theme");
            EventMusic.ismute = false;

        }
    }
}
