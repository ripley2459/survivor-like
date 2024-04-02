using System;
using UnityEngine;

public abstract class ACharacterController : MonoBehaviour
{
    public Action<ACharacterController> OnKilled;
}