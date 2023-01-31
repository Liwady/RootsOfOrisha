using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource player1AS, player2AS, cantResizeAS, collectAS, floatInAS, floatOutAS, sizeInAS, sizeOutAS, gateAS, flameAS, lavaAS, swapAS, eshuLaughAS;

    [SerializeField]
    private AudioClip[] walkingClipsCharacter1, walkingClipsCharacter2, wheelClips, lavaClips, eshuLaughClips;

    [SerializeField]
    private AudioSource[] wheelAS ;



    public void PlaySound(string _name)
    {
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
                if (!floatInAS.isPlaying)
                    floatInAS.Play();
                break;
            case "floatout":
                if (!floatOutAS.isPlaying)
                    floatOutAS.Play();
                break;
            case "sizein":
                sizeInAS.Play();
                break;
            case "sizeout":
                sizeOutAS.Play();
                break;
            case "gate":
                if (!gateAS.isPlaying)
                    gateAS.Play();
                break;
            case "flame":
                if (!flameAS.isPlaying)
                    flameAS.Play();
                break;
            case "lava":
                if (!lavaAS.isPlaying)
                {
                    lavaAS.clip = lavaClips[Random.Range(0, lavaClips.Length)];
                    lavaAS.Play();
                }
                break;
            case "swap":
                swapAS.Play();
                break;
            case "eshulaugh":
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
