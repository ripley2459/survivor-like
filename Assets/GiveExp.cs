using Unity.VisualScripting;
using UnityEngine;

public class GiveExp : MonoBehaviour
{
    [SerializeField] private float _speed = -3;
    [SerializeField] private int _exp = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController controller))
        {
            controller.PlayerLevel.GainExp(_exp);
            gameObject.SetActive(false);
        }
    }
}