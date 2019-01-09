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
    Image[] strikeImgs;
    [SerializeField]
    GameObject gameOverScreen;

    // Tracks total correct choices
    static int totalScore = 0;

    // Tracks total mistakes
    static int totalMistakes = 0;
    // Total mistakes tolerated
    const int MAX_MISTAKES = 3;

    // Initialize at the start
    private void Start()
    {
        op = GetComponent<MathOperations>();
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
        toggleGameObject(choiceMngr.gameObject);    // Reveal answer choice boxes
        toggleGameObject(startBtn);                 // Hide start button
        toggleGameObject(scoreText);                // Reveal score UI
        toggleGameObject(strikeText);               // Reveal score UI
        setScore();     // Initializes score
        startRound();   // Initializes first math expression
    }

    // Increments score and updates UI
    public void addScore()
    {
        totalScore++;
        setScore();
    }

    // Updates score UI 
    public void setScore()
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = string.Format("Score: {0}", totalScore);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
