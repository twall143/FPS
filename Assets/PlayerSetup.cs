using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour
{
   [SerializeField]
    Behaviour[] componentsToDiable;

    [SerializeField]
    string remoteLayerName = "RemotePlayer";

    Camera sceneCamera;

    void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
        }
        else
        {
           sceneCamera = Camera.main;
            if(sceneCamera != null)
                {
               sceneCamera.gameObject.SetActive(false);
            }
        }     
   
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();

        MultiplayerGameManager.RegisterPlayer(_netID, _player);
    }
    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    void DisableComponents()
    {
        for (int i = 0; i < componentsToDiable.Length; i++)
        {
            componentsToDiable[i].enabled = false;
        }
    }
    private void OnDisable()
    {
        if (sceneCamera != null)
        {
           sceneCamera.gameObject.SetActive(true);
        }

        MultiplayerGameManager.UnRegisterPlayer(transform.name);
    }

}
