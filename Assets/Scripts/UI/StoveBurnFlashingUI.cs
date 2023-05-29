using System;
using UnityEngine;

public class StoveBurnFlashingUI : MonoBehaviour
{
    private const string IS_FLASHING = "IsFlashing";
    
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private float burnedShowProgressAmount = .5f;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        bool isFlashing = stoveCounter.IsFried() && e.progressNormalized >= burnedShowProgressAmount;
        
        _animator.SetBool(IS_FLASHING, isFlashing);
    }
}
