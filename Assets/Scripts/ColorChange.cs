using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ColorChange : MonoBehaviour
{

    public InputActionReference colorReference = null;
    private MeshRenderer meshRenderer = null;
    private float lastValue = 0;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float value = colorReference.action.ReadValue<float>();
        if (value != lastValue)
        {
            UpdateColor(value);
            lastValue = value;
        }
    }

    private void UpdateColor(float newValue)
    {
        meshRenderer.material.color = new Color(newValue, newValue, 0);
    }
}

