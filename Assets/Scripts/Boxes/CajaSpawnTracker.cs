using UnityEngine;

public class CajaSpawnTracker : MonoBehaviour
{
    private SpawnBoxes spawner;
    private int spawnIndex;

    public void Setup(SpawnBoxes s, int index)
    {
        spawner = s;
        spawnIndex = index;
    }

    void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.LiberarPunto(spawnIndex);
        }
    }
}