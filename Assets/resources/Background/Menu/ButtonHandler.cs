using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private string sceneName; // Name of the scene to load
    public Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnStartButtonPress);
    }

    void OnStartButtonPress()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}