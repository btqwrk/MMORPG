using Mirror;
using Unity;

public class UINetworkPlayerScript : NetworkBehaviour
{
    private void Start()
    {
        if (isLocalPlayer)
        {
            // Request authority over this object
            CmdRequestAuthority();
        }
    }

    [Command]
    private void CmdRequestAuthority()
    {
        // Assign authority to the requesting client
        if (!isOwned)
        {
            NetworkIdentity networkIdentity = GetComponent<NetworkIdentity>();
            if (networkIdentity != null)
            {
                networkIdentity.AssignClientAuthority(connectionToClient);
            }
        }
    }

    [Command]
    public void CmdChangeScene(string sceneName)
    {
        // Your scene change logic here
    }
}
