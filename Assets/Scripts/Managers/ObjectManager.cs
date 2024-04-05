using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static Define;


//리소스 불러오기, 생성 담당
public class ObjectManager
{
    Dictionary<string , UnityEngine.Object> _resources = new Dictionary<string , UnityEngine.Object>();

    #region 오브젝트 모음
   
    #endregion

    #region 리소스 로드, 생성, 제거
    public T Load<T>(string key) where T : UnityEngine.Object
    {
        if(_resources.TryGetValue(key, out Object resources))
        {
            return resources as T;
        }
        
        Object prefab = Resources.Load<T>(key);
            
        if(prefab == null)
        {
            return null;
        }

        _resources.Add(key, prefab);


        

        //없으면 null return하여 오류 알림

        return prefab as T;
    }

    //TODO: 필요시 나중에 pooling 추가
    public GameObject Instantiate(string key, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>(key);

        //Dictionary에 없다는 뜻 -> 오류
        if (prefab == null)
        {
            Debug.LogError($"{key}를 불러오는데 오류가 발생했습니다.");
            return null;
        }

        GameObject go = Object.Instantiate(prefab, parent);
        go.name = prefab.name;
        return go;

    }

    public void Destory(GameObject go)
    {
        if(go == null)
        {
            Debug.LogError($"{go.name} 오브젝트는 존재하지 않습니다. (제거 불가능)");
        }

        Object.Destroy(go);
    }

    #endregion

    #region 스폰,디스폰
    public T Spawn<T>(Vector3 position, string resourceName) where T : UnityEngine.Object
    {
        GameObject go = Instantiate(resourceName);

        Rigidbody2D rigid = go.GetComponent<Rigidbody2D>();

        if(rigid != null)
        {
            rigid.position = position;

        }
        else
        {
            go.transform.position = position;

        }

        System.Type type = typeof(T);

        


       


        go.GetComponent<ISpawnable>().OnSpawn();


        if (type == typeof(HumanController))
        {
            return go.GetComponent<HumanController>() as T;
        }
        else if(type == typeof(Palanquin))
        {
            return go.GetComponent<Palanquin>() as T;

        }


        return null;
    }


    //Pooling 사용시 Spawn을 통해서가 아닌 맵에 프리팹을 직접 배치한 후에 Despawn을 호출한 경우 문제가 될 수 있다.
    //맵에 직접 배치했다면 Destroy , 코드로 Spawn하여 배치했다면 Despawn
    //아직 Pooling 안하고 있으니 무시해도 됨
    public void Despawn<T>(T obj) where T: UnityEngine.Object
    {
        //System.Type type = typeof(T);
        



        Destory(obj.GameObject());

       
       
    }

    #endregion

    public ObjectManager()
    {
        Init();
    }

    public void Init()
    {
        
    }

}
