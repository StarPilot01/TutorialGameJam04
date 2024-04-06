using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] int spawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SojuSpawner());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (Managers.GameManager.ClickMode == Define.EClickMode.Eat)
                return;

            Vector2 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            UrusaSpawn(vec);
        }
    }

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
    

    /// <summary>
    /// 시간마다 일단 랜덤 Cell 받아와서 스폰되게 했습니다.
    /// </summary>
    /// <param name="vec"></param>
    /// <returns></returns>
    Soju InstantiateSoju(Vector2 vec)
    {
        Debug.Log(vec);
        Soju soju = Managers.ObjectManager.Spawn<Soju>(vec, "Soju");
        return soju;
    }
    IEnumerator SojuSpawner()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnDelay);

            Vector2 pos = GameMap.GetEmptyCellRandomly();
            pos = GameMap.CellToWorld(pos);
            InstantiateSoju(pos);
        }
    }
}
