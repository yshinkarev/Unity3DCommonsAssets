using UnityEngine;

namespace Commons.Utils
{
    public static class TransformUtils
    {
        // Rotation.

        public static Quaternion ChangeRotationY(Quaternion rotation, float newAngleY)
        {
            Vector3 v = rotation.eulerAngles;
            return Quaternion.Euler(v.x, newAngleY, v.z);
        }

        public static Quaternion AddRotationY(Quaternion rotation, float newAngleY)
        {
            Vector3 v = rotation.eulerAngles;
            return Quaternion.Euler(v.x, v.y + newAngleY, v.z);
        }

        // Transform.

        public static Vector3 ChangeTransformX(Transform transform, float x)
        {
            Vector3 position = transform.position;
            position.x = x;
            transform.position = position;
            return position;
        }

        public static Vector3 ChangeTransformY(GameObject go, float y)
        {
            return ChangeTransformY(go.GetComponent<Transform>(), y);
        }

        public static Vector3 ChangeTransformY(Transform transform, float y)
        {
            Vector3 position = transform.position;
            position.y = y;
            transform.position = position;
            return position;
        }

        public static Vector3 ChangeTransformZ(Transform transform, float z)
        {
            Vector3 position = transform.position;
            position.z = z;
            transform.position = position;
            return position;
        }

        // Scale.

        public static Vector3 ChangeScaleX(Transform transform, float x)
        {
            Vector3 scale = transform.localScale;
            scale.x = x;
            transform.localScale = scale;
            return scale;
        }

        public static Vector3 ChangeScaleY(Transform transform, float y)
        {
            Vector3 scale = transform.localScale;
            scale.y = y;
            transform.localScale = scale;
            return scale;
        }

        public static Vector3 ChangeScaleZ(Transform transform, float z)
        {
            Vector3 scale = transform.localScale;
            scale.z = z;
            transform.localScale = scale;
            return scale;
        }
    }
}