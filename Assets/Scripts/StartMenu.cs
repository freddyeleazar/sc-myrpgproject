using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public WorldSettings_ worldSettings;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(NextScene);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(1);
    }
}
