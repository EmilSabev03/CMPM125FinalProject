//This script is used to handle the trophy object seen in the game over scenes.
//You can adjust the move speed and spin speed of the trophies.
//This script also handles the play again functionality if the player wants to play again for a better time.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrophyController : MonoBehaviour
{
    public Transform trophy;         // Assign the trophy GameObject
    public float moveSpeed = 2f;     // Speed of the movement
    public float spinSpeed = 50f;    // Speed of the rotation
    public Vector3 targetPosition;  // The middle of the screen position
    public GameObject playAgainButton; // Reference to the button UI

    private bool hasReachedTarget = false;

    void Start()
    {
        // Hide the button at the start
        if (playAgainButton != null)
        {
            playAgainButton.SetActive(false);
        }

        // Ensure trophy starts off-screen at the bottom
        trophy.position = new Vector3(targetPosition.x, -5f, targetPosition.z); // Adjust as needed for your scene
    }

    void Update()
    {
        if (!hasReachedTarget)
        {
            // Move the trophy towards the target position
            trophy.position = Vector3.MoveTowards(trophy.position, targetPosition, moveSpeed * Time.deltaTime);

            // Check if the trophy has reached the target position
            if (Vector3.Distance(trophy.position, targetPosition) < 0.1f)
            {
                hasReachedTarget = true;

                // Show the "Play Again" button
                if (playAgainButton != null)
                {
                    playAgainButton.SetActive(true);
                }
            }
        }
        else
        {
            // Rotate the trophy
            trophy.Rotate(Vector3.up * spinSpeed * Time.deltaTime, Space.World);
        }
    }

    // Method to restart the game (assign this to the button's OnClick event)
    public void PlayAgain()
    {
        SceneManager.LoadScene("125save2");
    }
}

