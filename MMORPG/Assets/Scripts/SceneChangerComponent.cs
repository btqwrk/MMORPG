using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class SceneChangerComponent : NetworkBehaviour
{
    [ClientRpc]
    public void RpcAuthorizeSceneChange()
    {
        // Check if this client is authorized to change scenes based on game rules.
        bool isAuthorized = true; // Implement your authorization logic here.

        if (isAuthorized)
        {
            // Load the new scene (e.g., "NewSceneName").
            SceneManager.LoadScene("Map_Starting_Area_Test");
        }
    }

    // Example method to request scene change authorization from the server.
    public void RequestSceneChangeAuthorization()
    {
        if (isLocalPlayer)
        {
            CmdRequestSceneChangeAuthorization();
        }
    }

    [Command]
    private void CmdRequestSceneChangeAuthorization()
    {
        // The client requests scene change authorization from the server.
        RpcAuthorizeSceneChange();
    }
}
