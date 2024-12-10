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

    void Start ()
    {
        SelectCategory(0);
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

        // Get the current question from the selected category 
        var question = selectedCategory.questions[currentQuestionIndex];

        // Set the question text in the UI 
        questionText.text = question.questionText;

        // Set the question image in the UI (if any) 
        questionImage.sprite = question.questionImage;

        // Loop through all reply buttons and set their text to the corresponding replies
        for (int i=0; i < replyButtons.Length; i++)
        {
        // Use TextMeshPro component for reply buttons
            TMP_Text buttonText = replyButtons[i].GetComponentInChildren<TMP_Text>();
            buttonText.text = question.replies[i];
        }
    }

    // Method to handle when a player selects a reply

    public void OnReplySelected(int replyIndex)
    {
        // Check if the selected reply is correct
        if (replyIndex == selectedCategory.questions[currentQuestionIndex].correctReplyIndex)
        {
            Debug.Log("Correct Reply!");
        }
        else
        {
            Debug.Log("Wrong Reply!");
        }

        // Proceed to the next question or end the quiz if all questions are replied
        currentQuestionIndex++;
        if (currentQuestionIndex < selectedCategory.questions.Length)
        { 
            DisplayQuestion(); // Display the next question
        }
        else
        {
            Debug.Log("Quiz Finished!");
            // Implement what happens when the quiz is finished (e.g., show results, restart, etc.)
        }
    }
}
