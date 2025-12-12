using UnityEngine;

public class MultiDisplayActivator : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Displays connected: " + Display.displays.Length);

        // Display 0 = main (already active)
        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
            Debug.Log("Activated display: " + i);
        }
    }
}

