using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform cameraPosition;

    private void Update()//Update要大写！！！！！！！
    {
        if (cameraPosition == null)
        {
            Debug.LogWarning("MoveCamera: cameraPosition is not assigned.", this);
            return;
        }

        transform.position = cameraPosition.position;
    }
}
