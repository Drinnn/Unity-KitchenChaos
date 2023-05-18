using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject serializedProgressEntity;
    [SerializeField] private Image barImage;

    private IHasProgress _progressEntity;

    private void Awake()
    {
        _progressEntity = serializedProgressEntity.GetComponent<IHasProgress>();
        if (_progressEntity == null)
        {
            Debug.LogError("Game Object " + serializedProgressEntity + " does not have a component that implements IHasProgress");
        }
    }

    private void Start()
    {
        _progressEntity.OnProgressChanged += ProgressEntity_OnProgressChanged;
        
        barImage.fillAmount = 0f;
        
        Hide();
    }

    private void ProgressEntity_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;

        if (e.progressNormalized == 0f || e.progressNormalized == 1f)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
