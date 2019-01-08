using UnityEngine;

// Class to handle all math operations

public class MathOperations : MonoBehaviour {

    // Math operations to use
	private const short ADD = 0;
	private const short SUB = 1;
	private const short MUL = 2;
	private const short DIV = 3;
    private int totalOperations = 4;    // Total operations available

    // Difficulty variables
    static int upperBound = 15;

    // Math variables
    static short operationToUse;
    static float leftExp, rightExp, result;
    static char sign;

    // Creates a random binary operation expression; objects to operate on are integers in the range [1, upperBound]
    public string generateExpression()
    {
        // Generate math operation
        operationToUse = (short) Random.Range(0, totalOperations-1);

        // Generate left and right numbers
        leftExp = Random.Range(1, upperBound);
        rightExp = Random.Range(1, upperBound);

        // Converts generated operation to mathematical expression
        #region // Math operations switch case
        switch (operationToUse)
        {
            case ADD:
                result = operateAdd(leftExp, rightExp);
                sign = '+';
                break;
            case SUB:
                result = operateSub(leftExp, rightExp);
                sign = '-';
                break;
            case MUL:
                result = operateMul(leftExp, rightExp);
                sign = '*';
                break;
            case DIV:
                result = operateDiv(leftExp, rightExp);
                sign = '/';
                break;
            default:
                throw new System.Exception("Undefined math operation generated");
        }
        #endregion

        //Debug.LogFormat("{0} {1} {2} = {3}", leftExp, sign, rightExp, result);
        return string.Format("{0} {1} {2} =", leftExp, sign, rightExp); // Returns the mathematical expression to solve
    }
    
    // Gets the result of the current expression
    public float getResult()
    {
        return result;
    }

    #region MathOperations
    /** List of supported operations ingame **/
    private float operateAdd(float a, float b) {
		return a + b;
	}

	private float operateSub(float a, float b) {
		return a - b;
	}

	private float operateMul(float a, float b) {
		return a * b;
	}

	private float operateDiv(float a, float b) {
		return a / b;
	}

	private float operateMod(float a, float b) {
		return a % b;
	}
    #endregion

}
