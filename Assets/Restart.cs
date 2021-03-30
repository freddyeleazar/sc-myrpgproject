using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    private bool isRestarted;

    private void Start()
    {
        isRestarted = false;
    }

    private void Update()
    {
        if (Input.anyKeyDown && !isRestarted)
        {
            SceneManager.LoadScene(0);
            isRestarted = true;
        }
    }
}
