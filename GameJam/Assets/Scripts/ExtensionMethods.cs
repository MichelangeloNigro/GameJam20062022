using UnityEngine;

public static class ExtensionMethods
{
	public static Transform Clear(this Transform transform)
        {
            foreach (Transform child in transform) {
                GameObject.Destroy(child.gameObject);
            }
            return transform;
        }

    public static int Cycle(int number, int minInclusive, int maxExclusive) {
        if (number >= maxExclusive) {
            return minInclusive;
        }
        return number;
    }
}
