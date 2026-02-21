using UnityEngine;

public class BezierTest : MonoBehaviour
{
   public LineRenderer lineRenderer;
   public Transform point1;
   public Transform point2; 
   public Transform point3;
   public Transform point4;
   private int sampleCount = 50; // 采样点数量
    void Start()
    {
        if (lineRenderer != null)
        {
            // 设置顶点数量为采样点数
            lineRenderer.positionCount = sampleCount;
            // 可选：设置LineRenderer的宽度，让线条更明显
            // lineRenderer.startWidth = 0.1f;
            // lineRenderer.endWidth = 0.1f;
        }
        else
        {
            Debug.LogError("请为LineRenderer组件赋值！");
        }

    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0;i<50;i++)
        {
            Vector3 point =BezierUtility.BezierIntepolate3(point1.position,point2.position,point3.position,(i/50.0f));
            if(point4!=null)
            {
                point = BezierUtility.BezierIntepolate4(point1.position,point2.position,point3.position,point4.position,(i/50.0f));
            }
            lineRenderer.SetPosition(i,point);
        }
        
    }
}
