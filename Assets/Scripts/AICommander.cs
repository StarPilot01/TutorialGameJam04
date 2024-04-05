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
        //�ΰ����� ������ ��ǥ��ǥ �����ϴ� ��, ����� ���Ƿ� 5,5�� �س��µ� ���߿� �����ϰ� �ٲ� ����
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
