using UnityEngine.Audio;
using UnityEngine;
// une classe sound qu'on utilise dans l'Eventmusic
[System.Serializable]
public class Sound
    {
    // la source du clip qu'on met
    public AudioClip clip;
    //le nom qu'on attribue au clip qu'on a mit 
    public string name;
    // la case qui nous permet de determiner si on veut jouer le clip en boucle 
    public bool loop;
    
        // la source de clip qu'on utilise pour faire les operations 
        [HideInInspector]
        public AudioSource source;
        
        [Range(0f,1f)]
        public float volume;     
    }
