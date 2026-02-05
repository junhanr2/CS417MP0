using UnityEngine;

public class OrbitMoon : MonoBehaviour
{
    private float orbitSpeed = 30f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, orbitSpeed * Time.deltaTime, 0f, Space.World);


    }
}
