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

        SendCommandToAI(human);
    }
  
    void SendCommandToAI(HumanController human)
    {
        //choose command

        int randNum = GetRandCommandNumber();

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
                human.Wait(2);
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
}
