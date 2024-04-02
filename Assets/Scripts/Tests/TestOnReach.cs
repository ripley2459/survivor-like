using UnityEngine;

public class TestOnReach : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      if (other.TryGetComponent<TestMoveTo>(out TestMoveTo entity))
      {
         entity.gameObject.SetActive(false);
      }
   }

   private void OnTriggerStay(Collider other)
   {
      if (other.TryGetComponent<TestMoveTo>(out TestMoveTo entity))
      {
         entity.gameObject.SetActive(false);
      }
   }
}
