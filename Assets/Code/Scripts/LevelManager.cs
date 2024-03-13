using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    public Transform[] path;
    public Transform startPoint;
    private Camera mainCamera;
    public string weather;

    [Header("Attributes")]
    [SerializeField] public float diff_Factor = 0.75f;
    public int waveNum = 1;


    private void Awake()
    {
        main = this;
        mainCamera = Camera.main;

    }

    // Eventually, the colors with correlate with the weather patterns
    // Each time a color changes, the weather changes.
 

    public void ChangeWeather()
    {
        int randint = Random.Range(0, 3);
        if (randint == 0) {
            weather = "Clear";
            mainCamera.backgroundColor = Color.yellow;

        } else if (randint == 1) {
            weather = "Snowstorm";
            mainCamera.backgroundColor = Color.cyan;

        } else if (randint == 2) {
            weather = "Heatwave";
            mainCamera.backgroundColor = Color.red;

        } else if (randint == 3) {
            weather = "Rainstorm";
            mainCamera.backgroundColor = Color.blue;
        }
    }

}
