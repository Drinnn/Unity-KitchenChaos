using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class TestingNetcodeUI : MonoBehaviour
{
    [SerializeField] private Button startAsHostButton;
    [SerializeField] private Button startAsClientButton;

    private void Awake()
    {
        startAsHostButton.onClick.AddListener(() =>
        {
           Debug.Log("Starting as Host...");
           NetworkManager.Singleton.StartHost();
           Hide();
        });
        startAsHostButton.onClick.AddListener(() =>
        {
            Debug.Log("Starting as Client...");
            NetworkManager.Singleton.StartClient();
            Hide();
        });
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
