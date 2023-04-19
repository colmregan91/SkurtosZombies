using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiddleSectionPanel : LobbyPanelBase
{
    [Header("MiddleSectionPanel Vars")] 
    [SerializeField] private Button _joinRandomRoomBtn;
    [SerializeField] private Button _joinRoomByArgBtn;
    [SerializeField] private Button _createRoomBtn;

    [SerializeField] private TMP_InputField _joinRoomByArgInputField;
    [SerializeField] private TMP_InputField _createRoomInputField;
    private NetworkRunnerController _networkRunnerController;
    
    public override void InitPanel(LobbyUIManager uiManager)
    {
        base.InitPanel(uiManager);

        _networkRunnerController = GlobalManagers.Instance.NetworkRunnerController;
        _joinRandomRoomBtn.onClick.AddListener(joinRandomRoom);
        _joinRoomByArgBtn.onClick.AddListener(() => createRoom(GameMode.Client, _joinRoomByArgInputField.text));
        _createRoomBtn.onClick.AddListener(() => createRoom(GameMode.Host, _createRoomInputField.text));
    }

    private void createRoom(GameMode mode, string field)
    {
        if (field.Length >= 2)
        {
            Debug.Log($"------------{mode}------------");
            _networkRunnerController.StartGame(mode, field);
        }
    }

    private void joinRandomRoom()
    {
        Debug.Log($"------------JoinRandomRoom!------------");
        _networkRunnerController.StartGame(GameMode.AutoHostOrClient, string.Empty);
    }
}