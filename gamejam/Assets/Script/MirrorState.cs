using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MirrorState : MonoBehaviour
{
    // 레이저 최적화 등의 이유로 거울을 새로 제작
    private DirectionConst.Direction direction;
    private Transform graphics;

    public void Awake()
    {
        graphics = transform.GetChild(0);
        //float zAngle = transform.rotation.eulerAngles.z;
        //SetDirection(zAngle);
    }

    public void OnMouseDown()
    {
        Debug.Log("거울 터치");
        SwitchDirection();
    }

    // =====================

    public void SwitchDirection()
    {
        if (this.direction == DirectionConst.Direction.Up)
        {
            SetDirection(DirectionConst.Direction.LeftUp);
        }
        else if (this.direction == DirectionConst.Direction.LeftUp)
        {
            SetDirection(DirectionConst.Direction.Left);
        }
        else if (this.direction == DirectionConst.Direction.Left)
        {
            SetDirection(DirectionConst.Direction.LeftDown);
        }
        else if (this.direction == DirectionConst.Direction.LeftDown)
        {
            SetDirection(DirectionConst.Direction.Up);
        }
    }
    
    public void SetDirection(DirectionConst.Direction direction)
    {
        this.direction = direction;
        SynchronizeZAngle();
    }

    public DirectionConst.Direction GetDirection()
    {
        return direction;
    }

    public void SetDirection(float zAngle)
    {
        float standard = 45;
        float halfStandard = standard / 2;

        zAngle %= 180;
        if (zAngle < halfStandard)
        {
            this.direction = DirectionConst.Direction.Up;
        }
        else if (zAngle < halfStandard * 3)
        {
            this.direction = DirectionConst.Direction.LeftUp;
        }
        else if (zAngle < halfStandard * 5)
        {
            this.direction = DirectionConst.Direction.Left;
        }
        else if (zAngle < halfStandard * 7)
        {
            this.direction = DirectionConst.Direction.LeftDown;
        }
        else
        {
            this.direction = DirectionConst.Direction.Up;
        }

        SynchronizeZAngle();
    }

    private void SynchronizeZAngle()
    {
        //Quaternion angle = graphics.rotation;
        Quaternion angle = transform.rotation;
        Vector3 eulerAngle = angle.eulerAngles;

        if (this.direction == DirectionConst.Direction.Up)
        {
            eulerAngle.z = 0 - eulerAngle.z;
        }
        else if (this.direction == DirectionConst.Direction.LeftUp)
        {
            eulerAngle.z = 45 - eulerAngle.z;
        }
        else if (this.direction == DirectionConst.Direction.Left)
        {
            eulerAngle.z = 90 - eulerAngle.z;
        }
        else if (this.direction == DirectionConst.Direction.LeftDown)
        {
            eulerAngle.z = 135 - eulerAngle.z;
        }
        else
        {
            // 방향이 DirectionConst.Direction 중 없는 경우
            Debug.Log("MirrorState: 거울방향 이상");
        }

        transform.Rotate(eulerAngle, Space.World);
        //graphics.Rotate(eulerAngle, Space.World);

    }
}
