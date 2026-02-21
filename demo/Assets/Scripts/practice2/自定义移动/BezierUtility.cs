using System.Collections.Generic;
using UnityEngine;

public class BezierUtility //工具类，不继承MonoBehaviour
{
    //v = (1-t)^2P0 +2t(1-t)P1 + t^2P2;  二次贝塞尔曲线公式                                                    
    //贝塞尔曲线计算公式
    internal static Vector3 CalculateBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        return Mathf.Pow(1 - t, 2) * p0 + 2 * (1 - t) * t * p1 + Mathf.Pow(t, 2) * p2;
    }

    //插值
    internal static Vector3 BezierIntepolate3(Vector3 p0,Vector3 p1,Vector3 p2,float t)
    {

        //p0p1 = p0 + t*(p1-p0); // 
        Vector3 p0p1 = Vector3.Lerp(p0, p1, t); // 计算P0和P1之间的中点
        Vector3 p1p2 = Vector3.Lerp(p1, p2, t); // 计算P1和P2之间的中点
        return Vector3.Lerp(p0p1, p1p2, t); // 计算中点之间的中点，即贝塞尔曲线上的点
    }
    internal static Vector3 BezierIntepolate4(Vector3 p0,Vector3 p1,Vector3 p2,Vector3 p3,float t)
    {

        //p0p1 = p0 + t*(p1-p0); // 
        Vector3 p0p1 = Vector3.Lerp(p0, p1, t); // 计算P0和P1之间的中点
        Vector3 p1p2 = Vector3.Lerp(p1, p2, t); // 计算P1和P2之间的中点
        Vector3 p2p3 = Vector3.Lerp(p2, p3, t); // 计算P2和P3之间的中点
        Vector3 px = Vector3.Lerp(p0p1, p1p2, t); // 计算P0P1和P1P2之间的中点
        Vector3 py = Vector3.Lerp(p1p2, p2p3, t);
        return Vector3.Lerp(px, py, t);
    }

    internal static List<Vector3>BezierINterpolate3List(Vector3 p0,Vector3 p1,Vector3 p2,float pointCount)
    {
        List<Vector3>pointList = new List<Vector3>();
        for(int i=0;i<pointCount;i++)//
        {
            pointList.Add(BezierIntepolate3(p0,p1,p2,(i/pointCount)));
        }
        return pointList;
    }

    //给出整体路径
    internal static List<Vector3>BezierINterpolate4List(Vector3 p0,Vector3 p1,Vector3 p2,Vector3 p3,float pointCount)
    {
        List<Vector3>pointList = new List<Vector3>();
        for(int i=0;i<pointCount;i++)//
        {
            pointList.Add(BezierIntepolate4(p0,p1,p2,p3,(i/pointCount)));
        }
        return pointList;
    }

    
}
