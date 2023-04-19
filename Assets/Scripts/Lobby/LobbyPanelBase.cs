using UnityEngine;

public class LobbyPanelBase : MonoBehaviour
{
    [field: SerializeField, Header("LobbyPanelBase Vars")]
    public LobbyPanelType PanelType { get; private set; }
    [SerializeField] private Animator _panelAnimator;
    
    protected LobbyUIManager lobbyUIManager;
    
    public enum LobbyPanelType
    {
        None,
        CreateNicknamePanel,
        MiddleSectionPanel
    }

    public virtual void InitPanel(LobbyUIManager uiManager)
    {
        lobbyUIManager = uiManager;
    }

    public void ShowPanel()
    {
        this.gameObject.SetActive(true);
        const string POP_IN_CLIP_NAME = "In";
        callAnimationCoroutine(POP_IN_CLIP_NAME, true);
    }

    protected void ClosePanel()
    {
        const string POP_OUT_CLIP_NAME = "Out";
        callAnimationCoroutine(POP_OUT_CLIP_NAME, false);
    }

    private void callAnimationCoroutine(string clipName, bool state)
    {
        StartCoroutine(Utils.PlayAnimAndSetStateWhenFinished(gameObject, _panelAnimator, clipName, state));
    }
}
