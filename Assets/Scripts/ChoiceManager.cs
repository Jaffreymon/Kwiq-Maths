using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChoiceManager : MonoBehaviour {

    [SerializeField]
    Button[] choices;

    [SerializeField]
    MathOperations op;
	
    public void createChoices()
    {
        op.generateExpression();
    }

	public void setChoices()
    {

        int correctIdx = Random.Range(0,choices.Length-1);
        float result = op.getResult();
        string correctAns = string.Format("{0}", result);
        string incorrectAns;

        List<string> used = new List<string>();

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
                    int randomDisplacement = Random.Range(1, 5);
                    randomDisplacement *= (Random.value > 0.5) ? -1 : 1;
                    incorrectAns = string.Format("{0}", result + randomDisplacement);
                } while (containsString(used, incorrectAns));
                #endregion

                used.Add(incorrectAns);
                choices[idx].GetComponentInChildren<TextMeshProUGUI>().text = string.Format("{0}", incorrectAns);
            }
            // Set correct choice box the correct value
            else
            {
                choices[correctIdx].GetComponentInChildren<TextMeshProUGUI>().text = correctAns;
            }
        }
    }

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

    public void compareAnswer(TextMeshProUGUI userAns)
    {
        if(userAns.text.CompareTo(op.getResult().ToString()) == 0)
        {
            userAns.color = Color.green;
        }
        else
        {
            userAns.color = Color.red;
        }
    }
}
