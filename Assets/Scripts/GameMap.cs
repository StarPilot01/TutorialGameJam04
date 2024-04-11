using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Define;

public class GameMap : MonoBehaviour
{
    [SerializeField] Tilemap tile;


    //static Vector2 _cellStartLeftTopPos = new Vector2(0, 0);
    
    //0~21 , 0~6 까지 맵 범위임
    static Vector2 _cellCount = new Vector2(22,8);
    static float _cellSize = 0.75f;

    //0부터 시작
    static bool[,] _placedItemMaps;

    // Start is called before the first frame update
    void Start()
    {
        _placedItemMaps = new bool[(int)_cellCount.y, (int)_cellCount.x];


        //Vector2 pos = GameMap.GetEmptyCellRandomly();
        //pos = GameMap.CellToWorld(pos);
        ////남자여자 랜덤으로
        //Managers.ObjectManager.Spawn<HumanController>(pos, "Human");

        //HumanController h = Managers.ObjectManager.Spawn<FemaleController>(CellToWorld(0, 0), "Human");
        

        //Managers.ObjectManager.Spawn<HumanController>(CellToWorld(9, 4), "Human");
        //Managers.ObjectManager.Spawn<HumanController>(CellToWorld(0, 6), "Human");
        //Managers.ObjectManager.Spawn<HumanController>(CellToWorld(20, 0), "Human");
        //Managers.ObjectManager.Spawn<HumanController>(CellToWorld(20, 6), "Human");

        //Managers.ObjectManager.Spawn<HumanController>(CellToWorld(1, 1), "Human");
        //Managers.ObjectManager.Spawn<HumanController>(CellToWorld(5, 5), "Human");


        //Debug.Log(WorldToCell(0.5f, -0.5f));
        //Debug.Log(WorldToCell(1.1f, -1.1f));
    }
   
    public static bool IsCellMapRange(Vector2 pos) //CellCount 기반
    {
        if (pos.x > _cellCount.x)
            return false;

        if (pos.x < 0)
            return false;

        if (pos.y > _cellCount.y)
            return false;

        if (pos.y < 0)
            return false;

        return true;
    }
    public static bool IsInsideMapArea(Vector2 pos) //World 위치 기반
    {
        Vector2 minPos = CellToWorld(0, 0);
        Vector2 maxPos = CellToWorld((int)_cellCount.x - 1, (int)_cellCount.y - 1);


        if (pos.x < minPos.x || pos.x > maxPos.x)
        {
            return false;
        }
        
        if(pos.y > minPos.y || pos.y < maxPos.y)
        {
            return false;
        }

       

        return true;
    }


    public static Vector2 GetEmptyCellRandomly()
    {
        while (true)
        {
            int x = UnityEngine.Random.Range(0, (int)_cellCount.x);
            int y = UnityEngine.Random.Range(0, (int)_cellCount.y);

            if (_placedItemMaps[y, x] == false)
            {
                return new Vector2(x, y);
            }

        }


    }
    public static Vector2 WorldToCell(float x, float y)
    {
        Vector2 cell = new Vector2((int)(x / _cellSize), - (int)(y / _cellSize));
        return cell;
    }
    public static Vector2 WorldToCell(Vector2 pos)
    {
        return WorldToCell((int)pos.x, (int)pos.y);
    }

    //public static Vector2 GetAdjustedCellCenterWorld(float x, float y)
    //{
    //    Vector2 cellPos;
    //
    //    cellPos.x = x / _cellSize + _cellSize * 0.5f;
    //    cellPos.y = y / _cellSize - _cellSize * 0.5f;
    //
    //   
    //    return cellPos;
    //
    //}
    //
    //public static Vector2 GetAdjustedCellCenterWorld(Vector2 pos)
    //{
    //    
    //
    //    return GetAdjustedCellCenterWorld(pos.x,pos.y);
    //
    //}

    public static Vector2 CellToWorld(int x, int y)
    {
        Vector2 world;

        world.x = x * _cellSize + _cellSize * 0.5f;
        world.y = -y * _cellSize - _cellSize * 0.5f;

        return world;

    } 
    
    public static Vector2 CellToWorld(Vector2 pos)
    {
        return CellToWorld((int)pos.x, (int)pos.y);
    }

    public static void PlaceItemToMap_World(Vector2 pos)
    {
        Vector2 cell = WorldToCell(pos.x, pos.y);

        _placedItemMaps[(int)cell.y, (int)cell.x] = true;

    }
    
    public static void PlaceItemToMap_Cell(Vector2 pos)
    {
        _placedItemMaps[(int)pos.y, (int)pos.x] = true;

    }

    public static void EraseItemFromMap_World(Vector2 pos)
    {

        Vector2 cell = WorldToCell(pos.x, pos.y);

        _placedItemMaps[(int)cell.y, (int)cell.x] = false;

    }
    public static void EraseItemFromMap_Cell(Vector2 pos)
    {
        _placedItemMaps[(int)pos.y, (int)pos.x] = false;

    }
}
