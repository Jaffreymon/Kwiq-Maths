using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// Class to handle gameplay flow

public class GameBehaviour : MonoBehaviour {

    // Game behaviours
    [SerializeField]
    DisplayBehaviour display;
    [SerializeField]
    ChoiceManager choiceMngr;
    MathOperations op;

    // UI elements in game
    [SerializeField]
    GameObject startBtn;
    [SerializeField]
    GameObject scoreText;
    [SerializeField]
    GameObject strikeText;
    [SerializeField]
    GameObject highscoreText;
    [SerializeField]
    GameObject[] strikeImgs;
    [SerializeField]
    GameObject gameOverScreen;

    // Tracks total correct choices
    static int totalScore = 0;
    // Tracks high score
    static int highScore;

    // Tracks total mistakes
    static int totalMistakes = 0;
    // Total mistakes tolerated
    const int MAX_MISTAKES = 3;

    // Initialize at the start
    private void Start()
    {
        op = GetComponent<MathOperations>();
        highScore = PlayerPrefs.GetInt("highscore", 0); // Gets the highscore to display on start screen

        displayHighscore(); // Initializes highscore
    }

    // Gets the title/mathematical expressions UI
    public DisplayBehaviour getDisplay()
    {
        return display;
    }

    // Gets the choice manager element
    public ChoiceManager getChoiceManager()
    {
        return choiceMngr;
    }

    // Toggles appropriate game elements and starts the game
    public void initGame()
    {
        totalScore = 50;
        toggleGameObject(choiceMngr.gameObject);    // Reveal answer choice boxes
        toggleGameObject(startBtn);                 // Hide start button
        toggleGameObject(scoreText);                // Reveal score UI
        toggleGameObject(strikeText);               // Reveal score UI
        displayScore();         // Initializes score
        startRound();           // Initializes first math expression
    }

    // Increments score and updates UI
    public void addScore()
    {
        totalScore++;
        displayScore();
    }

    // Updates score UI 
    public void displayScore()
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = string.Format("Score: {0}", totalScore);
    }

    // Updates highscore UI
    public void displayHighscore()
    {
        highscoreText.GetComponent<TextMeshProUGUI>().text = string.Format("High Score: {0}", highScore);
    }

    // Updates highscore data if current score is higher
    public void setHighscore()
    {
        if (totalScore > highScore)
        {
            highScore = totalScore;
            PlayerPrefs.SetInt("highscore", highScore);
            displayHighscore(); // Display new highscore if old highscore broken
        }
    }

    // Toggles a game object's active status
    private static void toggleGameObject(GameObject go)
    {
        go.SetActive(!go.activeSelf);
    }

    // Generates a new math expression and updates UI
    // Updates answer choices UI
    public void startRound()
    {        
        op.increaseUpperBound(totalScore);
        display.GenerateExpression();
        choiceMngr.setChoices();
    }

    // Generates a mathematical expression
    public MathOperations getMaths()
    {
        return op;
    }

    // Increments the total incorrect answers made
    public void recordMistake()
    {
        strikeImgs[totalMistakes++].gameObject.SetActive(true);
    }

    // Checks if the total mistakes meets the max tolerated
    public bool isGameAlive()
    {
        return totalMistakes < MAX_MISTAKES;
    }

    // Enables the gameover screen over game UI
    public void gameOver()
    {
        toggleGameObject(gameOverScreen);
    }

    // Reloads the game's scene
    public void resetScene()
    {
        totalMistakes = 0;
        totalScore = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Deletes highscore of current scene
    // TODO Delete when not testing
    private void OnApplicationQuit()
    {
        //PlayerPrefs.DeleteKey("highscore");
    }
}
