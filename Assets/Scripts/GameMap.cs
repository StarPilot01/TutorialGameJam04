using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class GameMap : MonoBehaviour
{
   
    //static Vector2 _cellStartLeftTopPos = new Vector2(0, 0);
    
    //0~21 , 0~6 까지 맵 범위임
    static Vector2 _cellCount = new Vector2(22,7);
    static float _cellSize = 0.75f;

    //0부터 시작
    BaseController[,] _map;

    // Start is called before the first frame update
    void Start()
    {
        _map = new BaseController[(int)_cellCount.y, (int)_cellCount.x];


        Managers.ObjectManager.Spawn<HumanController>(CellToWorld(0, 0), "Human");
        Managers.ObjectManager.Spawn<HumanController>(CellToWorld(9, 7), "Human");
        Managers.ObjectManager.Spawn<HumanController>(CellToWorld(0, 6), "Human");


        //Managers.ObjectManager.Spawn<HumanController>(CellToWorld(1, 1), "Human");
        //Managers.ObjectManager.Spawn<HumanController>(CellToWorld(5, 5), "Human");


        

        //Debug.Log(WorldToCell(0, 0));
        //Debug.Log(WorldToCell(0.5f, -0.5f));
        //Debug.Log(WorldToCell(1.1f, -1.1f));
        //
        //Debug.Log(GetAdjustedCellCenterWorld(1.1f, -1.1f));
        //Debug.Log(GetAdjustedCellCenterWorld(0.5f, -0.5f));

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public static Vector2 WorldToCell(float x, float y)
    {




        Vector2 cell = new Vector2((int)(x / _cellSize), - (int)(y / _cellSize));


        return cell;

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


}
