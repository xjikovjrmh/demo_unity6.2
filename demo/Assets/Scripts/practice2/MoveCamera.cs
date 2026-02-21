using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform godcameraPosition;
    public Transform carcameraPosition;

    private Transform currentPosition;
    public bool isGodView = true;


    void Start()
    {
        // 初始化前先检查锚点是否赋值
        if (godcameraPosition == null)
        {
            Debug.LogError("MoveCamera：godcameraPosition未赋值！请拖入上帝视角锚点物体", this);
            return;
        }
        if (carcameraPosition == null)
        {
            Debug.LogError("MoveCamera：carcameraPosition未赋值！请拖入小车视角锚点物体", this);
            return;
        }

        currentPosition = godcameraPosition;//初始化啊！！
    }
    private void Update()//Update要大写！！！！！！！
    {
        if(currentPosition == null) return;
        
        transform.position = currentPosition.position;
        
        
    }
    // 提供给ViewSwitchManager调用的方法，切换锚点
    public void SwitchAnchor(bool toGodView)
    {
        isGodView = toGodView;
        // 切换前检查锚点是否有效
        if (toGodView && godcameraPosition != null)
        {
            currentPosition = godcameraPosition;
        }
        else if (!toGodView && carcameraPosition != null)
        {
            currentPosition = carcameraPosition;
        }
        else
        {
            Debug.LogError("切换锚点失败：目标锚点未赋值！", this);
        }
    }
}
