using Cinemachine;
using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawnerController : NetworkBehaviour, IPlayerJoined, IPlayerLeft 
{
    [SerializeField] private NetworkPrefabRef _playerPrefab;
    [SerializeField] private GameObject _OfflineplayerPrefab;
    [SerializeField] private Transform[] _spawnPoints;

    private void Awake()
    {
        if (FindObjectOfType<NetworkRunner>() == null) // todo : fix load up swquence in future, having loading done through state machine
        {
            Instantiate(_OfflineplayerPrefab, Vector3.zero, Quaternion.identity);

            transform.parent.gameObject.SetActive(false);
        }
    }

    public override void Spawned()
    {
        if (Runner.IsServer)
        {
            foreach (var player in Runner.ActivePlayers)
            {
                spawnPlayer(player);
            }
        }
    }

    public void PlayerJoined(PlayerRef playerRef)
    {
        spawnPlayer(playerRef);
    }

    private void spawnPlayer(PlayerRef playerRef)
    {
        if (Runner.IsServer)
        {
            int index = playerRef % _spawnPoints.Length;
            var obj = Runner.Spawn(_playerPrefab, _spawnPoints[index].position, Quaternion.identity, playerRef);
            Runner.SetPlayerObject(playerRef, obj);
        }
    }

    private void deSpawnPlayer(PlayerRef playerRef)
    {
        if (Runner.IsServer)
        {
            if (Runner.TryGetPlayerObject(playerRef, out var networkObj))
            {
                Runner.Despawn(networkObj);
            }
            Runner.SetPlayerObject(playerRef, null);
        }
    }

    public void PlayerLeft(PlayerRef playerRef)
    {
        deSpawnPlayer(playerRef);
    }
}
