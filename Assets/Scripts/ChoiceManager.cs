using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Class to handle user's answer choice to current expression

public class ChoiceManager : MonoBehaviour {

    // UI and game elements
    [SerializeField]
    Button[] choices;
    [SerializeField]
    MathOperations op;
    [SerializeField]
    GameBehaviour gameBehaviour;

    // Range an incorrect answer can deviate from the correct value
    int resultVariance = 5;

    // Generates math expression to be solved
    public void createChoices()
    {
        op.generateExpression();
    }

    // Generates incorrect answers; displays possible answer choices when an expression is generated
	public void setChoices()
    {
        int correctIdx = Random.Range(0,choices.Length-1);  // Chooses which choice box will contain correct answer
        float result = op.getResult();                      // Gets the correct answer to current expression
        string correctAns = string.Format("{0}", result);   // Sets correct answer to a choice box

        string incorrectAns;    // Declares incorrect value string
        List<string> used = new List<string>(); // Declares a stash of used values

        // Loop through all button choices
        for (int idx = 0; idx < choices.Length; idx++)
        {
            choices[idx].GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
            // Give incorrect choice boxes an incorrect value
            if (idx != correctIdx)
            {
                // Loop until a unique value is generated
                #region // False Answer Generator
                do
                {
                    int randomDisplacement = Random.Range(1, resultVariance);    
                    randomDisplacement *= (Random.value > 0.5) ? -1 : 1;                // Randomly decides to subtract and add variance to correct answer
                    incorrectAns = string.Format("{0}", result + randomDisplacement);   // Evaluate incorrect answer
                } while (containsString(used, incorrectAns));
                #endregion

                used.Add(incorrectAns);     // Saves the generated answer
                choices[idx].GetComponentInChildren<TextMeshProUGUI>().text = string.Format("{0}", incorrectAns);   // Sets incorrect answer to a choice box
            }
            // Set correct choice box the correct value
            else
            {
                choices[correctIdx].GetComponentInChildren<TextMeshProUGUI>().text = correctAns;    // Sets correct answer to a choice box
            }
        }
    }

    // Determines if a List of strings contains a specified string
    private bool containsString(List<string> list, string item)
    {
        foreach (string s in list)
        {
            if (s.CompareTo(item) == 0)
            {
                return true;
            }
        }
        return false;
    }

    // Takes a UI element and compares its text value to an expression's answer
    // Updates the UI element color
    // Appropriately increments user score
    public void compareAnswer(TextMeshProUGUI userAns)
    {
        if(userAns.text.CompareTo(op.getResult().ToString()) == 0)
        {
            userAns.color = Color.green;
            gameBehaviour.addScore();
        }
        else
        {
            userAns.color = Color.red;
        }
    }
}
