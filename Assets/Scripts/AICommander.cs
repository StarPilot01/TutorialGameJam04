using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //인간에게 전달할 목표좌표 설정하는 곳, 현재는 임의로 5,5로 해놨는데 나중에 랜덤하게 바꿀 예정
        //
        return new Vector2(5, 5);
    }

    public void RequestCommand(HumanController human)
    {
        _humans.TryGetValue(human, out HumanController requestedHuman);

        SendCommandToAI(human);
    }
  
    void SendCommandToAI(HumanController human)
    {
        //choose command

        //int randNum = GetRandCommandNumber();

        int randNum = 0;

        switch ((EAICommand)randNum)
        { 
            case EAICommand.Move:
                {
                    Vector2 cellPos = FindSuitableMoveDestination();
                    human.MoveToDest(cellPos);
                }


                break;
            case EAICommand.Wait:
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
}
