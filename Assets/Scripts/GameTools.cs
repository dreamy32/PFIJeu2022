using UnityEngine;

public static class GameTools
{
    public static bool CompareLayers(LayerMask layerMask, int layer)
    {
        return (layerMask & 1 << layer) == 1 << layer;
    }
}