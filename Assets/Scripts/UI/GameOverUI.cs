using System;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;
    
    private void Start()
    {
        KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
      
        Hide();
    }

    private void Update()
    {
        recipesDeliveredText.text = DeliveryManager.Instance.SuccessfulRecipesAmount.ToString();
    }

    private void KitchenGameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (KitchenGameManager.Instance.IsGameOver())
        {
            Show();
        }
        else
        {
            Hide();
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
