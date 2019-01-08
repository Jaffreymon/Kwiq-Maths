using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Class to handle gameplay flow

public class GameBehaviour : MonoBehaviour {

    // UI elements in game
    [SerializeField]
    DisplayBehaviour display;
    [SerializeField]
    ChoiceManager choiceMngr;
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
    MathOperations op;

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

    public void recordMistake()
    {
        strikeImgs[totalMistakes++].gameObject.SetActive(true);
    }

    public bool isGameAlive()
    {
        return totalMistakes < MAX_MISTAKES;
    }

    public void gameOver()
    {
        toggleGameObject(gameOverScreen);
    }
}
