using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Utility
{
    public class LerpUtilities
    {
        public static Vector3 LerpVector3(Vector3 start, Vector3 end, float i)
        {
            return Vector3.Lerp(start, end, i);
        }

        public static Vector2 LerpVector2(Vector2 start, Vector2 end, float i)
        {
            return Vector2.Lerp(start, end, i);
        }

        public static float LerpFloat(float start, float end, float i)
        {
            return Mathf.Lerp(start, end, i);
        }
    }
}
