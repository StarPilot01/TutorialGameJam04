using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSpawner
{
    
    float _spawnCycleSec;

    Coroutine _coHumanSpawnCycle;


    public void SetSpawnCycle(float cycle)
    {
        _spawnCycleSec = cycle;
    }

    public void StartSpawn()
    {
        _coHumanSpawnCycle = CoroutineManager.StartCoroutine(StartHumanSpawnCycle(_spawnCycleSec));
    }


    IEnumerator StartHumanSpawnCycle(float cycle)
    {
        while (true)
        {
            yield return new WaitForSeconds(cycle);

            Vector2 pos = GameMap.GetEmptyCellRandomly();
            pos = GameMap.CellToWorld(pos);
            //���ڿ��� ��������
            Managers.ObjectManager.Spawn<HumanController>(pos, "Human");
        }
    }
    
}
