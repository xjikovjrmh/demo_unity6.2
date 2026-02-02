using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ButtonTest : MonoBehaviour
{
    //图片
    [Range(0,1)]
    public float imageDisplay;
    public Image testImage;

    //按钮
    public Image buttonImage;
    public bool imageColorChange=false;

    //滑块和滑动条
    public Scrollbar scrollbar;
    public GameObject obj; //需要移动的物体

    public TMP_Text text1;

    private void Update()
    {
        ImageColorChange();
    }

    public void ImageColorChange()
    {
        testImage.fillAmount = imageDisplay;
    }
    
    public void ButtonClickTest()
    {
        if(!imageColorChange)
        {
            testImage.color = Color.red;
            imageColorChange = true;
        }
        else
        {
            testImage.color = Color.white;
            imageColorChange = false;
        }
    }

    public void TestChange(float value)
    {
        text1.text=value.ToString("F2");//value记录滑块的值
    }

    public void ScrollbarChange()
    {
        
        Vector3 newPos = new Vector3(scrollbar.value*1000f+514,188,0);
        obj.transform.position = newPos;
    }

    public void DropdownChange(int index)
    {
        switch(index)
        {
            case 0:
                Debug.Log("选择了第一个选项");
                break;
            case 1:
                Debug.Log("选择了第二个选项");
                break;
            case 2:
                Debug.Log("选择了第三个选项");
                break;
        }
    }
    
}
