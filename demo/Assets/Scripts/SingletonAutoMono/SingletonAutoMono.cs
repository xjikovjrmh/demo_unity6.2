using UnityEngine;

public class SingletonAutoMono<T> : MonoBehaviour where T : MonoBehaviour//只有继承自MonoBehaviour基类的类才能传进来
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private static T instance;

    public static T GetInstance()
    {

        if (instance == null)
        {
            //动态挂载
            //在场景上场景空物体
            GameObject obj = new GameObject();
            obj.name = typeof(T).ToString();
            // 将对象改名为脚本名 ，方便编辑器 看到改对象
            DontDestroyOnLoad(obj);
            //为obj对象动态添加指定T类型的组件 返回组件实例赋值给instance
            instance = obj.AddComponent<T>();
            //过场景时不移除对象，保证其在整个生命周期都存在


        }
        return instance;// 改这里的逻辑


    }








}
