using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;

public class ButtonScript : MonoBehaviour
{
    public CannonControl host;
    public float waitTime = 1.0f;
    private bool waitTimeActive = false;
    public UnityEvent onPressed, onReleased;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Button" && waitTimeActive)
        {
            onPressed.Invoke();
            host.Shoot();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Button" && !waitTimeActive)
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
