using UnityEngine;

public abstract class ABullet : MonoBehaviour
{
    protected ARangedWeapon.RangedDamageData _data;

    public ARangedWeapon.RangedDamageData Data
    {
        get => _data;
        set => _data = value;
    }
}