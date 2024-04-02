using UnityEngine;

/*
 * TODO CHANGE IT BECAUSE PERFORMANCES ARE TERRIBLES
 */
public class Billboard : MonoBehaviour
{
    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }
}