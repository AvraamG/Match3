using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
    public int xIndex;
    public int yIndex;
    bool isMoving = false;

    public void SetCoordinates(int xCoordinate,int yCoordinate)
    {
        xIndex = xCoordinate;
        yIndex = yCoordinate;
    }

    public void Move(int destX,int destY, float timeToMove)
    {
        if (!isMoving)
        {
            StartCoroutine(MoveRoutine(new Vector3(destX, destY, 0), timeToMove));

        }
    }

    public AnimationCurve lerpCurve;

    //Compared to an UpdateMethod this Coroutine will run a couple of times and never check it again. An update will check every frame if it should move.
    IEnumerator MoveRoutine (Vector3 destination, float timeToMove)
    {
        Vector3 startPosition = transform.position;
        bool reachedDestination = false;
        float elapsedTime = 0;
        isMoving = true;
        while (!reachedDestination)
        {

            //this will go infinite so I am checking for a small threshold to snap it there.
            if (Vector3.Distance(transform.position,destination)<0.01f)
            {
                reachedDestination = true;
                transform.position = destination;
                SetCoordinates((int)destination.x, (int)destination.y);
                break;
            }
            elapsedTime += Time.deltaTime;

            //how close should I be to 0-1 a.k.a start or end.
            float t = Mathf.Clamp( elapsedTime / timeToMove,0f,1f);

            //Ease out.
            //  t = Mathf.Sin(t * Mathf.PI * 0.5f);

            //Ease in
            // t = 1-Mathf.Sin(t * Mathf.PI * 0.5f);

            //SmoothStep
            t = t * t * (3 - 2 * t);

            this.transform.position = Vector3.Lerp(this.transform.position, destination, t);
            //wait till next frame to resume execution
            yield return null;
        }
        isMoving = false;
    }
}
