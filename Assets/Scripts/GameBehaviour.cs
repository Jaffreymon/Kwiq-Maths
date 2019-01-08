using UnityEngine;

public class GameBehaviour : MonoBehaviour {

    [SerializeField]
    DisplayBehaviour display;
    [SerializeField]
    ChoiceManager choiceMngr;
    [SerializeField]
    GameObject startBtn;
    [SerializeField]
    GameObject scoreText;

    public DisplayBehaviour getDisplay()
    {
        return display;
    }

    public ChoiceManager getChoiceManager()
    {
        return choiceMngr;
    }

    public void initGame()
    {
        toggleGameObject(choiceMngr.gameObject);
        toggleGameObject(startBtn);
        toggleGameObject(scoreText);
        startRound();
    }

    private void toggleGameObject(GameObject go)
    {
        go.SetActive(!go.activeSelf);
    }

    public void startRound()
    {
        display.GenerateExpression();
        choiceMngr.setChoices();
    }
}
