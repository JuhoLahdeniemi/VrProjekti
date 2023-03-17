using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;

public class Button2Script : MonoBehaviour
{
    public GameManager host;
    public float waitTime = 1.0f;
    private bool waitTimeActive = false;
    public UnityEvent onPressed, onReleased;
    private int pressedCounter = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Button" && waitTimeActive && pressedCounter < 2)
        {
            onPressed.Invoke();
            host.setTargetArea(1);
            pressedCounter++;
            Debug.Log("PressedCounter on: " + pressedCounter);
            Debug.Log("Target area on: " + GameManager.manager.getTargetArea());
        }
        else if (other.tag == "Button" && waitTimeActive && pressedCounter == 2)
        {
            onPressed.Invoke();
            host.resetTargetArea();
            pressedCounter = 0;
            Debug.Log("PressedCounter on: " + pressedCounter);
            Debug.Log("Target area on: " + GameManager.manager.getTargetArea());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Button" && !waitTimeActive)
        {
            onReleased.Invoke();
            StartCoroutine(WaitForWT());
        }
    }

    IEnumerator WaitForWT()
    {
        waitTimeActive = true;
        yield return new WaitForSeconds(waitTime);
        waitTimeActive = false;
    }

}
