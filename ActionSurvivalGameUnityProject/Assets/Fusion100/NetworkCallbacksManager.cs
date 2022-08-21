using ASG.Gameplay.Character;
using ASG.Gameplay.Player;
using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkCallbacksManager : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private NetworkPrefabRef _playerPrefab;
    private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();

    private PlayerInputsHandler m_localInputsHandler = null;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    #region INetworkRunnerCallbacks
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("OnPlayerJoined");
        /*if(runner.IsServer && !m_isReady)
        {
            LoadGameplayScene(runner);
        }*/

        if (runner.IsServer)
        {
            CreateCharacterForPlayer(runner, player);
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        // Find and remove the players avatar
        if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
        {
            runner.Despawn(networkObject);
            _spawnedCharacters.Remove(player);
        }
    }
    public void OnInput(NetworkRunner runner, NetworkInput input) 
    {
        var data = new NetworkInputData();

        if(CheckLocalCharacter())
        {
            data.lookAround = m_localInputsHandler.LocalLookAroundInputs;
            data.movement = m_localInputsHandler.LocalMovementInputs;
        }
        else
        {
            data.lookAround = default;
            data.movement = default;
        }

        input.Set(data);
    }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnConnectedToServer(NetworkRunner runner) 
    {
        Debug.Log("OnConnectedToServer");
    }
    public void OnDisconnectedFromServer(NetworkRunner runner) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }
    public void OnSceneLoadDone(NetworkRunner runner) 
    {
        Debug.Log("OnSceneLoadDone");
        if (runner.IsServer)
        {
            CheckAllPlayersHaveACharacter(runner);
        }
            
    }
    public void OnSceneLoadStart(NetworkRunner runner) 
    {
        Debug.Log("OnSceneLoadStart");
    }
    #endregion

    private bool CheckLocalCharacter()
    {
        if (m_localInputsHandler != null) return true;

        List<PlayerInputsHandler> inputsHandler = new List<PlayerInputsHandler>(FindObjectsOfType<PlayerInputsHandler>());
        m_localInputsHandler = inputsHandler.Find(handler => handler.HasInputAuthority);

        return m_localInputsHandler != null;
    }

    public void CheckAllPlayersHaveACharacter(NetworkRunner runner)
    {
        if (!runner.IsServer) return;
        List<PlayerRef> players = new List<PlayerRef>(runner.ActivePlayers);
        for(int i = 0; i < players.Count; ++i)
        {
            if(_spawnedCharacters.TryGetValue(players[i], out NetworkObject obj) && obj != null)
            {

            }
            else
            {
                CreateCharacterForPlayer(runner, players[i]);
            }
        }
    }

    private void CreateCharacterForPlayer(NetworkRunner runner, PlayerRef player)
    {
        if (_spawnedCharacters.ContainsKey(player)) return;
        // Create a unique position for the player
        Vector3 spawnPosition = new Vector3((player.RawEncoded % runner.Config.Simulation.DefaultPlayers) * 3, 1, 0);
        NetworkObject networkPlayerObject = runner.Spawn(_playerPrefab, spawnPosition, Quaternion.identity, player);
        // Keep track of the player avatars so we can remove it when they disconnect
        _spawnedCharacters.Add(player, networkPlayerObject);
    }

    #region Accessors
    public NetworkObject GetLocalPlayerCharacter()
    {
        if(_spawnedCharacters.TryGetValue(GetComponent<NetworkRunner>().LocalPlayer, out NetworkObject l_obj))
        {
            return l_obj;
        }
        return null;
    }
    #endregion
}
