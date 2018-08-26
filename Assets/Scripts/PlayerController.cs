using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    
    private Rigidbody rb;
    [SerializeField]
    private float speed;

    public int count;

    public int CCount()
    {
        return count;
    }

    private void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        count = 0;

    }

    public int numbers;


    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed * Time.deltaTime);

        numbers = CCount();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {            
            Destroy(other.gameObject);/*.SetActive(false) */
            count = count + 1;

        }

        if(other.gameObject.CompareTag("Finish"))
        {
            Invoke("Restart", 0f);
        }
        if (other.gameObject.CompareTag("Kill"))
        {
            GameManager.instance.GameOver();
        }
            
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
    }


}