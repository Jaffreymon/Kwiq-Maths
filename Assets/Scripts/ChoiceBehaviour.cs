using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(DisplayBehaviour))]
public class ChoiceBehaviour : MonoBehaviour {

    GameBehaviour gameBehaviour;

    private void Start()
    {
        gameBehaviour = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameBehaviour>();
    }

    public void compareAnswer()
    {
        GetComponentInParent<ChoiceManager>().compareAnswer(GetComponentInChildren<TextMeshProUGUI>());
        StartCoroutine(PrepareNextRound());
    }

    IEnumerator PrepareNextRound()
    {
        yield return new WaitForSecondsRealtime(1);
        gameBehaviour.startRound();
    }
}
