using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallCatcher : MonoBehaviour
{
    public int myRingNumber;

    public GameObject basketballGrid;
    public CircleCollider2D circleCollider;
    public bool isCatch = false;
    public Rigidbody2D ballRigidbody;

    [Header("Values for setup basketball grid")]
    [SerializeField] private float timeToReturnScale = 0.2f;
    [SerializeField] public float startScale = 2.1f;
    [SerializeField] private float endScale = 4f;

    private void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            ballRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            var thisBall = ballRigidbody.transform.GetComponentInChildren<Ball>();

            thisBall.timerToDie = 0;
            thisBall.OffEffects();

            ballRigidbody.transform.parent = transform;
            ballRigidbody.bodyType = RigidbodyType2D.Static;
            isCatch = true;
            circleCollider.enabled = false;

            AudioManager.instance.PlayOneShot(Clips.Catch);

            CounterManager.instance.SetNumberRingInBallCatcher(myRingNumber);

            basketballGrid.gameObject.transform.DOScaleY(endScale, timeToReturnScale);
            StartCoroutine(ReturnScale(timeToReturnScale));
        }
    }

    IEnumerator ReturnScale(float timeToReturnScale)
    {
        yield return new WaitForSeconds(timeToReturnScale);
        basketballGrid.gameObject.transform.DOScaleY(startScale, timeToReturnScale);
    }
}
