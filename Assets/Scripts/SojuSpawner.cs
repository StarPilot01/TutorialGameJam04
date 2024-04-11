using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SojuSpawner
{
    
    float _spawnCycleSec;
    Coroutine _coSojuSpanwCycle;

    public void SetSpawnCycle(float cycle)
    {
        _spawnCycleSec = cycle;
    }

    public void StartSpawn()
    {
        _coSojuSpanwCycle = CoroutineManager.StartCoroutine(StartSojuSpawnCycle(_spawnCycleSec));
    }

    IEnumerator StartSojuSpawnCycle(float cycle)
    {
        while (true)
        {
            yield return new WaitForSeconds(cycle);

            int SojuCount = Random.Range(1, 3);

            for(int i = 0; i < SojuCount; i++)
            {
                Vector2 pos = GameMap.GetEmptyCellRandomly();
                pos = GameMap.CellToWorld(pos);
                Managers.ObjectManager.Spawn<Soju>(pos, "Soju");
            }
           
        }
    }

    public void Stop()
    {
        CoroutineManager.StopCoroutine(_coSojuSpanwCycle);
    }



}
