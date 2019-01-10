using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBehaviour : MonoBehaviour {

    [SerializeField]
    Slider slider;
    [SerializeField]
    Image sliderFill;


    // Time to pick an answer
    public float timeLeft;
    // Total time to pick an answer
    const float timeLimit = 5f;
    // Status of timer ticking
    bool activeTimer = false;


    // Sets the status of the time clocking down
    public void setTimerStatus(bool status)
    {
        activeTimer = status;
    }

    // Adjust the blue and green color values of sliderFill
    private void adjustFillGBColor(float newVal)
    {
        sliderFill.color = new Color(1.0f, newVal, newVal); // Decreases non red color of fill
    }

    // Resets the timer to initial state
    public void resetTimer()
    {
        timeLeft = timeLimit;
        adjustFillGBColor(1.0f);
        setTimerStatus(true);
    }

    // Ticks time left by time change and returns remaining time
    public float getTimeLeft()
    {
        if (timeLeft > 0f)
        {
            timeLeft -= Time.deltaTime; // Ticks down the time clock

            slider.value = timeLeft / timeLimit;    // Sets the slider value

            // Adjust the color of the fill as time runs out
            adjustFillGBColor(slider.value);
        }

        return timeLeft;
    }

    // Gets status of timer
    public bool getTimerStatus()
    {
        return activeTimer;
    }
}
