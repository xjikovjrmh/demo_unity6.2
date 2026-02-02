using System.Collections.Generic;
using System.Collections;
using UnityEngine;


public class GUI_demo : MonoBehaviour
{
    public GameObject obj;
   
    public Rect startBtnRect;
    public Rect deleteBtnRect;
    public GUIContent btnContent;
    public GUIContent btn2Content;
    public GUIStyle btnStyle;
    private bool isAutoCreating = false;//是否自动创建
    private Coroutine autoCreateCoroutine;
    //初始位置和高
    Vector3 position;
    float height;
    //创建的物体列表
    private List<GameObject> createdObjects = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startBtnRect  = new Rect(30, 40, 200, 100);
        deleteBtnRect = new Rect(30, 160, 200, 100);
        btnContent = new GUIContent();
        btn2Content = new GUIContent();
       
       
        btnStyle.alignment = TextAnchor.MiddleCenter;//居中


        obj = ABMgr.GetInstance().LoadRes<GameObject>("model", "Cylinder");
        //createdObjects.Add(obj);
        height = obj.GetComponent<MeshRenderer>().bounds.size.y;
    }

    
    // Update is called once per frame
    private void OnGUI()
    {
        
        // 合并的切换按钮
        if (isAutoCreating)
        {
            btnContent.text = "暂停";
        }
        else
        {
            btnContent.text = "自动";
        }
        if (GUI.Button(startBtnRect, btnContent, btnStyle))
        {
            ToggleAutoCreation();   //点击的时候会改变状态
        }

        btn2Content.text = "删除";
        if(GUI.Button(deleteBtnRect, btn2Content, btnStyle))
        {
            position -= new Vector3(0, 0, 4 * height);
            GameObject obj =createdObjects[createdObjects.Count - 1];
            createdObjects.Remove(obj);
            Destroy(obj);
        }
      


    }
    private void ToggleAutoCreation()
    {
        isAutoCreating = !isAutoCreating;
        if (isAutoCreating)
        {
            //自动创建
            if(autoCreateCoroutine==null)
            {
                autoCreateCoroutine = StartCoroutine(AutoCreateObjects());
            }
        }
        else
        {
            //暂停自动创建
            if(autoCreateCoroutine!=null)
            {
                StopCoroutine(autoCreateCoroutine);
                autoCreateCoroutine = null;
            }
        }
    }

    private IEnumerator AutoCreateObjects()
    { 
        while(true)
        {
            CreateNextObject();
            yield return new WaitForSeconds(1);
        }
    }
    private void CreateNextObject()
    {
        Debug.Log("自动前进");
        position += new Vector3(0, 0, 4 * height);
        GameObject newObj = Instantiate(obj, obj.transform.position + position, obj.transform.rotation);
        createdObjects.Add(newObj);
    }


}
