using System;
using UnityEngine;

public static class MathExtention 
{
    public static int IntegerPartOfValue(float value) => (int)Math.Truncate((decimal)value);

    public static float FractionalPartOfValue(float value) => value - IntegerPartOfValue(value);

    public static float Direction(float value) => value > 0 ? 1 : -1;

    public static Vector2 Direction(Vector2 vector) => vector.normalized;

    public static int RoundedValue(float value)
    {
        var fractionalCoordinate = FractionalPartOfValue(value);

        return fractionalCoordinate > 0.5f * Direction(value) ? Mathf.CeilToInt(value) : Mathf.RoundToInt(value);
    }

    public static Vector2 RoundedVector(Vector2 vector) => 
        new Vector2(RoundedValue(vector.x), RoundedValue(vector.y));

    public static T TryGetComponentFromIgnoredRaycast<T>(Vector2 origin, Vector2 direction, int rayID)
    {
        var rays = Physics2D.RaycastAll(origin, direction);

        rays[rayID].collider.TryGetComponent(out T obtainedComponent);

        return obtainedComponent;
    }

    public static T TryGetComponentFromIgnoredRaycast<T>(Vector2 origin, Vector2 direction, float distance, int rayID)
    {
        var rays = Physics2D.RaycastAll(origin, direction, distance);

        rays[rayID].collider.TryGetComponent(out T obtainedComponent);

        return obtainedComponent;
    }
}
