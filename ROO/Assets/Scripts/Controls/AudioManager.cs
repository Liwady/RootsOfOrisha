using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource player1AS, player2AS, cantResizeAS, collectAS, floatInAS, floatOutAS;

    [SerializeField]
    private AudioClip[] walkingClipsCharacter1, walkingClipsCharacter2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PutRandomClip(AudioClip[]_clips, AudioSource _source)
    {

    }
    public void PlaySound(string _name)
    {
        Debug.Log(_name);
        switch (_name)
        {
            case "walk1": //char 1
                if (!player1AS.isPlaying)
                {
                    player1AS.clip = walkingClipsCharacter1[Random.Range(0, walkingClipsCharacter1.Length)];
                    player1AS.Play();
                }
                break;
            case "walk2": //char 2
                if (!player2AS.isPlaying)
                {
                    player2AS.clip = walkingClipsCharacter1[Random.Range(0, walkingClipsCharacter1.Length)];
                    player2AS.Play();
                }
                break;
            case "cantResize":
                   cantResizeAS.Play();
                break;
            case "collect":
                if (!collectAS.isPlaying)
                collectAS.Play();
                break;
            case "floatin":
                if(!floatInAS.isPlaying)
                floatInAS.Play();
                break;
            case "floatout":
                if (!floatOutAS.isPlaying)
                    floatOutAS.Play();
                break;
            default:
                break;
        }
    }

    public void StopSound(string _name)
    {
        switch (_name)
        {
            case "1": //char 1
                player1AS.Play();
                break;
            case "2": //char 2
                player2AS.Play();
                break;
            default:
                break;
        }
    }
}
