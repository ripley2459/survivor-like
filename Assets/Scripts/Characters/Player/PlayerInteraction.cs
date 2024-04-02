using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float _interactionDistance = 5f;
    [SerializeField] private LayerMask _interactionLayer;
    private Camera _camera;
    private InteractionFeedbacks _interactionFeedbacks;
    private float _interactionDuration = 0f;
    private IInteractable _targetInteractable;
    private IInteractable _lastInteractable;
    public bool Interacting => PlayerController.PlayerActions.Human.Interact.ReadValue<float>() > 0f;
    public PlayerController PlayerController { get; private set; }

    private void Awake()
    {
        _camera = GetComponentInChildren<Camera>();
        PlayerController = GetComponent<PlayerController>();
        _interactionFeedbacks = GetComponentInChildren<InteractionFeedbacks>();
    }

    private void Update()
    {
        // Check if valid interaction
        if (ReferenceEquals(_targetInteractable, null))
        {
            ResetInteraction();
            return;
        }

        // Check if actual target is the same as the one of the previous frame
        if (!ReferenceEquals(_lastInteractable, null) && _targetInteractable != _lastInteractable)
        {
            ResetInteraction();
            return;
        }

        _lastInteractable = _targetInteractable;

        switch (_targetInteractable.GetInteractionState())
        {
            case InteractionState.Usable:
                _interactionFeedbacks.gameObject.SetActive(true);
                _interactionFeedbacks.SetInteractionText(_targetInteractable.GetInteractionUsableText());
                break;
            case InteractionState.Used:
                _interactionFeedbacks.gameObject.SetActive(true);
                _interactionFeedbacks.SetInteractionText(_targetInteractable.GetInteractionUsedText());
                break;
            case InteractionState.Locked:
                _interactionFeedbacks.gameObject.SetActive(true);
                _interactionFeedbacks.SetInteractionText(_targetInteractable.GetInteractionLockedText());
                break;
            case InteractionState.Deactivated:
                _interactionFeedbacks.gameObject.SetActive(false);
                break;
            default:
                _interactionFeedbacks.gameObject.SetActive(false);
                return;
        }

        if (Interacting && _targetInteractable.GetInteractionState() == InteractionState.Usable)
        {
            _interactionDuration += Time.deltaTime;

            if (_interactionDuration >= _targetInteractable.GetInteractionDuration())
            {
                _targetInteractable.Interact();
                ResetInteraction();
                return;
            }
        }
        else _interactionDuration = 0f;

        _interactionFeedbacks.SetInteractionPercentage(_interactionDuration, _targetInteractable.GetInteractionState() == InteractionState.Usable ? _targetInteractable.GetInteractionDuration() : 0f);
    }

    private void ResetInteraction()
    {
        _targetInteractable = null;
        _lastInteractable = null;
        _interactionDuration = 0f;
        _interactionFeedbacks.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, _interactionDistance, _interactionLayer))
        {
            SetInteractable(hit.collider.TryGetComponent<IInteractable>(out IInteractable targetInteractable) ? targetInteractable : null);
        }
        else SetInteractable(null);
    }

    private void SetInteractable(IInteractable interactable)
    {
        _targetInteractable = !ReferenceEquals(interactable, null) && interactable.GetInteractionState() != InteractionState.Deactivated ? interactable : null;
    }

    public enum InteractionState
    {
        /// <summary>
        /// The interaction is ready to be consumed.
        /// </summary>
        Usable,

        /// <summary>
        /// The interaction has been consumed.
        /// </summary>
        Used,

        /// <summary>
        /// Interaction is unusable.
        /// </summary>
        Locked,

        /// <summary>
        /// The interaction is unusable and doesn't provides feedbacks.
        /// </summary>
        Deactivated
    }
}