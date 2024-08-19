using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodThing : MonoBehaviour
{
    public Vector3 ScaleFactor;
    [SerializeField] public float changeSize = 0.2f;
    [SerializeField] public float changeMas = 0.2f;
    private ManagePoints mngPoints;
    public int myValue; // good things have a Neg-score and bad things Have Pos-score

    private void Start()
    {
        mngPoints = GameObject.FindGameObjectWithTag("mngPoints").GetComponent<ManagePoints>();
        ScaleFactor = new Vector2(changeSize, changeSize);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Tornado")
        {
            FindObjectOfType<Tornado>().Grow();
            GameObject.FindGameObjectWithTag("mngPoints").GetComponent<ManagePoints>().AddToScore(myValue);
            Destroy(gameObject);
        }
    }

    private void Grow(GameObject gmb)
    {
        gmb.transform.DOScale(gmb.transform.localScale + ScaleFactor, 0.5f);
        gmb.GetComponent<Rigidbody2D>().mass += 0.2f;
    }
}
