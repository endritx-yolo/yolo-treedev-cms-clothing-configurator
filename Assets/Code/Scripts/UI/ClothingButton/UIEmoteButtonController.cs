using NaughtyAttributes;
using UnityEngine;

public class UIEmoteButtonController : MonoBehaviour
{
    [BoxGroup("")] [SerializeField] private RectTransform _gridRectTransform;
    [BoxGroup("")] [SerializeField] private GameObject _emoteButton;

    private RuntimeMannequinAnimationManager _runtimeMannequinAnimationManager;

    private void Start()
    {
        _runtimeMannequinAnimationManager = FindObjectOfType<RuntimeMannequinAnimationManager>();
        CreateButtons();
    }
    
    
    private void CreateButtons()
    {
        if (_runtimeMannequinAnimationManager == null) return;

        for (int i = 0; i < _runtimeMannequinAnimationManager.AnimList.Clips.Count; i++)
            Instantiate(_emoteButton, _gridRectTransform);
    }
    
}