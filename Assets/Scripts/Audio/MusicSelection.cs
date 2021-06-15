using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicSelection : MonoBehaviour
{
    [SerializeField] string scene;
    [SerializeField] AudioClip Level1;
    [SerializeField] AudioClip PortalLevel;
    [SerializeField] AudioClip MushroomLevel;
    [SerializeField] AudioClip Pirate;
    [SerializeField] AudioClip MermaidLevel;
    [SerializeField] AudioClip VolcanoLevel;
    [SerializeField] AudioClip FinalLevel;
    [SerializeField] AudioClip GameOver;
    [SerializeField] AudioClip YouWin;
    [SerializeField] AudioClip Menu;
    [SerializeField] AudioClip cine;
    [SerializeField] bool extend;

    private AudioSource audioSource;

    private static string currentSong = null;
    private static int finalCount = 0;
    
    public enum MusicState { Dead, ContinueLevel, BeforeMainLevelFinalTry };
    public static MusicState ms;

    private void Awake()
    {
        switch (scene)
        {
            case "Start":
                {
                    if (currentSong != "Start")
                    {
                        currentSong = "Start";
                        MusicClass.instance.PlayMusic(Level1);
                    }
                }
                break;

            case "StartExtended":
                {
                    if(currentSong == "Menu")
                    {
                        currentSong = "Start";
                        MusicClass.instance.PlayMusic(Level1);
                    }
                }
                break;

            case "Portal":
                {
                    if (currentSong != "Portal")
                    {
                        currentSong = "Portal";
                        MusicClass.instance.PlayMusic(PortalLevel);
                    }
                }
                break;

            case "Mushroom":
                {
                    if (currentSong != "Mushroom")
                    {
                        currentSong = "Mushroom";
                        MusicClass.instance.PlayMusic(MushroomLevel);
                    }  
                }
                break;

            case "Pirate":
                {
                    if (currentSong != "Pirate")
                    {
                        currentSong = "Pirate";
                        MusicClass.instance.PlayMusic(Pirate);
                    }
                }
                break;

            case "Mermaid":
                {
                    if (currentSong != "Mermaid")
                    {
                        currentSong = "Mermaid";
                        MusicClass.instance.PlayMusic(MermaidLevel);
                    }
                }
                break;

            case "Volcano":
                {
                    if (currentSong != "Volcano")
                    {
                        MusicClass.instance.PlayMusic(VolcanoLevel);
                        currentSong = "Volcano";
                    }
                }
                break;

            case "VolcanoExtended":
                {
                    if (currentSong == "Menu")
                    {
                        currentSong = "Volcano";
                        MusicClass.instance.PlayMusic(VolcanoLevel);
                    }
                }
                break;

            case "Final":
                {
                    if (currentSong != "Final")
                    {
                        MusicClass.instance.PlayMusic(FinalLevel);
                    }
                    
                    currentSong = "Final";
                }
                break;

            case "Cinesyn":
                {
                    print("here");
                    MusicClass.instance.PlayMusic(cine);
                    print("here");
                }
                break;

            case "Game Over":
                {
                    MusicClass.instance.PlayMusic(GameOver);
                    currentSong = "GameOver";
                }
                break;

            case "You win!":
                {
                    MusicClass.instance.PlayMusic(YouWin);
                }
                    break;

            case "Menu":
                {

                    if (currentSong != "Menu")
                    {
                        currentSong = "Menu";
                        MusicClass.instance.PlayMusic(Menu);
                    }                     
                }
                break;
        }
    }
}
