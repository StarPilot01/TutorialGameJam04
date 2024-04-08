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

            Vector2 pos = GameMap.GetEmptyCellRandomly();
            pos = GameMap.CellToWorld(pos);
            Managers.ObjectManager.Spawn<Soju>(pos, "Soju");
        }
    }



    //void Update()
    //{
    //    if(Input.GetMouseButtonDown(0))
    //    {
    //        if (Managers.GameManager.ClickMode == Define.EClickMode.Eat)
    //            return;
    //
    //        Vector2 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //
    //        UrusaSpawn(vec);
    //    }
    //}

    /// <summary>
    /// //CellToWorld 써서 하고 싶었는데 cellSize가 0.75라서 특정 구간되면
    /// 그 구간이 반올림이 딱 떨어져서 Cell이 정확하게 안나옵니다.
    /// 그래서 일단 그냥 마우스 위치로 설치되게하겠습니다.
    /// </summary>
    /// <param name="vector"></param>

    public void UrusaSpawn(Vector2 vector) 
    {
        //Debug.Log("Mouse : " + vector);
        //vector = new Vector2(Mathf.Round(vector.x), -Mathf.Round(-vector.y));
        //Debug.Log("Mouse Round : " + vector);        
        //Vector2 pos = GameMap.WorldToCell(vector);
        Debug.Log("World To Cell : " + vector);

        if (!GameMap.IsWorldMapRange(vector))
            return;

        InstantiateUrusa(vector);
    }
    Ursa InstantiateUrusa(Vector2 vec)
    {
        //Vector3 pos = new Vector3(vec.x, vec.y, 0);
        //Vector3 pos = GameMap.CellToWorld(vec);

        Debug.Log("Cell To World : " + vec);
        Ursa urusa = Managers.ObjectManager.Spawn<Ursa>(vec, "Urusa");

        return urusa;
    }
    

  
}
