using UnityEngine;
using UnityEngine.UI;

public class LoadingCavnasController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Button _cancelBtn;
    private NetworkRunnerController _networkRunnerController;
    
    private void Start()
    {
        _networkRunnerController = GlobalManagers.Instance.NetworkRunnerController;
        _networkRunnerController.OnStartedRunnerConnection += onStartedRunnerConnection;
        _networkRunnerController.OnPlayerJoinedSuccessfully += onPlayerJoinedSuccessfully;
        
        _cancelBtn.onClick.AddListener(_networkRunnerController.ShutDownRunner);
        this.gameObject.SetActive(false);
    }
    private void onStartedRunnerConnection()
    {
        this.gameObject.SetActive(true);
        const string CLIP_NAME = "In";
        StartCoroutine(Utils.PlayAnimAndSetStateWhenFinished(gameObject, _animator, CLIP_NAME));
    }
    
    private void onPlayerJoinedSuccessfully()
    {
        const string CLIP_NAME = "Out";
        StartCoroutine(Utils.PlayAnimAndSetStateWhenFinished(gameObject, _animator, CLIP_NAME, false));
    }

    private void OnDestroy()
    {
        _networkRunnerController.OnStartedRunnerConnection -= onStartedRunnerConnection;
        _networkRunnerController.OnPlayerJoinedSuccessfully -= onPlayerJoinedSuccessfully;
    }
}
