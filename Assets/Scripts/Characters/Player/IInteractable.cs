public interface IInteractable
{
    public PlayerInteraction.InteractionState GetInteractionState();

    public float GetInteractionDuration();

    public string GetInteractionUsableText();

    public string GetInteractionUsedText();

    public string GetInteractionLockedText();

    public void Interact();
}