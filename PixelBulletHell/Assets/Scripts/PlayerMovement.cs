using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //map movement
    private float moveSpeed = 250f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool invince = false;
    [SerializeField] private TextMesh damageTaken;
    private int damageTakenVal = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = (new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))).normalized;
        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            transform.eulerAngles = new Vector3(0, 90 - (90 * Mathf.Sign(Input.GetAxisRaw("Horizontal"))), 0);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = (movement * moveSpeed * Time.fixedDeltaTime); 
    }

    public void TakeDamage()
    {
        if(!invince)
        {
            invince = true;
            damageTakenVal++;
            damageTaken.text = "" + damageTakenVal;
            StartCoroutine(InvincibilityFlash());
        }
    }

    private IEnumerator InvincibilityFlash()
    {
        for(int i = 0; i < 12; i++)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1 - GetComponent<SpriteRenderer>().color.a);
            yield return new WaitForSeconds(0.075f);
        }
        GetComponent<SpriteRenderer>().color = Color.white;
        invince = false;
    }
}
