using System;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI movementUpKeyBindingText;
    [SerializeField] private TextMeshProUGUI movementLeftKeyBindingText;
    [SerializeField] private TextMeshProUGUI movementDownKeyBindingText;
    [SerializeField] private TextMeshProUGUI movementRightKeyBindingText;
    [SerializeField] private TextMeshProUGUI interactKeyBindingText;
    [SerializeField] private TextMeshProUGUI interactAltKeyBindingText;
    [SerializeField] private TextMeshProUGUI pauseKeyBindingText;

    private void Start()
    {
        GameInput.Instance.OnKeysRebind += GameInput_OnKeysRebind;
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        
        UpdateVisual();
        
        Show();
    }

    private void GameInput_OnKeysRebind(object sender, EventArgs e)
    {
        UpdateVisual();
    }
    
    private void KitchenGameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (KitchenGameManager.Instance.IsCountdownToStartActive())
        {
            Hide();
        }
    }

    private void UpdateVisual()
    {
        movementUpKeyBindingText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        movementLeftKeyBindingText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        movementDownKeyBindingText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        movementRightKeyBindingText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactKeyBindingText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAltKeyBindingText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact_Alternate);
        pauseKeyBindingText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
