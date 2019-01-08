using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayBehaviour : MonoBehaviour {

    MathOperations op;
    TextMeshProUGUI display;

    // Use this for initialization
    void Start () {
        op = this.GetComponent<MathOperations>();
        display = this.GetComponent<TextMeshProUGUI>();
    }
	
    public void GenerateExpression()
    {
        display.text = op.generateExpression();
    }
}
