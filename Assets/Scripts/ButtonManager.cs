using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void Maze1()
    {
        PlayerPrefs.SetInt("Maze", 1);
        SceneManager.LoadScene("Maze1");
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().StopMusic();
        Destroy(GameObject.FindGameObjectWithTag("Music"));
    }
    public void Maze2()
    {
        PlayerPrefs.SetInt("Maze", 2);
        SceneManager.LoadScene("Maze2");
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().StopMusic();
        Destroy(GameObject.FindGameObjectWithTag("Music"));
    }
    public void Controls()
    {
        SceneManager.LoadScene("Controls");
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().PlayMusic();
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().PlayMusic();
    }
    public void Bonus()
    {
        SceneManager.LoadScene("Bonus");
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().PlayMusic();
    }
    public void TitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
        //GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().PlayMusic();
        if (GameObject.FindGameObjectWithTag("MusicLevel1") != null)
        {
            GameObject.FindGameObjectWithTag("MusicLevel1").GetComponent<MusicManager>().StopMusic();
            Destroy(GameObject.FindGameObjectWithTag("MusicLevel1"));
        }
        if (GameObject.FindGameObjectWithTag("MusicLevel2") != null)
        {
            GameObject.FindGameObjectWithTag("MusicLevel2").GetComponent<MusicManager>().StopMusic();
            Destroy(GameObject.FindGameObjectWithTag("MusicLevel2"));
        }
    }
    public void MazeSelect()
    {
        SceneManager.LoadScene("MazeSelect");
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>().PlayMusic();
        if (GameObject.FindGameObjectWithTag("MusicLevel1") != null)
        {
            GameObject.FindGameObjectWithTag("MusicLevel1").GetComponent<MusicManager>().StopMusic();
            Destroy(GameObject.FindGameObjectWithTag("MusicLevel1"));
        }
        if (GameObject.FindGameObjectWithTag("MusicLevel2") != null)
        {
            GameObject.FindGameObjectWithTag("MusicLevel2").GetComponent<MusicManager>().StopMusic();
            Destroy(GameObject.FindGameObjectWithTag("MusicLevel2"));
        }
    }
    public void deletePlayerprefs()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("MazeSelect");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
