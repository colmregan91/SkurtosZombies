using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkRunnerController : MonoBehaviour, INetworkRunnerCallbacks
{
    public event Action OnStartedRunnerConnection;
    public event Action OnPlayerJoinedSuccessfully;
    [SerializeField] private NetworkRunner _networkRunnerPrefab;

    public static NetworkRunner _networkRunnerInstance;

    public void ShutDownRunner()
    {
        _networkRunnerInstance.Shutdown();
    }
    
    public async void StartGame(GameMode mode, string roomName)
    {
        OnStartedRunnerConnection?.Invoke();

        if (_networkRunnerInstance == null)
        {
            _networkRunnerInstance = Instantiate(_networkRunnerPrefab);
        }
        
        //Register so we will get the callbacks as well
        _networkRunnerInstance.AddCallbacks(this);

        //ProvideInput means that that player is recording and sending inputs to the server.
        _networkRunnerInstance.ProvideInput = true;

        StartGameArgs startGameArgs = new StartGameArgs()
       {
           GameMode = mode,
           SessionName = roomName,
           PlayerCount = 4,
           SceneManager = _networkRunnerInstance.GetComponent<INetworkSceneManager>()
       };

      StartGameResult result = await _networkRunnerInstance.StartGame(startGameArgs);
      if (result.Ok)
      {
          const string SCENE_NAME = "GameScene";
          _networkRunnerInstance.SetActiveScene(SCENE_NAME);
      }
      else
      {
          Debug.LogError($"Failed to start: {result.ShutdownReason}");
      }
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
       Debug.Log("OnPlayerJoined");
       OnPlayerJoinedSuccessfully?.Invoke();
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("OnPlayerLeft");
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
       
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        Debug.Log("OnInputMissing");
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        Debug.Log("OnShutdown");
        
        const string LOBBY_SCENE = "Lobby";
        SceneManager.LoadScene(LOBBY_SCENE);
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        Debug.Log("OnConnectedToServer");
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        Debug.Log("OnDisconnectedFromServer");
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        Debug.Log("OnConnectRequest");
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        Debug.Log("OnConnectFailed");
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        Debug.Log("OnUserSimulationMessage");
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        Debug.Log("OnSessionListUpdated");
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        Debug.Log("OnCustomAuthenticationResponse");
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        Debug.Log("OnHostMigration");
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
        Debug.Log("OnReliableDataReceived");
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        Debug.Log("OnSceneLoadDone");
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        Debug.Log("OnSceneLoadStart");
    }
}
