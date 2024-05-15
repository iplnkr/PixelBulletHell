using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    //map movement
    private float moveSpeed = 350f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool invince = false;
    [SerializeField] private TextMesh damageTaken;
    private int damageTakenVal = 0;
    [SerializeField] private GameObject birdImage;

    //camera shake
    private CinemachineVirtualCamera vCam;
    private CinemachineBasicMultiChannelPerlin perlinNoise;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        vCam = FindObjectOfType<CinemachineVirtualCamera>();
        perlinNoise = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
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
        perlinNoise.m_AmplitudeGain = 5;
        for(int i = 0; i < 12; i++)
        {
            birdImage.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1 - birdImage.GetComponent<SpriteRenderer>().color.a);
            //perlinNoise.m_AmplitudeGain = perlinNoise.m_AmplitudeGain - 0.25f;
            yield return new WaitForSeconds(0.075f);
        }
        birdImage.GetComponent<SpriteRenderer>().color = Color.white;        
        perlinNoise.m_AmplitudeGain = 0;
        invince = false;
    }
}
