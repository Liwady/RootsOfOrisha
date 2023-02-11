using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource player1AS, player2AS, cantResizeAS, collectAS, floatInAS, floatOutAS, sizeInAS, sizeOutAS, gateAS, flameAS, lavaAS, swapAS, eshuLaughAS;

    [SerializeField]
    private AudioClip[] walkingClipsCharacter1, walkingClipsCharacter2, wheelClips, lavaClips, eshuLaughClips;

    [SerializeField]
    private AudioSource[] wheelAS;


    //A public method that plays a sound based on the given input string

    public void PlaySound(string _name)
    {
        switch (_name)
        {
            //If the input string is "walk1", play the walking sound for character 1
            case "walk1":
                if (!player1AS.isPlaying)
                {
                    player1AS.clip = walkingClipsCharacter1[Random.Range(0, walkingClipsCharacter1.Length)];
                    player1AS.Play();
                }
                break;
            //If the input string is "walk2", play the walking sound for character 2
            case "walk2":
                if (!player2AS.isPlaying)
                {
                    player2AS.clip = walkingClipsCharacter1[Random.Range(0, walkingClipsCharacter1.Length)];
                    player2AS.Play();
                }
                break;
            //If the input string is "cantResize", play the "cant resize" sound effect
            case "cantResize":
                cantResizeAS.Play();
                break;
            //If the input string is "collect", play the "collect" sound effect
            case "collect":
                if (!collectAS.isPlaying)
                    collectAS.Play();
                break;
            //If the input string is "floatin", play the "float in" sound effect
            case "floatin":
                if (!floatInAS.isPlaying)
                    floatInAS.Play();
                break;
            //If the input string is "floatout", play the "float out" sound effect
            case "floatout":
                if (!floatOutAS.isPlaying)
                    floatOutAS.Play();
                break;
            //If the input string is "sizein", play the "size in" sound effect
            case "sizein":
                sizeInAS.Play();
                break;
            //If the input string is "sizeout", play the "size out" sound effect
            case "sizeout":
                sizeOutAS.Play();
                break;
            //If the input string is "gate", play the "gate" sound effect
            case "gate":
                if (!gateAS.isPlaying)
                    gateAS.Play();
                break;
            //If the input string is "flame", play the "flame" sound effect
            case "flame":
                if (!flameAS.isPlaying)
                    flameAS.Play();
                break;
            //If the input string is "lava", play a random "lava" sound effect from the list
            case "lava":
                if (!lavaAS.isPlaying)
                {
                    lavaAS.clip = lavaClips[Random.Range(0, lavaClips.Length)];
                    lavaAS.Play();
                }
                break;
            //If the input string is "swap", play the "swap" sound effect
            case "swap":
                swapAS.Play();
                break;
            //If the input string is "eshulaugh", play a random "eshuLaugh" sound effect from the list
            case "eshulaugh":
                Debug.Log("c");
                if (!eshuLaughAS.isPlaying)
                {
                    eshuLaughAS.clip = eshuLaughClips[Random.Range(0, eshuLaughClips.Length)];
                    eshuLaughAS.Play();
                }
                break;
            default:
                break;
        }
    }

    // This function plays a sound based on a given name and a number. If the sound name is "wheel", 
    // it selects a random clip from the wheelClips array and plays it on the AudioSource at the given index.
    public void PlaySound(string _name, int _num)
    {
        switch (_name)
        {
            case "wheel":
                if (!wheelAS[_num].isPlaying)
                {
                    wheelAS[_num].clip = wheelClips[Random.Range(0, wheelClips.Length)];
                    wheelAS[_num].Play();
                }
                break;

            default:
                break;
        }
    }

    // This function stops a sound based on the given name. If the sound name is "gate", it stops the gateAS audio source.
    public void StopSound(string _name)
    {
        switch (_name)
        {
            case "gate":
                gateAS.Stop();
                break;
            default:
                break;
        }
    }

    // This function stops a sound based on the given name and number. If the sound name is "wheel", it stops the audio source 
    // at the given index.
    public void StopSound(string _name, int _num)
    {
        switch (_name)
        {
            case "wheel":
                if (wheelAS[_num].isPlaying)
                    wheelAS[_num].Stop();
                break;
            default:
                break;
        }
    }
}
