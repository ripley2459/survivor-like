using UnityEngine;

public class NormalBullet : ABullet
{
    private void FixedUpdate()
    {
        var position1 = transform.position;
        position1 += _data.Direction * Time.deltaTime * _data.Speed;
        position1.y = 0.5f;
        transform.position = position1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ACharacterController>(out ACharacterController controller) && controller.Team == _data.TargetTeam)
        {
            controller.Vitality.Damage(_data.Damage);
            gameObject.SetActive(--_data.Piercing >= 0);
        }
    }
}