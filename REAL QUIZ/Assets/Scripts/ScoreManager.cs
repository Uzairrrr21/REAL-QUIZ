using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import the TextMeshPro namespace to use TextMeshProUGUI

public class ScoreManager: MonoBehaviour
{
    public int score; // Variable to store the player's current score
    public TextMeshProUGUI scoreText; // Reference to the TextMesh ProUGUI component for displaying the score

    // Called when the script is first run
    void Start()
    {
        score= 0; // Initialize the score to e at the start of the game
        UpdateScoreText(); // Update the score display to show the initial score
    }

    // Adds points to the current score
    public void AddScore(int points)
    { 
        score += points; // Increase the score by the specified number of points
        UpdateScoreText(); // Update the score display to reflect the new score

    }

    // Subtracts points from the current score
    public void SubtractScore(int points)
    {
        score -= points; // Decrease the score by the specified number of points
        UpdateScoreText(); // Update the score display to reflect the new score
    }

    void UpdateScoreText()
    {
        scoreText.text = "" + score.ToString(); //update the textmeshprogui component with the current score
    }
}