using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Transform playerTransfom;
    [SerializeField] float distance = -5f;
    [SerializeField] float verticalOffset = 3f;
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        Vector3 offset = new Vector3(0f,verticalOffset, distance);
        transform.position = playerTransfom.position + offset;
        transform.LookAt(playerTransfom);
    }
}
