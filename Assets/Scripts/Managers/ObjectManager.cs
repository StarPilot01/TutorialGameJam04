using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static Define;


//���ҽ� �ҷ�����, ���� ���
public class ObjectManager
{
    Dictionary<string , UnityEngine.Object> _resources = new Dictionary<string , UnityEngine.Object>();

    #region ������Ʈ ����
   
    #endregion

    #region ���ҽ� �ε�, ����, ����
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


        

        //������ null return�Ͽ� ���� �˸�

        return prefab as T;
    }

    //TODO: �ʿ�� ���߿� pooling �߰�
    public GameObject Instantiate(string key, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>(key);

        //Dictionary�� ���ٴ� �� -> ����
        if (prefab == null)
        {
            Debug.LogError($"{key}�� �ҷ����µ� ������ �߻��߽��ϴ�.");
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
            Debug.LogError($"{go.name} ������Ʈ�� �������� �ʽ��ϴ�. (���� �Ұ���)");
        }

        Object.Destroy(go);
    }

    #endregion

    #region ����,����
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


    //Pooling ���� Spawn�� ���ؼ��� �ƴ� �ʿ� �������� ���� ��ġ�� �Ŀ� Despawn�� ȣ���� ��� ������ �� �� �ִ�.
    //�ʿ� ���� ��ġ�ߴٸ� Destroy , �ڵ�� Spawn�Ͽ� ��ġ�ߴٸ� Despawn
    //���� Pooling ���ϰ� ������ �����ص� ��
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
