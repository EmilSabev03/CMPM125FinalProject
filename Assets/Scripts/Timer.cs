//This script handles and manages the timer UI which is displayed on screen.
//We also use this timer script to handle the gameover collision object. Depending on the time the player gets, it switches to a scene with respect to the player's time.

//This youtube video was used to help create this script: https://www.youtube.com/watch?v=POq1i8FyRyQ&t=61s

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI timeElapsedText;
    float timeElapsed;
    void Update()
    {
        timeElapsed += Time.deltaTime;

        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);

        timeElapsedText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Car") && timeElapsed <= 90)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        else if (collision.gameObject.CompareTag("Car") && timeElapsed > 90 && timeElapsed <= 120)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }

        else if (collision.gameObject.CompareTag("Car") && timeElapsed > 120)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
        }
        
    }
}
