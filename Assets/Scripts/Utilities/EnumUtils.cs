using System;

public static class EnumUtils
{
    public static T GetRandomEnumExcludingFirst<T>() where T : Enum
    {
        var values = (T[])Enum.GetValues(typeof(T));
        int index = UnityEngine.Random.Range(1, values.Length); // исключает 0

        return values[index];
    }
}
