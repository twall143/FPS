﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetworkManager : NetworkManager
{
    /*
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameManagerNetwork.playersNumber++;

        GameObject player = null;

        if (VariablesRealTime.type == "Cat")
        {
            player = Instantiate(playerPrefab, singleton.startPositions[1].transform.position, singleton.startPositions[1].transform.rotation);

            player.tag = "Cat";

            GameManagerNetwork.cats++;
        }
        else if (VariablesRealTime.type == "Bear")
        {
            player = Instantiate(playerPrefab, singleton.startPositions[0].transform.position, singleton.startPositions[0].transform.rotation);

            player.tag = "Bear";

            GameManagerNetwork.bears++;
        }

        if (player != null)
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
    */
}