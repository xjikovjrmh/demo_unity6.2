using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class KeyBoardInput : MonoBehaviour
{
    public Vector3 spawnOffset = new Vector3(0, 0, 4); // 每次生成的偏移
    public GameObject CylinderPrefab;
    
    [Header("生成起始点（建议拖入空物体作为参考点）")]
    public Vector3 spawnOriginPos=new Vector3(0,2,0); // 可选：用场景中的一个空物体作为起点

    private Vector3 nextSpawnPosition; // ✅ 用 Vector3 记录下一个生成位置
    public bool isAutoSpawning = false;
    private Coroutine spawnCoroutine;
    public float deltaTime = 1f;

    void Start()
    {
        // 初始化生成起点：优先用 spawnOrigin，否则用当前脚本所在位置
        nextSpawnPosition = spawnOriginPos+spawnOffset;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            ToggleAutoSpawn();
        }
    }

    void ToggleAutoSpawn()
    {
        if (isAutoSpawning)
        {
            StopCoroutine(spawnCoroutine);
            isAutoSpawning = false;
            Debug.Log("自动生成已停止");
        }
        else
        {
            if (CylinderPrefab == null)
            {
                CylinderPrefab = ABMgr.GetInstance().LoadRes<GameObject>("model", "Cylinder");
                CylinderPrefab.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                CylinderPrefab.transform.position+= new Vector3(0, 2, 0);
                if (CylinderPrefab == null)
                {
                    Debug.LogError("无法加载 Cylinder 预制体！");
                    return;
                }
            }

            spawnCoroutine = StartCoroutine(AutoSpawnRoutine());
            isAutoSpawning = true;
            Debug.Log("自动生成已启动");
        }
    }

    IEnumerator AutoSpawnRoutine()
    {
        while (true)
        {
            Quaternion cylinderRotation = Quaternion.Euler(90f, 0f, 0f);
            // ✅ 在当前位置生成
            Instantiate(CylinderPrefab, nextSpawnPosition, cylinderRotation);
            
            // ✅ 累加偏移，为下一次生成做准备
            nextSpawnPosition += spawnOffset;

            yield return new WaitForSeconds(deltaTime);
        }
    }
}