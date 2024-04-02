using System.Collections.Generic;
#if UNITY_STANDALONE
using UnityEngine;
#endif

public static class R
{
    private static System.Random _random = new System.Random();

    public static T GetRandom<T>(T[] objects)
    {
        return objects[_random.Next(0, objects.Length)];
    }

    public static float Remap(float value, float minO, float maxO, float minD, float maxD)
    {
        return minD + (value - minO) * (maxD - minD) / (maxO - minO);
    }

#if UNITY_STANDALONE
    public static RaycastHit[] ConeCastAll(Vector3 origin, float maxRadius, Vector3 direction, float maxDistance, float coneAngle, LayerMask layers)
    {
        RaycastHit[] sphereCastHits = Physics.SphereCastAll(origin - new Vector3(0, 0, maxRadius), maxRadius, direction, maxDistance, layers);
        List<RaycastHit> coneCastHitList = new List<RaycastHit>();

        if (sphereCastHits.Length > 0)
        {
            foreach (var t in sphereCastHits)
            {
                Vector3 hitPoint = t.collider.ClosestPoint(origin);
                Vector3 directionToHit = hitPoint - origin;
                float angleToHit = Vector3.Angle(direction, directionToHit);

                if (angleToHit < coneAngle)
                    coneCastHitList.Add(t);
            }
        }

        RaycastHit[] coneCastHits = new RaycastHit[coneCastHitList.Count];
        coneCastHits = coneCastHitList.ToArray();

        return coneCastHits;
    }
#endif
}