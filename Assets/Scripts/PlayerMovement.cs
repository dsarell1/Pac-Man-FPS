using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Speed and Jump Variables")]
    [Space]
    public float speed = 1;
    public float jumpHeight = 300;
    private Rigidbody playerRigidbody;

    [Header("Health")]
    [Space]
    public int health = 7;

    [Header("Ground Check Stuff")]
    [Space]
    public Transform groundCheck;
    public LayerMask ground;
    private bool onGround;

    [Header("Camera Movement Variables")]
    [Space]
    public Camera playerCam;
    public float LookSpeed = 5.0f;
    private float upperLookLimit = 80;
    private float lowerLookLimit = 80;
    public float rotationX = 0;

    [Header("Text Variables")]
    [Space]
    public TextMeshProUGUI score;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI doorText;
    public TextMeshProUGUI objectiveText;
    public TextMeshProUGUI keyText;
    public TextMeshProUGUI highScoreText;
    public int scoreCount;

    [Header("Audio")]
    [Space]
    public AudioSource collectiveSF;
    public AudioSource levelMusic;
    public AudioSource gameOverMusic;
    public AudioSource bossMusic;
    public AudioSource victoryMusic;

    [Header("GameOverStuff")]
    [Space]
    public GameObject gameOverScreen;
    public bool gameOver = false;

    [Header("Enemies")]
    [Space]
    public GameObject pacManButlers;
    public GameObject masterPacman;
    public int pacmanKills = 0;

    [Header("Misc")]
    [Space]
    public GameObject door;
    public GameObject door2;
    public GameObject door3;
    public GameObject gun;
    public GameObject floor3;
    public GameObject stairs;
    public bool levelComplete = false;
    public bool levelComplete2 = false;

    Vector3 playerMoveInput;
    int floorNum = 1;
    int highScore, highScore2;
    int buttons = 0;
    bool playVictoryMusic = false;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("Maze1HighScore");
        highScore2 = PlayerPrefs.GetInt("Maze2HighScore");
        playerRigidbody = GetComponent<Rigidbody>();
        if (PlayerPrefs.GetInt("Maze") == 1)
            GameObject.FindGameObjectWithTag("MusicLevel1").GetComponent<MusicManager>().PlayMusic();
        else if (PlayerPrefs.GetInt("Maze") == 2)
            GameObject.FindGameObjectWithTag("MusicLevel2").GetComponent<MusicManager>().PlayMusic();
        masterPacman.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        setText();
        levelComplete = false;
        levelComplete2 = false;
        playVictoryMusic = false;
}
    void setText()
    {
        score.text = "Score: ";

        health = 7;
        healthText.text = "Life: " + health.ToString();

        doorText.text = "";
        objectiveText.text = "~ Collect all the Power Pellets ~";
        keyText.text = "";
        highScoreText.text = "";

        gameOverScreen.SetActive(false);
    }
    
    void Update()
    {

        if (gameOver || levelComplete || levelComplete2)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (levelComplete)
            {
                bossMusic.Pause();
                if (!playVictoryMusic)
                {
                    victoryMusic.Play();
                    playVictoryMusic = true;
                }
                StartCoroutine(finishLevel());
            }
            if (levelComplete2)
            {
                bossMusic.Pause();
                if (!playVictoryMusic)
                {
                    victoryMusic.Play();
                    playVictoryMusic = true;
                }
                StartCoroutine(finishLevel2());
            }
            
        }
        else if (!gameOver || !levelComplete || !levelComplete2)
        {
            playerMoveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            Vector3 move = transform.TransformDirection(playerMoveInput) * speed;
            playerRigidbody.velocity = new Vector3(move.x, playerRigidbody.velocity.y, move.z);

            onGround = Physics.CheckSphere(groundCheck.position, 0.3f, ground);

            if (Input.GetKeyDown(KeyCode.Space) && onGround)
            {
                playerRigidbody.AddForce(Vector3.up * jumpHeight);
            }

            // Mouse Movements
            rotationX -= Input.GetAxis("Mouse Y") * LookSpeed;
            rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
            playerCam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * LookSpeed, 0);

            gun.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                levelMusic.Stop();
                bossMusic.Stop();
                SceneManager.LoadScene("MazeSelect");
                GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().PlayMusic();
                if (PlayerPrefs.GetInt("Maze") == 1)
                {
                    GameObject.FindGameObjectWithTag("MusicLevel1").GetComponent<MusicManager>().StopMusic();
                    Destroy(GameObject.FindGameObjectWithTag("MusicLevel1"));
                }
                else if (PlayerPrefs.GetInt("Maze") == 2)
                {
                    GameObject.FindGameObjectWithTag("MusicLevel2").GetComponent<MusicManager>().StopMusic();
                    Destroy(GameObject.FindGameObjectWithTag("MusicLevel2"));
                }
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectible"))
        {
            collectiveSF.Play();
            other.gameObject.SetActive(false);
            scoreCount += 10;
            score.text = "Score: " + scoreCount.ToString();
            if (scoreCount == 160 && floorNum == 1)
            {
                StartCoroutine(showObjectiveText());
            }
        }
        if (other.gameObject.CompareTag("Collectible2"))
        {
            collectiveSF.Play();
            other.gameObject.SetActive(false);
            scoreCount += 10;
            score.text = "Score: " + scoreCount.ToString();
            if (scoreCount == 700 && floorNum == 1)
            {
                StartCoroutine(showObjectiveText2());
            }
        }
        if (other.gameObject.CompareTag("Button"))
        {
            if (scoreCount >= 160 && floorNum == 1)
            {
                door.SetActive(false);
                other.gameObject.SetActive(false);
                objectiveText.text = "";
                StartCoroutine(showDoorText());
            } 
            else if (pacmanKills >= 9 && floorNum == 2)
            {
                door2.SetActive(false);
                other.gameObject.SetActive(false);
                objectiveText.text = "";
                StartCoroutine(showDoorText());
            }
            else
            {
                StartCoroutine(showKeyText());
            }
        }
        if (other.gameObject.CompareTag("Level2Button"))
        {
            if (scoreCount >= 700 && floorNum == 1)
            {
                if (buttons == 1)
                {
                    buttons++;
                    door.SetActive(false);
                    door3.SetActive(false);
                    other.gameObject.SetActive(false);
                    objectiveText.text = "";
                    StartCoroutine(showDoorText());
                }
                else
                {
                    buttons++;
                    other.gameObject.SetActive(false);
                    StartCoroutine(showKeyText2());
                }
            }
            else if (pacmanKills >= 45 && floorNum == 2)
            {
                door2.SetActive(false);
                other.gameObject.SetActive(false);
                stairs.SetActive(true);
                objectiveText.text = "";
                StartCoroutine(showDoorText());
            }
            else
            {
                StartCoroutine(showKeyText());
            }
        }
        if (other.gameObject.CompareTag("Next Floor"))
        {
            other.gameObject.SetActive(false);
            if (floorNum == 1)
            {
                objectiveText.text = "~ Destroy the Pac-Man Butlers! ~";
                pacManButlers.SetActive(true);
                door.SetActive(true);
                if (PlayerPrefs.GetInt("Maze") == 2)
                    door3.SetActive(true);
                floorNum += 1;
            }
            else if (floorNum == 2)
            {
                objectiveText.text = "~ Defeat the Master Pac-Man! ~";
                door2.SetActive(true);
            }
        }
        
        if (other.gameObject.CompareTag("PacmanButler"))
        {
            health--;
            healthText.text = "Life: " + health.ToString();
            if (health == 0)
            {
                playerDeath();
            }
        }
        if (other.gameObject.CompareTag("Boss Floor"))
        {
            if (floorNum == 2)
            {
                floor3.SetActive(true);
                other.gameObject.SetActive(false);
                masterPacman.SetActive(true);
                if (PlayerPrefs.GetInt("Maze") == 1)
                    GameObject.FindGameObjectWithTag("MusicLevel1").GetComponent<MusicManager>().StopMusic();
                else if (PlayerPrefs.GetInt("Maze") == 2)
                    GameObject.FindGameObjectWithTag("MusicLevel2").GetComponent<MusicManager>().StopMusic();
                bossMusic.Play();
            }
        }
        if (other.gameObject.CompareTag("Boss"))
        {
            health -= 2;
            healthText.text = "Life: " + health.ToString();
            if (health == 0)
            {
                playerDeath();
            }
        }
        if (other.gameObject.CompareTag("Death Floor"))
        {
            playerDeath();
        }
    }

    void playerDeath()
    {
        if (PlayerPrefs.GetInt("Maze") == 1)
            GameObject.FindGameObjectWithTag("MusicLevel1").GetComponent<MusicManager>().StopMusic();
        else if (PlayerPrefs.GetInt("Maze") == 2)
            GameObject.FindGameObjectWithTag("MusicLevel2").GetComponent<MusicManager>().StopMusic();
        bossMusic.Pause();
        gameOverMusic.Play();
        gameOverScreen.SetActive(true);
        gameOver = true;
    }
    IEnumerator showDoorText()
    {
        doorText.text = "DOOR UNLOCKED";
        yield return new WaitForSeconds(3);
        doorText.text = "";
    }
    IEnumerator showKeyText()
    {
        keyText.text = "Objective Not Complete";
        yield return new WaitForSeconds(2);
        keyText.text = "";
    }
    IEnumerator showKeyText2()
    {
        keyText.text = "One Button Left";
        yield return new WaitForSeconds(2);
        keyText.text = "";
    }
    IEnumerator showObjectiveText()
    {
        objectiveText.text = "Objective Complete";
        yield return new WaitForSeconds(3);
        objectiveText.text = "Find the Switch to Unlock the Next Floor";
    }
    IEnumerator showObjectiveText2()
    {
        objectiveText.text = "Objective Complete";
        yield return new WaitForSeconds(3);
        objectiveText.text = "Find the Switches to Unlock the Next Floor";
    }
    IEnumerator finishLevel()
    {
        if (scoreCount > highScore)
        {
            PlayerPrefs.SetInt("Maze1HighScore", scoreCount);
            highScoreText.text = "New High Score: ";
            yield return new WaitForSeconds(4);
            highScoreText.text = "New High Score: " + scoreCount.ToString();
        }
        else if (scoreCount <= highScore)
        {
            highScoreText.text = "Score: ";
            yield return new WaitForSeconds(4);
            highScoreText.text = "Score: " + scoreCount.ToString();
        }
        yield return new WaitForSeconds(7);
        victoryMusic.Pause();
        SceneManager.LoadScene("MazeSelect");
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().PlayMusic();
        GameObject.FindGameObjectWithTag("MusicLevel1").GetComponent<MusicManager>().StopMusic();
        Destroy(GameObject.FindGameObjectWithTag("MusicLevel1"));
    }
    IEnumerator finishLevel2()
    {
        if (scoreCount > highScore2)
        {
            PlayerPrefs.SetInt("Maze2HighScore", scoreCount);
            highScoreText.text = "New High Score: ";
            yield return new WaitForSeconds(4);
            highScoreText.text = "New High Score: " + scoreCount.ToString();
        }
        else if (scoreCount <= highScore2)
        {
            highScoreText.text = "Score: ";
            yield return new WaitForSeconds(4);
            highScoreText.text = "Score: " + scoreCount.ToString();
        }
        yield return new WaitForSeconds(7);
        Destroy(levelMusic);
        victoryMusic.Pause();
        SceneManager.LoadScene("MazeSelect");
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().PlayMusic();
        GameObject.FindGameObjectWithTag("MusicLevel2").GetComponent<MusicManager>().StopMusic();
        Destroy(GameObject.FindGameObjectWithTag("MusicLevel2"));
    }
}
