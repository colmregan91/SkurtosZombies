using Cinemachine;
using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawnerController : NetworkBehaviour, IPlayerJoined, IPlayerLeft 
{
    [SerializeField] private NetworkPrefabRef m_playerPrefab;
    [SerializeField] private Transform[] m_spawnPoints;

    public override void Spawned()
    {
        if (Runner.IsServer)
        {
            foreach (var player in Runner.ActivePlayers)
            {
                SpawnPlayer(player);
            }
        }
    }

    public void PlayerJoined(PlayerRef playerRef)
    {
        SpawnPlayer(playerRef);
    }

    private void SpawnPlayer(PlayerRef playerRef)
    {
        if (Runner.IsServer)
        {
            int index = playerRef % m_spawnPoints.Length;
            var obj = Runner.Spawn(m_playerPrefab, m_spawnPoints[index].position, Quaternion.identity, playerRef);
            Runner.SetPlayerObject(playerRef, obj);
        }
    }

    private void DeSpawnPlayer(PlayerRef playerRef)
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
        DeSpawnPlayer(playerRef);
    }
}
