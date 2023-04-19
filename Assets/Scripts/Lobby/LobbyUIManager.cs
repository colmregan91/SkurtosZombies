using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUIManager : MonoBehaviour
{
    [SerializeField] private LoadingCavnasController _loadingCanvasControllerPrefab;
    [SerializeField] private LobbyPanelBase[] _lobbyPanels;

    private void Start()
    {
        foreach (var lobby in _lobbyPanels)
        {
            lobby.InitPanel(this);
        }
        
        Instantiate(_loadingCanvasControllerPrefab);
    }

    public void ShowPanel(LobbyPanelBase.LobbyPanelType type)
    {
        foreach (var lobby in _lobbyPanels)
        {
            if (lobby.PanelType == type)
            {
                lobby.ShowPanel();
                break;
            }
        }
    }
}