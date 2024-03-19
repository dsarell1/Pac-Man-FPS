using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MazeSelectHS : MonoBehaviour
{
    public TextMeshProUGUI maze1HighScore;
    public TextMeshProUGUI maze2HighScore;
    // Start is called before the first frame update
    void Start()
    {
        maze1HighScore.text = "Beginner Maze HighScore: " + PlayerPrefs.GetInt("Maze1HighScore");
        maze2HighScore.text = "Expert Maze HighScore: " + PlayerPrefs.GetInt("Maze2HighScore");
    }
}
