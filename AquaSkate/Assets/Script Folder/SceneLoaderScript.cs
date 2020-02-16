using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderScript : MonoBehaviour
{
    public void SceneManagement(GameObject button)
    {
        SceneManager.LoadScene(button.name);
    }
    
}
