using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball") && GameManager.manager.getTargetArea() == 3)
        {
            GameManager.manager.setPoints(3);
        }
        else if (collision.gameObject.CompareTag("Ball") && GameManager.manager.getTargetArea() == 2)
            {
            GameManager.manager.setPoints(2);
        }
        else if (collision.gameObject.CompareTag("Ball") && GameManager.manager.getTargetArea() == 1)
        {
            GameManager.manager.setPoints(1);
        }

        if (collision.gameObject.CompareTag("Target"))
        {
            Destroy(this.gameObject);
        }
    }
}
