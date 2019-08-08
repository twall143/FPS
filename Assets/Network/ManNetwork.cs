using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class ManNetwork : NetworkManager
{
    /*
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        ManGame.playerNumber++;
        var player = (GameObject)GameObject.Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        if (ManGame.playerNumber == 1) { player.GetComponent<NetPlayer>().team = 1; }
        if (ManGame.playerNumber == 2) { player.GetComponent<NetPlayer>().team = 2; }
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        player.GetComponent<NetPlayer>().Init();
    }
    */
}