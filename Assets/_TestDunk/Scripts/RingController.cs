using System.Collections;
using UnityEngine;
using DG.Tweening;

public class RingController : MonoBehaviour
{
    public int myRingNumber;

    [SerializeField] private BallCatcher ballCatcher;
    [SerializeField] private RingRotator ringRotator;
    [SerializeField] private TrajectoryRenderer trajectoryRenderer;
    [SerializeField] private GameObject star;
    [SerializeField] private float power = 5f;

    private Rigidbody2D ballRigidbody;

    [Header("Variables to Setup begin and end shot")]
    [SerializeField] private float startScale = 2.1f;
    [SerializeField] private float endScale = 3f;
    [SerializeField] private float sqrMagnitudeBetweenDirections = 1.5f;
    [SerializeField] private float timeToChange = 0.1f;

    private void Start()
    {
        if (Random.Range(0, 5) == 4)
        {
            star.SetActive(true);
        }
    }
    void Update()
    {
        if (ballCatcher.isCatch == true && Time.timeScale == 1)
        {
            ballRigidbody = ballCatcher.ballRigidbody;

            if (Input.GetMouseButtonDown(0))
            {
                ringRotator.isActive = true;
            }
            else if (Input.GetMouseButton(0))
            {
                if (CalculateSqrMagnitude() > sqrMagnitudeBetweenDirections)
                {
                    trajectoryRenderer.ShowTrajectory(trajectoryRenderer.transform.position, CalculateBallSpeed());
                    transform.DOScaleY(endScale, timeToChange);
                }
                else
                {
                    trajectoryRenderer.ClearPointsTrajectory();
                    transform.DOScaleY(startScale, timeToChange);
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ringRotator.isActive = false;
                trajectoryRenderer.ClearPointsTrajectory();
                transform.DOScaleY(startScale, timeToChange);

                if (CalculateSqrMagnitude() > sqrMagnitudeBetweenDirections)
                {
                    Shot();
                    ballRigidbody = null;
                }
            }
        }
    }

    private void Shot()
    {
        ballRigidbody.bodyType = RigidbodyType2D.Dynamic;
        ballRigidbody.AddForce(CalculateBallSpeed(), ForceMode2D.Impulse);
        ballCatcher.isCatch = false;
        StartCoroutine(OnBallCatcherCollider());
    }

    private Vector2 CalculateBallSpeed()
    {
        return (ringRotator.CurrentMouseDirection - ringRotator.StartMouseDirection + Vector2.right) * power;
    }

    private float CalculateSqrMagnitude()
    {
        return (ringRotator.CurrentMouseDirection - ringRotator.StartMouseDirection + Vector2.right).sqrMagnitude;
    }
    IEnumerator OnBallCatcherCollider()
    {
        yield return new WaitForSeconds(0.6f);
        ballCatcher.circleCollider.enabled = true;
    }
}
