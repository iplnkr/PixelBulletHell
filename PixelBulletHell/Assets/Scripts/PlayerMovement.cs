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
    [SerializeField] private AudioSource damageSound;

    //mirage
    private float mirageCooldown = 0.5f;
    private Vector3[] last3Pos = new Vector3[3];
    private Vector3[] last3Rot = new Vector3[3];
    [SerializeField] private GameObject mirage1;
    [SerializeField] private GameObject mirage2;
    [SerializeField] private GameObject mirage3;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        vCam = FindObjectOfType<CinemachineVirtualCamera>();
        perlinNoise = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        //last3Transforms[0] = birdImage.transform;
        //last3Transforms[1] = birdImage.transform;
        //last3Transforms[2] = birdImage.transform;
        mirage1.transform.parent = null;
        mirage2.transform.parent = null;
        mirage3.transform.parent = null;
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

        
        //mirage stuff
        mirageCooldown -= Time.fixedDeltaTime;
        if(mirageCooldown <= 0)
        {
            mirageCooldown = 0.1f;
            last3Pos[2] = last3Pos[1];
            last3Pos[1] = last3Pos[0];
            last3Pos[0] = birdImage.transform.position;
            last3Rot[2] = last3Rot[1];
            last3Rot[1] = last3Rot[0];
            last3Rot[0] = birdImage.transform.eulerAngles;
            mirage3.GetComponent<SpriteRenderer>().sprite = mirage2.GetComponent<SpriteRenderer>().sprite;
            mirage2.GetComponent<SpriteRenderer>().sprite = mirage1.GetComponent<SpriteRenderer>().sprite;
            mirage1.GetComponent<SpriteRenderer>().sprite = mirage3.GetComponent<SpriteRenderer>().sprite;
            if(last3Pos[2] != null)
            {
                mirage3.transform.position = last3Pos[2];
                mirage3.transform.localScale = birdImage.transform.lossyScale;
                mirage3.transform.eulerAngles = last3Rot[2];
            }
            if(last3Pos[1] != null)
            {
                mirage2.transform.position = last3Pos[1];
                mirage2.transform.localScale = birdImage.transform.lossyScale;
                mirage2.transform.eulerAngles = last3Rot[1];
            }
            if(last3Pos[0] != null)
            {
                mirage1.transform.position = last3Pos[0];
                mirage1.transform.localScale = birdImage.transform.lossyScale;
                mirage1.transform.eulerAngles = last3Rot[0];
            }
        }

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
        damageSound.Play();
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
