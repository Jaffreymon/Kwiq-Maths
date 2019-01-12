using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

// Class to handle gameplay flow

public class GameBehaviour : MonoBehaviour {

    // Firebase Components
    const string url = "https://kwiq-maths-388e4.firebaseio.com/";
    DatabaseReference dbRef;

    // Game behaviours
    [SerializeField]
    DisplayBehaviour display;
    [SerializeField]
    ChoiceManager choiceMngr;
    [SerializeField]
    TimerBehaviour timer;
    MathOperations op;


    // Audio Components
    [SerializeField]
    AudioClip[] SFX;
    // Index 0: Correct audio sound, Index 1: Wrong audio sound
    const int CORRECT_DING = 0;
    const int INCORRECT_DING = 1;
    AudioSource source;


    // UI elements in game to edit
    [SerializeField]
    GameObject scoreText;
    [SerializeField]
    GameObject highscoreText;
    [SerializeField]
    GameObject[] strikeImgs;


    // GameHolder elements in game to toggle
    [SerializeField]
    GameObject startMenuHolder;
    [SerializeField]
    GameObject activeGameHolder;
    [SerializeField]
    GameObject gameOverHolder;

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
        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(url);
        dbRef = FirebaseDatabase.DefaultInstance.GetReference("Highscores");

        PlayerPrefs.DeleteKey("highscore");

        op = GetComponent<MathOperations>();
        source = GetComponent<AudioSource>();
        highScore = PlayerPrefs.GetInt("highscore", 0); // Gets the highscore to display on start screen

        displayHighscore(); // Initializes highscore
    }

    // Handles the timer
    private void Update()
    {
        if (timer.getTimerStatus() && timer.getTimeLeft() <= 0f)
        {
            choiceMngr.compareAnswer(null);
        }
    }

    #region Getters
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

    // Generates a mathematical expression
    public MathOperations getMaths()
    {
        return op;
    }
    #endregion

    // Toggles appropriate game elements and starts the game
    public void initGame()
    {
        toggleGameObject(startMenuHolder);  // Disables start menu elements
        toggleGameObject(activeGameHolder); // Enables active game elements
        displayScore();         // Initializes score
        startRound();           // Initializes first math expression
    }

    // Toggles a game object's active status
    private static void toggleGameObject(GameObject go)
    {
        go.SetActive(!go.activeSelf);
    }

    #region Score related functions
    // Increments score and updates UI
    public void addScore()
    {
        totalScore++;
        source.clip = SFX[CORRECT_DING];
        source.Play();
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

            pushScoreToDB();
        }
    }

    // Increments the total incorrect answers made
    public void recordMistake()
    {
        strikeImgs[totalMistakes++].gameObject.SetActive(true);
        source.clip = SFX[INCORRECT_DING];
        source.Play();
    }

    #endregion

    #region Gameplay Logistics

    // Generates a new math expression and updates UI
    // Updates answer choices UI
    public void startRound()
    {        
        op.increaseUpperBound(totalScore);
        display.GenerateExpression();
        choiceMngr.setChoices();
        timer.resetTimer();
    }

    // Checks if the total mistakes meets the max tolerated
    public bool isGameAlive()
    {
        return totalMistakes < MAX_MISTAKES;
    }

    // Enables the gameover screen over game UI
    public void gameOver()
    {
        toggleGameObject(gameOverHolder);   // Enables gameover elements
    }

    // Reloads the game's scene
    public void resetScene()
    {
        totalMistakes = 0;
        totalScore = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    #endregion

    #region Slider functions

    public void statusTimer(bool status)
    {
        timer.setTimerStatus(status);
    }

    #endregion

    #region Firebase functions
    private void debugPrintList()
    {
        // Saves high score to firebase
        dbRef.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Failed to fetch data");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snap = task.Result;
                
            }
        });
    }

    private void pushScoreToDB()
    {
        // Creates score entry in json format
        LeaderboardEntry newEntry = new LeaderboardEntry("test", highScore);
        string json = JsonUtility.ToJson(newEntry);

        // Sets the value of the highscore
        dbRef.Child(SystemInfo.deviceUniqueIdentifier).SetRawJsonValueAsync(json);
    }
    #endregion
}

class LeaderboardEntry
{
    public string username;
    public int score;

    public LeaderboardEntry(string _uid, int _score)
    {
        username = _uid;
        score = _score;
    }
}