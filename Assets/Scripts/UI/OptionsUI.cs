using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { private set; get; }
    
    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;
    
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAltButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAltText;
    [SerializeField] private TextMeshProUGUI pauseText;

    [SerializeField] private Transform pressToRebindKeyTransform;



    private void Awake()
    {
        Instance = this;
        
        soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        closeButton.onClick.AddListener(Hide);
        
        moveUpButton.onClick.AddListener(() => RebindKey(GameInput.Binding.Move_Up));
        moveDownButton.onClick.AddListener(() => RebindKey(GameInput.Binding.Move_Down));
        moveLeftButton.onClick.AddListener(() => RebindKey(GameInput.Binding.Move_Left));
        moveRightButton.onClick.AddListener(() => RebindKey(GameInput.Binding.Move_Right));
        interactButton.onClick.AddListener(() => RebindKey(GameInput.Binding.Interact));
        interactAltButton.onClick.AddListener(() => RebindKey(GameInput.Binding.Interact_Alternate));
        pauseButton.onClick.AddListener(() => RebindKey(GameInput.Binding.Pause));
    }

    private void Start()
    {
        KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;
        
        UpdateVisual();
        
        HideRebindingOverlay();
        Hide();
    }

    private void KitchenGameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.Volume * 10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.Volume * 10f);

        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact_Alternate);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void ShowRebindingOverlay()
    {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }
    
    private void HideRebindingOverlay()
    {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindKey(GameInput.Binding binding)
    {
        ShowRebindingOverlay();
        GameInput.Instance.RebindBinding(binding, () =>
        {
            HideRebindingOverlay();
            UpdateVisual();
        });
    }
}
