using UnityEditor;
using UnityEngine;
using DG.Tweening;

public class wizard : MonoBehaviour
{
    [Header("Stats")]
    public float speed;

    [Header("Fields")]
    public Rigidbody2D rb;
    public GameObject wand;
    public Animator anim;

    [Header("Debug")]
    public GameObject currentReflector;
    public Sprite[] idleSprites;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleMovement();
        HandleWandToggle();

    }

    private void HandleMovement()
    {
        float dirX = Input.GetAxis("Horizontal");
        float dirY = Input.GetAxis("Vertical");

        float dirXRaw = Input.GetAxisRaw("Horizontal");
        float dirYRaw = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(dirXRaw) > 0 || Mathf.Abs(dirYRaw) > 0)
        {
            anim.enabled = true;
            if (Mathf.Abs(dirYRaw) > 0)
            {
                if (dirYRaw > 0)
                {
                    anim.SetBool("GoRight", false);
                    anim.SetBool("GoLeft", false);
                    anim.SetBool("GoUp", true);
                    anim.SetBool("GoDown", false);
                }
                else
                {
                    anim.SetBool("GoRight", false);
                    anim.SetBool("GoLeft", false);
                    anim.SetBool("GoUp", false);
                    anim.SetBool("GoDown", true);
                }
            }
            else
            {
                if (dirXRaw > 0)
                {
                    anim.SetBool("GoRight", true);
                    anim.SetBool("GoLeft", false);
                    anim.SetBool("GoUp", false);
                    anim.SetBool("GoDown", false);
                }
                else
                {
                    anim.SetBool("GoRight", false);
                    anim.SetBool("GoLeft", true);
                    anim.SetBool("GoUp", false);
                    anim.SetBool("GoDown", false);
                }
            }
        }
        else
        {
            anim.enabled = false;
            Debug.Log(anim.GetBool("GoRight"));
            if (anim.GetBool("GoRight"))
            {
                GetComponent<SpriteRenderer>().sprite = idleSprites[0];
            }
            if (anim.GetBool("GoUp"))
            {
                GetComponent<SpriteRenderer>().sprite = idleSprites[1];
            }
            if (anim.GetBool("GoLeft"))
            {
                GetComponent<SpriteRenderer>().sprite = idleSprites[2];
            }
            if (anim.GetBool("GoDown"))
            {
                GetComponent<SpriteRenderer>().sprite = idleSprites[3];
            }
        }
        rb.velocity = new Vector2(dirX, dirY) * speed;
    }

    private void HandleWandToggle()
    {
        if (Input.GetMouseButton(0))
        {
            if (GetComponent<AudioSource>().volume != 0.6f)
            {
                GetComponent<AudioSource>().DOFade(0f, 0.6f);
            }
            wand.SetActive(true);
        }
        else
        {
            if (GetComponent<AudioSource>().volume != 0)
            {
                GetComponent<AudioSource>().DOFade(0f, 0.6f);
            }
            wand.SetActive(false);
        }
    }
}
