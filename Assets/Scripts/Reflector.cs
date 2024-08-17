using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class Reflector : MonoBehaviour, IDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public bool chosen;
    private bool pointerOver;
    public int myType;
    public GameObject myFlag;
    private float currentVelocity;
    private float JustHit;

    //Event handlers
    public void OnPointerEnter(PointerEventData eventData)
    {
        pointerOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pointerOver = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mpos.x, mpos.y, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!chosen)
        {
            chosen = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<wizard>().currentReflector = gameObject;
        }
    }


    //Code
    private void Start()
    {
        chosen = false;
    }

    void Update()
    {
        if (chosen)
        {
            if (Input.GetMouseButton(1))
            {
                LookAtMouse();
            }
        }

        if (Input.GetMouseButton(0) && !pointerOver)
        {
            chosen = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<wizard>().currentReflector = null;
        }

        JustHit -= Time.deltaTime;
    }

    void LookAtMouse()
    {
        Vector2 movement = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float targetAngle = Mathf.Atan2(-movement.x, movement.y) * Mathf.Rad2Deg + 180;
        float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.z, targetAngle, ref currentVelocity, 0.1f);
        transform.rotation = Quaternion.Euler(0, 0, smoothedAngle);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Tornado" && JustHit <= 0)
        {
            JustHit = 0.5f;

            if (myType == 2)
            {
                Shrink(other.gameObject);
            }
            else if (myType == 3)
            {
                Grow(other.gameObject);
            }
        }
    }

    IEnumerator Shrink(GameObject gmb)
    {
        gmb.GetComponent<Tornado>().Stop();
        gmb.transform.DOScale(gmb.transform.localScale * 0.8f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        gmb.GetComponent<Tornado>().Go();
    }

    IEnumerator Grow(GameObject gmb)
    {
        gmb.GetComponent<Tornado>().Stop();
        gmb.transform.DOScale(gmb.transform.localScale * 1.2f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        gmb.GetComponent<Tornado>().Go();
    }
}
