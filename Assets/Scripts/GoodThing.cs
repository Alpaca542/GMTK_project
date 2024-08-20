using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class GoodThing : MonoBehaviour
{
    public Vector3 ScaleFactor;
    [SerializeField] public float changeSize = 0.2f;
    [SerializeField] public float changeMas = 0.2f;
    private ManagePoints mngPoints;
    public GameObject myParticles;
    public int myValue; // good things have a Neg-score and bad things Have Pos-score

    private void Start()
    {
        mngPoints = GameObject.FindGameObjectWithTag("mngPoints").GetComponent<ManagePoints>();
        ScaleFactor = new Vector2(changeSize, changeSize);
        GoodThingFinder goodThingFinder = FindObjectOfType<GoodThingFinder>();
        goodThingFinder.RegisterGoodThing(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Tornado")
        {
            FindObjectOfType<Tornado>().Grow();
            GameObject.FindGameObjectWithTag("mngPoints").GetComponent<ManagePoints>().AddToScore(myValue);
            Instantiate(myParticles, transform.position, Quaternion.identity);
            CameraShaker.Instance.ShakeOnce(1.5f, 2f, 0.5f, 1f);
            GetComponent<soundManager>().PlaySound(0, 0.8f, 1.2f, 1f);
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        GoodThingFinder goodThingFinder = FindObjectOfType<GoodThingFinder>();
        if (goodThingFinder != null)
        {
            goodThingFinder.UnregisterGoodThing(this);
        }
    }

    private void Grow(GameObject gmb)
    {
        gmb.transform.DOScale(gmb.transform.localScale + ScaleFactor, 0.5f);
        gmb.GetComponent<Rigidbody2D>().mass += 0.2f;
    }
}
