using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float tempThrust;

    [SerializeField] float levelLoadDelay = 2f;

    [SerializeField] GameObject audioManager;
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject[] colliders;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip portalSound;
    [SerializeField] AudioClip enterPortalSound;
    [SerializeField] AudioClip finalPortalSound;
    [SerializeField] AudioClip incinerateSound;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip takeOrb;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem convergentEffect;
    [SerializeField] ParticleSystem plasmaExplosion;
    [SerializeField] ParticleSystem turnOn;
    [SerializeField] ParticleSystem[] endGameEffects;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshPro scoreBump;

    [SerializeField] float portalExpulsion;
    [SerializeField] bool waitForPortal = false;
    [SerializeField] float portalTime;

    [SerializeField] bool lockMovement;
    [SerializeField] int movementLockTime;
    [SerializeField] Vector3 effectVector;

    [SerializeField] GameObject whale;
    [SerializeField] GameObject IgnoreNarwhal;

    [SerializeField] bool playTurn = false;
    [SerializeField] bool secondTime;

    [SerializeField] GameObject dissolveObject;
    [SerializeField] bool dissolveLevel;
    [SerializeField] float dissolveEveryoneTime;

    Rigidbody rigidBody;
    AudioSource audioSource;
    SoundEffectManager soundEffect;

    public enum State { Alive, Dying, Transcending, Waiting, Portaling, Releveling }
    State state = State.Alive;

    private static Material robotColor;

    Queue<TextMeshPro> scoreQueue;
    List<TextMeshPro> scoreIncrease;
    private int tempScore = 0;

    private int deathCount = 0;
    int dissolveCount = 0;
    public static int playTry = 0;
    bool checkedPlayTry = false;

    private int i = 0;
   
    bool collisionsDisabled = false;

    private float portalWait = 0;

    // Start is called before the first frame update
    void Start()
    {
        print(robotColor);
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        if (audioManager != null)
        {
            soundEffect = audioManager.GetComponent<SoundEffectManager>();
        }

        //score = GetComponent<Score>();

        if (SceneManager.GetActiveScene().buildIndex != 5 && SceneManager.GetActiveScene().buildIndex != 17)
        {
            scoreText.gameObject.GetComponent<TextMeshProUGUI>().SetText("Saved: " + Score.getScore());
            livesText.gameObject.GetComponent<TextMeshProUGUI>().SetText("Lives: " + (4 - PlayerStats.GetLevelTry()));
        }
        else if (SceneManager.GetActiveScene().buildIndex != 17)
        {
            livesText.gameObject.GetComponent<TextMeshProUGUI>().SetText("Lives: " + (4 - PlayerStats.GetLevelTry()));
        }

        scoreBump.gameObject.SetActive(false);

        scoreQueue = new Queue<TextMeshPro>();

        scoreIncrease = new List<TextMeshPro>();

        while (scoreIncrease.Count > 0)
        {
            for (int i = 0; i< scoreIncrease.Count; ++i)
            {
                scoreIncrease[i].transform.localScale *= 2f;
            }
        }

        if (PlayerStats.GetLevelTry() == 1)
        {
            MusicSelection.ms = MusicSelection.MusicState.BeforeMainLevelFinalTry;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playTry == 0 && !checkedPlayTry && playTurn && !secondTime)
        {
            IgnoreNarwhal.gameObject.SetActive(false);
            checkedPlayTry = true;
            Invoke("AppearButton", 6);
        }
        if (playTry == 0 && !checkedPlayTry && playTurn && secondTime)
        {
            IgnoreNarwhal.gameObject.SetActive(false);
            checkedPlayTry = true;
            Invoke("movementLock", 4);
        }

        if (lockMovement)
        {
            lockMovement = false;
            movementLock();
        }

        if (waitForPortal && portalWait == 0f)
        {
            gameObject.SetActive(false);
            ++portalWait;
            Invoke("ActivateShip", portalTime);
        }

        if (dissolveLevel && dissolveCount < 1)
        {
            ++dissolveCount;
            Dissolution();
        }

        if (state == State.Alive)
        {
            ProcessInput();
        }

        if (Debug.isDebugBuild)
        {
            ResondToDebugKeys();
        }
    }

    private void ResondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextScene();
            PlayerStats.ResetLevelTry();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionsDisabled = !collisionsDisabled;
 
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive || collisionsDisabled) { return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":               
                print("Ok");
                break;

            case "Finish":
                StartSuccessSequence();
                break;
            case "ExitPortal":
                PortalSequence();
                break;

            case "Convergent Portal":
   
                Convergence();
                break;

            default:
                print("Death");
                StartDeathSequence(collision);
                break;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        print(other.tag);
        print(other.tag == "Orb");
        if (other.tag == "Orb")
        {
            print("here");
            other.gameObject.GetComponent<Orb>().deathStart();
            if (!soundEffect.isPlaying())
            {
                soundEffect.playAudioClip(takeOrb, 1);
            }
            
            if (!other.gameObject.GetComponent<Orb>().PointsAssigned() && state == State.Alive)
            {
                Score.updateScore(other.gameObject.GetComponent<Orb>().getBonus());
                tempScore += other.gameObject.GetComponent<Orb>().getBonus();


                scoreBump.gameObject.SetActive(true);
            
                scoreBump.SetText("+" + other.gameObject.GetComponent<Orb>().getBonus());
                scoreBump.transform.position = other.transform.position;
                //scoreBump.transform.localScale = other.transform.localScale / 4;
                scoreQueue.Enqueue(scoreBump);
                scoreIncrease.Add(scoreBump);
                Invoke("ScoreGone", 1f);
               

                scoreText.gameObject.GetComponent<TextMeshProUGUI>().SetText("Saved: " + Score.getScore()); 
            }
            
            print(Score.getScore());
        }
        else
        {
            StartDeathSequence(other.GetComponent<Collision>());
        }
    }

    private void StartDeathSequence(Collision collision)
    {
        print(deathCount);
        if (deathCount < 1)
        {
            Score.updateScore(-tempScore);
            tempScore = 0;
            print("detecting collision");
            if (collision.gameObject.name == "Bip001 Prop1")
            {
                colliders[0].GetComponent<Animation>().Play("victory");
                audioSource.Stop();
                soundEffect.playAudioClip(death, .5f);
                deathParticles.Play();
            }
            else if (collision.gameObject.tag == "Portal")
            {
                print("portal");
                plasmaExplosion.Play();
                audioSource.Stop();
                soundEffect.playAudioClip(incinerateSound);
            }
            else
            {
                soundEffect.playAudioClip(death, .5f);
                deathParticles.Play();
            }

            if (PlayerStats.GetLevelTry() < 3)
            {
                PlayerStats.UpdateLevelTry();
                livesText.gameObject.GetComponent<TextMeshProUGUI>().SetText("Lives: " + (4 - PlayerStats.GetLevelTry()));
               
                ++playTry;
                state = State.Releveling;

                Invoke("ReloadScene", levelLoadDelay);
            }
            else if (PlayerStats.GetLevelTry() >= 3)
            {

                PlayerStats.UpdateLevelTry();
                livesText.gameObject.GetComponent<TextMeshProUGUI>().SetText("Lives: " + (4 - PlayerStats.GetLevelTry()));
                PlayerStats.ResetLevelTry();

                if (PlayerStats.GetPractice())
                {   
                    Invoke("GameOver", levelLoadDelay);
                }
                else
                {
                    Invoke("GameOverReal", levelLoadDelay);
                }

                state = State.Dying;
            }

            ++deathCount;
        }
    }

    private void StartSuccessSequence()
    {
        if (state != State.Dying && state != State.Releveling)
        {
            state = State.Transcending;
            MusicClass.continueSong = true;
            audioSource.Stop();
            audioManager.GetComponent<SoundEffectManager>().playAudioClip(success);
            successParticles.transform.position = gameObject.transform.position;
            successParticles.Play();

            PlayerStats.ResetLevelTry();
            Invoke("LoadNextScene", levelLoadDelay);
            Invoke("DissapearObject", .5f);
        }
    }

    private void ProcessInput()
    {
        RespondToThrustInput();
        RespondtoRotateInput();
    }

    private void RespondToThrustInput()
    {
        
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
         
            mainEngineParticles.Stop();
        }
    }

    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime); //can thrust while rotating
        if (!audioSource.isPlaying )
        {
            audioSource.PlayOneShot(mainEngine, .75f);
        }
   
        if (turnOn != null)
        {
            turnOn.Stop();
        }
        
        if (!mainEngineParticles.isEmitting)
        {
            mainEngineParticles.Play();
        }
    }

    private void ApplyThrust(float thrustVar)
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust * thrustVar * Time.deltaTime); //can thrust while rotating
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine, .75f);
        }

        if (turnOn != null)
        {
            turnOn.Stop();
        }

        mainEngineParticles.Play();
    }

    private void RespondtoRotateInput()
    {
       rigidBody.freezeRotation = true; // take manual of rotation 

        float rotationThisFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.left * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.left * rotationThisFrame);
        }

       rigidBody.freezeRotation = false; // resume physics control of rotation
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    private void GameOver()
    {
        print("game Over");
        MusicClass.continueSong = false;

        int lastScene = SceneManager.sceneCountInBuildSettings - 2;
        SceneManager.LoadScene(lastScene);
        ++playTry;
    }

    public State getState()
    {
        return this.state;
    }

    public void PortalSequence()
    {
        PlayerStats.ResetLevelTry();
        state = State.Portaling;
        MusicClass.continueSong = false;

        plasmaExplosion.transform.position = transform.position;
        gameObject.SetActive(false);

        plasmaExplosion.Play();
        
        soundEffect.playAudioClip(enterPortalSound);
        Invoke("LoadNextScene", 2);
    }

    public void ActivateShip()
    {
        gameObject.SetActive(true);

        plasmaExplosion.transform.position = gameObject.transform.position + gameObject.transform.localScale.magnitude * effectVector;
        plasmaExplosion.Play();
        ApplyThrust(portalExpulsion);
        audioSource.Stop();
        //audioSource.PlayOneShot(portalSound, 1);
        soundEffect.playAudioClip(portalSound);

        if (colliders.Length > 0)
        {
            colliders[0].transform.Rotate(0, 180, 0);
            colliders[0].GetComponent<Animation>().Play("atk01");
        }
    }

    private void movementLock()
    {
        print("movement lock");
        state = State.Waiting;

        Invoke("unlockMovement", movementLockTime);
    }

    private void unlockMovement()
    {
        print("unlock movement");
        state = State.Alive;
        IgnoreNarwhal.gameObject.SetActive(false);
    }
    
    public void ignoreDialog()
    {
        ApplyThrust(tempThrust);
        turnOn.Play();
        
        CancelInvoke();
        unlockMovement();
    }

    private void Convergence()
    {
        convergentEffect.transform.position = transform.position;
        convergentEffect.Play();
        MusicClass.continueSong = false;
        soundEffect.playAudioClip(finalPortalSound);
        Invoke("LoadNextScene", 2);
        gameObject.SetActive(false);
   
        //if (i < endGameEffects.Length)
        //{
        //    endGameEffects[i].Play();
        //    Invoke("Convergence", .5f);
        //    ++i;
        //}
    }

    private void ScoreGone()
    {
        TextMeshPro currentItem = scoreQueue.Dequeue();
        scoreIncrease.RemoveAt(0);
        currentItem.gameObject.SetActive(false);
    }

    private void DissapearObject()
    {
        gameObject.SetActive(false);
    }

    private void Dissolution()
    {
        Invoke("DissolveEveryone", dissolveEveryoneTime);
    }

    private void DissolveEveryone()
    {
        dissolveObject.GetComponent<SpawnEnable>().DissolveOperator();
        
        PlayerStats.WriteString();
        Invoke("WinCondition", 12);

    }

    public static void updateColor(Material mat)
    {
        robotColor = mat;
    }

    public Material getColor()
    {
        return robotColor;
    }

    public void ReloadScene()
    {
        MusicClass.continueSong = true;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void AppearButton()
    {
        IgnoreNarwhal.gameObject.SetActive(true);
    }

    private void GameOverReal()
    {
        PlayerStats.WriteString();
        int pointScene = SceneManager.sceneCountInBuildSettings - 2;
        MusicClass.continueSong = false;
        SceneManager.LoadSceneAsync(pointScene);
        ++playTry;
    }

    private void WinCondition()
    {
        PlayerStats.ResetLevelTry();
      
        MusicClass.continueSong = false;
        int pointScene = SceneManager.sceneCountInBuildSettings - 3;
        SceneManager.LoadScene(pointScene);
        ++playTry;
    }
}
