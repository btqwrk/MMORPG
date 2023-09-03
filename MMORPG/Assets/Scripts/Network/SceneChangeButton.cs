using UnityEngine;
using UnityEngine.UI;

public class SceneChangeButton : MonoBehaviour
{
    public string sceneName = "YourSceneName"; // Change this to the desired scene name.

    public SceneChangeRequester sceneChangeRequester;

    private void Start()
    {

        // Add a click event handler to the button to request a scene change.
        Button button = GetComponent<Button>();
        if (button != null && sceneChangeRequester != null)
        {
            button.onClick.AddListener(RequestSceneChange);
        }
    }

    private void RequestSceneChange()
    {
        // Request a scene change using the SceneChangeRequester.
        if (sceneChangeRequester != null)
        {
            sceneChangeRequester.RequestSceneChange(sceneName);
        }
    }
}
