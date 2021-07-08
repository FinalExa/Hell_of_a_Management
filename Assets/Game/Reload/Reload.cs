using UnityEngine;
using UnityEngine.SceneManagement;
public class Reload : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            string thisScene = SceneManager.GetActiveScene().name;
            //SceneManager.UnloadSceneAsync(thisScene);
            SceneManager.LoadScene(thisScene);
        }
    }
}
