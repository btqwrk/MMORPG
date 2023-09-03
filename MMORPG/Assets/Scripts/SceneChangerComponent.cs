using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerComponent : MonoBehaviour
{
    [SerializeField] private string sceneName;

    // Start is called before the first frame update
    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }   
}
