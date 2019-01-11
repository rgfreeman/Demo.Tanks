using UnityEngine;

public class MoveBehavior : MonoBehaviour
{
    // constants
    public const float ROUND_TOLERANCE = 0.25f;

    // other game objects
    public MapBehaviour mapBehaviour;

    // set in inspector
    public float speed;
    public Vector2 size;

    void Update()
    {
        var vector = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

        // horizontal moving have priority
        if (Mathf.Abs(vector.x)>0 && Mathf.Abs(vector.y)>0)
        {
            vector.y = 0;
        }

        var delta = vector * Time.deltaTime * speed;
        var floorX = Mathf.Floor(transform.position.x);
        var floorY = Mathf.Floor(transform.position.y);
        var ceilX = Mathf.Ceil(transform.position.x);
        var ceilY = Mathf.Ceil(transform.position.y);

        // current cell index
        var currentX = Mathf.Round(transform.position.x);
        var currentY = Mathf.Round(transform.position.y);

        if (delta.x > 0)
        {
            // move right
            var newRight = TanksMathf.RoundDown(transform.position.x + size.x / 2 + delta.x);

            // aligment
            var d = transform.position.y - currentY;
            var indexOfNearest = (d == 0) ? 0 : Mathf.Sign(d);
            if
            (
                (int)currentX < mapBehaviour.mapSize.x - 1 &&
                Mathf.Abs(currentY - transform.position.y) <= ROUND_TOLERANCE &&
                mapBehaviour.map[(int)newRight, (int)currentY] == false &&
                mapBehaviour.map[(int)newRight, (int)(currentY + indexOfNearest)] == true
            )
            {
                RoundY();
            }

            if 
            (
                (int)newRight >= mapBehaviour.mapSize.x ||
                mapBehaviour.map[(int)newRight, (int)floorY] == true ||
                mapBehaviour.map[(int)newRight, (int)ceilY] == true
            )
            {
                // wall exist - rounding position to wall
                RoundX();
            }
            else
            {
                // move position
                transform.position += delta;
            }
        }

        if (delta.x < 0)
        {
            // move left
            var newLeft = TanksMathf.RoundDown(transform.position.x - size.x / 2 + delta.x);

            // aligment
            var d = transform.position.y - currentY;
            var indexOfNearest = (d == 0) ? 0 : Mathf.Sign(d);
            if
            (
                (int)currentX > 0 &&
                Mathf.Abs(currentY - transform.position.y) <= ROUND_TOLERANCE &&
                mapBehaviour.map[(int)newLeft, (int)currentY] == false &&
                mapBehaviour.map[(int)newLeft, (int)(currentY + indexOfNearest)] == true
            )
            {
                RoundY();
            }

            if
            (
                (int)newLeft < 0 ||
                mapBehaviour.map[(int)newLeft, (int)floorY] == true ||
                mapBehaviour.map[(int)newLeft, (int)ceilY] == true
            )
            {
                // wall exist - rounding position to wall
                RoundX();
            }
            else
            {
                // move position
                transform.position += delta;
            }
        }

        if (delta.y > 0)
        {
            // move up
            var newUp = TanksMathf.RoundDown(transform.position.y + size.y / 2 + delta.y);

            // aligment
            var d = transform.position.x - currentX;
            var indexOfNearest = (d == 0) ? 0 : Mathf.Sign(d);
            if
            (
                (int)currentY < mapBehaviour.mapSize.y - 1 &&
                Mathf.Abs(currentX - transform.position.x) <= ROUND_TOLERANCE &&
                mapBehaviour.map[(int)currentX, (int)newUp] == false &&
                mapBehaviour.map[(int)(currentX + indexOfNearest), (int)newUp] == true
            )
            {
                RoundX();
            }


            if 
            (
                (int)newUp >= mapBehaviour.mapSize.y ||
                mapBehaviour.map[(int)floorX, (int)newUp] == true ||
                mapBehaviour.map[(int)ceilX, (int)newUp] == true
            )
            {
                // wall exist - rounding position to wall
                RoundY();
            }
            else
            {
                // move position
                transform.position += delta;
            }
        }

        if (delta.y < 0)
        {
            // move down
            var newDown = TanksMathf.RoundDown(transform.position.y - size.y / 2 + delta.y);

            // aligment
            var d = transform.position.x - currentX;
            var indexOfNearest = (d == 0) ? 0 : Mathf.Sign(d);
            if
            (
                (int)currentY > 0 &&
                Mathf.Abs(currentX - transform.position.x) <= ROUND_TOLERANCE &&
                mapBehaviour.map[(int)currentX, (int)newDown] == false &&
                mapBehaviour.map[(int)(currentX + indexOfNearest), (int)newDown] == true
            )
            {
                RoundX();
            }

            if
            (
                (int)newDown < 0 ||
                mapBehaviour.map[(int)floorX, (int)newDown] == true ||
                mapBehaviour.map[(int)ceilX, (int)newDown] == true
            )
            {
                // wall exist - rounding position to wall
                RoundY();
            }
            else
            {
                // move position
                transform.position += delta;
            }
        }
    }

    void RoundX()
    {
        transform.position = new Vector3
        (
            Mathf.Round(transform.position.x),
            transform.position.y,
            0
        );
    }

    void RoundY()
    {
        transform.position = new Vector3
        (
            transform.position.x,
            Mathf.Round(transform.position.y),
            0
        );
    }
}
