using UnityEngine;

public class TestInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private float _interactionDuration = 0f;
    [SerializeField] private string _interactionUsableText = "Interaction text!";
    [SerializeField] private string _interactionUsedText = "Interaction used!";
    [SerializeField] private string _interactionLockedText = "Interaction locked!";
    private PlayerInteraction.InteractionState _state = PlayerInteraction.InteractionState.Usable;

    public PlayerInteraction.InteractionState GetInteractionState()
    {
        return _state;
    }

    public float GetInteractionDuration()
    {
        return _interactionDuration;
    }

    public string GetInteractionUsableText()
    {
        return _interactionUsableText;
    }

    public string GetInteractionUsedText()
    {
        return _interactionUsedText;
    }

    public string GetInteractionLockedText()
    {
        return _interactionLockedText;
    }

    public void Interact()
    {
        _state = PlayerInteraction.InteractionState.Used;
        print("HELLO WORLD!");
    }
}