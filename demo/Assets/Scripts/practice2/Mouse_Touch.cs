using System.Collections.Generic;
using UnityEngine;

public class Mouse_Touch : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private List<GameObject> selectedobjs= new List<GameObject>();
    public Camera cam;
    public LayerMask raycastLayer = -1; // 默认所有层
    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) //鼠标左键按下
        {
              //out 额外返回信息
                if(TryRayCast(out RaycastHit hit))//检测到物体   如果没有击中物体将不能直接访问.gameObject，会报错
                {
                    GameObject clickedObj = hit.collider.gameObject;
                    if(selectedobjs.Contains(clickedObj))//已选中则取消选中
                    {
                        HighlightSystem2.Instance.DisableHighlight(clickedObj);
                        selectedobjs.Remove(clickedObj);//移除
                        Debug.Log("取消选中物体:"+clickedObj.name);
                    }
                    else
                    {
                    HighlightSystem2.Instance.EnableHightlight(clickedObj,Color.yellow,10f);
                    selectedobjs.Add(clickedObj);//添加
                    Debug.Log("选中物体:"+clickedObj.name);
                    }
                    
               
                }
        }

        // 🗑️ Delete 键：删除或隐藏选中物体
        if(Input.GetKeyDown(KeyCode.Delete))
        {
             if ( selectedobjs.Count > 0)
        {
            DeleteOrHideSelectedObject();
        }
        else
        {
            Debug.Log("未选中任何物体，无法删除或隐藏。");
        }
        }
       

    }

    private void DeleteOrHideSelectedObject()
    {
        for(int i=selectedobjs.Count-1;i>=0;i--)
        {
            GameObject obj = selectedobjs[i];
            if(obj==null)continue;
            string objName = obj.name;


            obj.SetActive(false); // 隐藏物体
            Debug.Log("已隐藏物体: " + objName);

        HighlightSystem2.Instance.RemoveHighlight(obj); // 移除高亮
        }
        selectedobjs.Clear(); // 清空选择
    }
    //摄像检测封装，
    private bool TryRayCast(out RaycastHit hit)
    {
        //将屏幕坐标转为世界空间射线
        Ray ray = cam.ScreenPointToRay(Input.mousePosition); //从摄像机发出一条经过鼠标位置的射线
         return Physics.Raycast(ray, out hit,1000f,raycastLayer);  //如果命中，则返回 true，并将命中的信息存储在 hit 中
                                        //最大距离限制     //检测层
    }



}
