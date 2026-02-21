using System.Collections.Generic;
using UnityEngine;

public class AlongPathMove : MonoBehaviour
{
    public float v;//m/s
    private List<Vector3>path = new List<Vector3>();
    public Transform point0;
    public Transform point1;
    public Transform point2;
    public Transform point3;

    private float totalLength;//路的总长度
    private float currentLength;//当前已经走过的长度
    private int index=0;//当前在哪一段路上

    public LineRenderer lineRenderer;


    void Start()
    {
       
    }
    Vector3 dir;
    Vector3 pos;
    // Update is called once per frame
    void Update()
    {
         path =BezierUtility.BezierINterpolate3List(point0.position,point1.position,point2.position,1000);
        lineRenderer.positionCount = path.Count;
        lineRenderer.SetPositions(path.ToArray());

        for(int i=0;i<path.Count-1;i++)
        {
            totalLength += Vector3.Distance(path[i], path[i + 1]);
        }

        float s=v*Time.time;//得到应该移动的距离

        if (currentLength < totalLength)
        {
            for(int i=index;i<path.Count-1;i++)
            {
                currentLength+= Vector3.Distance(path[i], path[i + 1]);
                if(currentLength>s)
                {
                    index = i;
                    currentLength -= Vector3.Distance(path[i], path[i + 1]);//有可能出现
                    dir = (path[i + 1] - path[i]).normalized;//得到当前路段的方向
                    pos = path[i] + dir * (s - currentLength);//得到当前位置
                    break;
                }
                transform.position = pos;
                transform.rotation=Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(dir,transform.up),Time.deltaTime*5);


            }
        }
    }
}
