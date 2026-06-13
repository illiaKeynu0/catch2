using UnityEngine;

public class CTR_Player_Avatar : MonoBehaviour
{
    public static CTR_Player_Avatar Instance;
    
    [HideInInspector]public Animator animator;
    
    private SpriteRenderer _spriteRenderer; 
    
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

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        if (_SYS_Player_Input.Instance.MoveInput.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
    }
}
