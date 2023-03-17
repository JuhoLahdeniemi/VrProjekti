using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBarrel : MonoBehaviour
{
    public float currentAngle;
    public float startAngle;
    public bool rotating;
    public float rotateDuration;
    public float counter;
    private float _xAngle;

    public float xAngle
    {
        get
        {
            return _xAngle;
        }

        set
        {
            Debug.Log("Uusi kulma, johon piippu k‰‰ntyy on: " + value);
            startAngle = transform.localRotation.eulerAngles.x; // rotaation x  arvo, josta l‰hdet‰‰n.
            _xAngle = value;
            rotating = true;
            counter = 0;
        }
    }


    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;

        if (counter > rotateDuration && rotating == true)
        {
            // Piippu on k‰‰ntym‰ss‰ ja juuri k‰‰ntynyt oikeaan kulmaan, koska counter menee suuremmaksi kuin rotateDuration.
            rotating = false; // Tiedet‰‰n, ett‰ k‰‰ntyminen on p‰‰ttynyt.
        }

        currentAngle = Mathf.LerpAngle(startAngle, _xAngle, counter / rotateDuration);
        //transform.localEulerAngles = new Vector3(currentAngle, 0, 0);
        transform.localEulerAngles = new Vector3(0, 0, currentAngle);
    }
}
