using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleObject : MonoBehaviour
{
    public InputActionReference toggleReference = null;
    // Start is called before the first frame update
    void Awake()
    {
        toggleReference.action.started += Toggle;
    }

    void onDestroy()
    {
        toggleReference.action.started -= Toggle;
    }

    // Update is called once per frame
    public void Toggle(InputAction.CallbackContext context)
    {

        List<string> posNames = transform.GetComponent<VisualizeData>().posNames;
        List<string> rotNames = transform.GetComponent<VisualizeData>().rotNames;
        List<string> scaleNames = transform.GetComponent<VisualizeData>().scaleNames;
        List<string> colorNames = transform.GetComponent<VisualizeData>().colorNames;

        transform.GetComponent<VisualizeData>().spreadPoints(posNames, rotNames, scaleNames, colorNames);

    }
}
