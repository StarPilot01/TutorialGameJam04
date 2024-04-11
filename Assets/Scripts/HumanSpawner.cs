using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
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
            //3분의 1의 확률로 즉시 스폰
            int rand = Random.Range(0, 6);
            
            
            if(rand != 0)
            {
                yield return new WaitForSeconds(cycle);

            }




            SpawnHuman();


        }
    }

    void SpawnHuman()
    {
        int humanCount = Random.Range(1, 3);

        for(int i = 0; i < humanCount; i++)
        {
            Vector2 pos = GameMap.GetEmptyCellRandomly();
            pos = GameMap.CellToWorld(pos);
            //남자여자 랜덤으로

            int genderIdx = Random.Range(0, 2);

            string resourceName = ((EHumanGender)genderIdx).ToString();


            int speed = GetRandomSpeed();



            HumanController human = Managers.ObjectManager.Spawn<HumanController>(pos, resourceName);
            human.MoveSpeed = speed;
        }

        
    }
    

    int GetRandomSpeed()
    {
        float elapsedTime = Managers.GameManager.ElapsedTime;
        if (elapsedTime <= 20.0f)
        {
            return Random.Range(80, 120);
        }
        else if (elapsedTime <= 45.0f)
        {
            return Random.Range(100, 180);

        }
        else if (elapsedTime <= 65.0f)
        {
            return Random.Range(150, 250);

        }
        else if (elapsedTime <= 80.0f)
        {
            return Random.Range(200, 280);

        }
        else
        {
            return Random.Range(280, 300);

        }
    }

    public void Stop()
    {
        CoroutineManager.StopCoroutine(_coHumanSpawnCycle);
    }
}
