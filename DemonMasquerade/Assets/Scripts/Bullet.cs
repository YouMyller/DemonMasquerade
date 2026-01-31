using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        Invoke("removeBullet", 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void removeBullet()
    {
        rb.linearVelocity = Vector3.zero;
        this.gameObject.SetActive(false);
        
    }

    
}
