using UnityEngine;
using UnityEngine.InputSystem;

public class _SYS_Player_Input : MonoBehaviour
{
    public static _SYS_Player_Input Instance;
    
    [HideInInspector]public Vector2 MoveInput;
    
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
   
    public void OnMove(InputValue value)
    {
        MoveInput = value.Get<Vector2>();
    }
    
    public void OnReplay(InputValue button)
    {
        if (button.isPressed && _SYS_GameManager.Instance.currentGameState == _SYS_GameManager.GameState.End && CTR_Water.Instance.isReset)
        {
            _SYS_GameManager.Instance.ResetGame();
        }
    }

    public void OnDash(InputValue button)
    {
        if (button.isPressed && CTR_DashUpgrade.IsDashActive)
        { 
            CTR_Player_Physics.Instance.Dash();
        }
    }
}
