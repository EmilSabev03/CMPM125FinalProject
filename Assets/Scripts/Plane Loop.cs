//chatgpt helped with this script: https://chatgpt.com/share/67486629-aa2c-8001-af60-ee25b7ddab8e

using UnityEngine;

public class PlaneLoop : MonoBehaviour
{
    public Vector3 startPoint = new Vector3(0, 0, 0);
    public Vector3 endPoint = new Vector3(0, 0, 0);
    public float speed = 1000f;
    
    private void Start()
    {
        transform.position = startPoint;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, endPoint, speed * Time.deltaTime);

        if (transform.position == endPoint)
        {
            transform.position = startPoint;
        }
    }
}
