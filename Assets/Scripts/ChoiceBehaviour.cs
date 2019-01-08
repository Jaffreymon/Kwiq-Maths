using UnityEngine;
using TMPro;

public class ChoiceBehaviour : MonoBehaviour {

    public void compareAnswer()
    {
        GetComponentInParent<ChoiceManager>().compareAnswer(GetComponentInChildren<TextMeshProUGUI>());
    }
}
