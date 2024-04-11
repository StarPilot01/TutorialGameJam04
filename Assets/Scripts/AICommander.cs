using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * AI 하는 모든 행동을 관장
 */
public class AICommander
{
    enum EAICommand
    { 
        Move,
        Wait,
        Length
    
    }

    HashSet<HumanController> _humans = new HashSet<HumanController>();

    Vector2 FindSuitableMoveDestination()
    {
        


        //일단 랜덤하게 만들었음
        return GameMap.GetEmptyCellRandomly();
    }

    public void RequestCommand(HumanController human)
    {
        _humans.TryGetValue(human, out HumanController requestedHuman);

        if(human != null)
        {
            SendCommandToAI(human);

        }
    }
  
    void SendCommandToAI(HumanController human)
    {
        //choose command

        //int randNum = GetRandCommandNumber();

        //TODO : 나중에 바꿈
        int randNum = Random.Range(0, 3);
        if(randNum == 2)
        {
            randNum = 0;
        }

        //int randNum = 0;

        switch ((EAICommand)randNum)
        { 
            case EAICommand.Move:
                {
                    Vector2 cellPos = FindSuitableMoveDestination();
                    human.MoveToDest(cellPos);
                }


                break;
            case EAICommand.Wait:
                human.Wait(1.5f);
                break;

            default:
                Debug.Assert(false);
                break;

        
        }

    }

    int GetRandCommandNumber()
    {
        int randNum = Random.Range(0, (int)EAICommand.Length);

        

        return randNum;


    }

    public void NotifyDead(HumanController human)
    {
        _humans.Remove(human);
    }

    public void AddHuman(HumanController human)
    {
        _humans.Add(human);
    }

    public void StopAllHuman()
    {


        foreach(HumanController human in _humans)
        {
            human.StopAllCoroutines();
            human.StopAllAction();

          

        }


        _humans.Clear();

        //Debug.Log(_humans.Count);
    }

    public void ResetAll()
    {
        foreach (HumanController human in _humans)
        {
            human.StopAllCoroutines();
            human.StopAllAction();

            Managers.ObjectManager.Despawn(human);

        }


        _humans.Clear();
    }
}
