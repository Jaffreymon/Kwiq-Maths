using UnityEngine;
using TMPro;

// Class to Update UI of math expression to display

public class DisplayBehaviour : MonoBehaviour {

    // UI and game elements
    MathOperations op;
    TextMeshProUGUI display;

    // Use this for initialization
    void Start () {
        op = this.GetComponent<MathOperations>();
        display = this.GetComponent<TextMeshProUGUI>();
    }
	
    // Updates the math expression to be solved
    public void GenerateExpression()
    {
        display.text = op.generateExpression(); // Uses MathOperations to generate math expression
    }
}
