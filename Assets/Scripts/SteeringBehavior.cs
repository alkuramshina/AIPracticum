using UnityEngine;
using Random = UnityEngine.Random;

public static class SteeringBehavior
{
    private const float SlowingRadius = 10f;
    private const float WanderJitter = 40f;
    private const float WanderRadius = 1.2f;
    private const float WanderDistance = 2f;

    public static Vector3 Seek(Vector3 currentPosition,
        Vector3 targetPosition, float speed)
    {
        var desiredVelocity = targetPosition - currentPosition;
        var distance = Vector3.Distance(currentPosition, targetPosition);

        if (distance < SlowingRadius)
        {
            desiredVelocity = desiredVelocity.normalized * speed * (distance / SlowingRadius);
        }
        else
        {
            desiredVelocity = desiredVelocity.normalized * speed;
        }

        return desiredVelocity;
    }

    public static Vector3 Pursue(Vector3 currentPosition,
        Vector3 targetPosition,
        Vector3 targetVelocity, float speed)
    {
        var distance = Vector3.Distance(currentPosition, targetPosition);
        var ahead = distance / 10;
        var futurePosition = targetPosition + targetVelocity * ahead;

        return Seek(currentPosition, futurePosition, speed);
    }

    public static Vector3 Wander(Vector3 currentPosition, float speed)
    {
        var theta = Random.value * 2 * Mathf.PI;
        var wanderTarget = new Vector3(WanderRadius * Mathf.Cos(theta), 0f, WanderRadius * Mathf.Sin(theta));

        var jitter = WanderJitter * Time.deltaTime;
        
        wanderTarget += new Vector3(Random.Range(-1f, 1f) * jitter, 0f, Random.Range(-1f, 1f) * jitter);
        wanderTarget.Normalize();
        wanderTarget *= WanderRadius;

        var targetPosition = currentPosition + Vector3.right * WanderDistance + wanderTarget;

        return Seek(currentPosition, targetPosition, speed);
    }
}