using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalManagers : MonoBehaviour
{
    public static GlobalManagers Instance { get; private set; }

    [SerializeField] private GameObject _parentObj;

    [field: SerializeField] public NetworkRunnerController NetworkRunnerController { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(_parentObj);
        }
    }

    public void PlayOffline()
    {
        NetworkRunnerController = null;
        SceneManager.LoadScene("GameScene");
    }
}