using System;
using UnityEngine;
using UnityEngine.Audio;

public class EventMusic : MonoBehaviour
{
    public static EventMusic instance;
    public Sound[] sounds;
    public static bool ismute = false;
    private void Start()
    {
        Play("Theme"); // on joue la musique nomme Theme
    }
    public void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
        if(instance == null)
        {
            instance = this; // si un objet musique n'existe pas on cree l'objet quand on change de scene
        }
        else
        {
            Destroy(gameObject); // si l'objet existe deja on detruit le nouvel objet cree donc on garde une seule musique 
            return;
        }
        DontDestroyOnLoad(transform.gameObject);
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); // Méthode qui joue la musique qu'elle prend en argument
        s.source.Play();
    }
    public void Mute (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); // Méthode qui coupe le son de la musique qu'elle prend en argument
        s.source.mute = true;
    }
    public void unmute(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); // pour reactiver le son coupe 
        s.source.mute = false;
    }
     
}
