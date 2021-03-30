using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour {

    public Animation2DManager manager;
    public string exampleName;

    void OnGUI()
    {
        if (GUILayout.Button("Play Animation Once"))
        {
            manager.Play(exampleName, reverse: true, loop: false, restart: true);
        }

        if (GUILayout.Button("Play Animation Looped"))
        {
            manager.Play(exampleName, reverse: true, loop: true, restart: true);
        }

        if (manager.isPlaying(exampleName))
        {
            if (GUILayout.Button("Pause"))
            {
                manager.Pause(exampleName);
            }
        }
        else
        {
            if (GUILayout.Button("Resume"))
            {
                manager.Resume(exampleName);
            }
        }
    }
}
