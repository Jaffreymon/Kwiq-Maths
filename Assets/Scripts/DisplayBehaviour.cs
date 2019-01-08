using UnityEngine;
using TMPro;

// Class to Update UI of math expression to display

public class DisplayBehaviour : MonoBehaviour {

    // UI and game elements
    [SerializeField]
    GameBehaviour gm;
    TextMeshProUGUI display;

    // Use this for initialization
    void Start () {
        display = this.GetComponent<TextMeshProUGUI>();
    }
	
    // Updates the math expression to be solved
    public void GenerateExpression()
    {
        display.text = gm.getMaths().generateExpression(); // Uses MathOperations to generate math expression
    }
}
