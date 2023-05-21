using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private float maxFootstepTimer = .1f; 
    
    private Player _player;

    private float _footstepTimer;
    
    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        _footstepTimer -= Time.deltaTime;
        if (_footstepTimer < 0f)
        {
            _footstepTimer = maxFootstepTimer;

            if (_player.IsWalking)
            {
                float volume = 1f;
                SoundManager.Instance.PlayFootstepSound(_player.transform.position, volume);
            }
        }
    }
}
