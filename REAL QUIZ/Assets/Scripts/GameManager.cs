using UnityEngine;
using TMPro; // Import the TextMeshPro namespace
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Array to hold all categories (each category is a different QuestionData Scriptable Object)
    public QuestionData[] categories;

    // Reference to the selected category's QuestionData Scriptable Object
    private QuestionData selectedCategory;

    // Index to track the current question within the selected Category
    private int currentQuestionIndex = 0;

    // UI elements to display the question text, image, and reply buttons
    public TMP_Text questionText; // Use TMP_Text for TextMesh
    public Image questionImage;
    public Button [] replyButtons;

    [Header("Score")]
    public ScoreManager score;
    public int correctReply = 10;
    public int wrongReply = 5;
    public TextMeshPro scoreText;

    [Header("correctReplyIndex")]
    public int correctReplyIndex;
    int correctReplies;

    [Header("Game Finished Panel")]
    public GameObject GameFinished;
    void Start ()
    {
        int selectedCategoryIndex = PlayerPrefs.GetInt("SelectedCategory", 0);
        GameFinished.SetActive(false);
        SelectCategory(selectedCategoryIndex);
    }
    // Method to select a category based on the player's choice
    // categoryIndex: the index of the category selected by the player
    public void SelectCategory(int categoryIndex)
    {
    // Set the selectedCategory to the chosen category's QuestionData Scriptable Object
    selectedCategory = categories[categoryIndex];

    // Reset the current question index to start from the first question in the selected category 
    currentQuestionIndex = 0;

    // Display the first question in the selected category 
    DisplayQuestion();
    }

    // Method to display the current question 
    public void DisplayQuestion()
    {
        // Check if a category has been selected 
        if (selectedCategory == null) return;

        ResetButtons();

        // Get the current question from the selected category 
        var question = selectedCategory.questions[currentQuestionIndex];
        // Set the question image in the UI (if any)
        if (question.questionImage != null)
        {
            questionImage.sprite = question.questionImage;
            questionImage.gameObject.SetActive(true);
        }
        else
        {
            questionImage.gameObject.SetActive(false);
        }

        // Set the question text in the UI 
        questionText.text = question.questionText;

        // Set the question image in the UI (if any) 
        questionImage.sprite = question.questionImage;

        // Loop through all reply buttons and set their text to the corresponding replies
        for (int i = 0; i < replyButtons.Length; i++)
        {
            TMP_Text buttonText = replyButtons[i].GetComponentInChildren<TMP_Text>();
            buttonText.text = i < question.replies.Length ? question.replies[i] : "";
            replyButtons[i].gameObject.SetActive(i < question.replies.Length); // Disable extra buttons
        }
    }

    // Method to handle when a player selects a reply

    public void OnReplySelected(int replyIndex)
    {
        // Check if the selected reply is correct
        if (replyIndex == selectedCategory.questions[currentQuestionIndex].correctReplyIndex)
        {
            score.AddScore(correctReply);
            correctReplies++;
            Debug.Log("Correct Reply!");
        }
        else
        {
            score.SubtractScore(wrongReply);
            Debug.Log("Wrong Reply!");
        }

        // Proceed to the next question or end the quiz if all questions are replied
        currentQuestionIndex++;
        Debug.Log($"Current Question Index: {currentQuestionIndex}/{selectedCategory.questions.Length}");
        if (currentQuestionIndex < selectedCategory.questions.Length)
        { 
            DisplayQuestion(); // Display the next question
        }
        else
        {
            ShowGameFinishedPanel();
            Debug.Log("Quiz Finished!");
            // Implement what happens when the quiz is finished (e.g., show results, restart, etc.)
        }
    }

    //Call this method when you want to show the correct reply
    public void ShowCorrectReply()
    {
        correctReplyIndex = selectedCategory.questions[currentQuestionIndex].correctReplyIndex;

        // Loop through all buttons
        for (int i = 0; i < replyButtons.Length; i++)
        {
            if (i == correctReplyIndex)
            {
                replyButtons[i].interactable = true; // Show the correct button
            }
            else
            {
                replyButtons[i].interactable = false; // Hide the incorrect buttons
            }
        }       
    }

    public void ResetButtons()
    {
        foreach (var button in replyButtons)
        {
            button.interactable = true;
        }
    }

    public void ShowGameFinishedPanel()
    {
        GameFinished.SetActive(true);
        scoreText.text = "" + correctReplies + " / " + selectedCategory.questions.Length;
    }
}
