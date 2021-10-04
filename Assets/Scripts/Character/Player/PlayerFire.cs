using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    enum FireDirection { Up, Down, Left, Right };
    bool upFire;
    bool downFire;
    bool leftFire;
    bool rightFire;
    float remainFireInterval;
    PlayerStatus playerStatus;
    [SerializeField] float fireInterval;
    [SerializeField] GameObject playerProjectileUp;
    [SerializeField] GameObject playerProjectileDown;
    [SerializeField] GameObject playerProjectileLeft;
    [SerializeField] GameObject playerProjectileRight;
    [SerializeField] GameObject releasedBoom;
    [SerializeField] Transform firePoint;

    private void Awake()
    {
        playerStatus = GetComponent<PlayerStatus>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerStatus.boomNumber > 0)
        {
            Instantiate(releasedBoom, transform.position, Quaternion.identity);
        }
        ConfirmFireDirection();
        Fire();
        if (remainFireInterval > 0f)
        {
            remainFireInterval -= Time.deltaTime;
        }
    }

    /// <summary>
    /// 确定开火方向，即四个bool值。
    /// </summary>
    private void ConfirmFireDirection()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeFireDirection(FireDirection.Up);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeFireDirection(FireDirection.Down);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeFireDirection(FireDirection.Left);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeFireDirection(FireDirection.Right);
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            upFire = false;
            UpdateFireDirection();
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            downFire = false;
            UpdateFireDirection();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            leftFire = false;
            UpdateFireDirection();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            rightFire = false;
            UpdateFireDirection();
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition)
                - transform.position;
            if (direction.x > 0 && Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                ChangeFireDirection(FireDirection.Right);
            else if (direction.x < 0 && Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                ChangeFireDirection(FireDirection.Left);
            else if (direction.y > 0 && Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
                ChangeFireDirection(FireDirection.Up);
            else if (direction.y < 0 && Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
                ChangeFireDirection(FireDirection.Down);
        }

        if (Input.GetMouseButtonUp(0))
        {
            upFire = false; downFire = false; leftFire = false; rightFire = false;
        }
    }

    /// <summary>
    /// 按下上下左右攻击键时，切换开火方向
    /// </summary>
    /// <param name="fireDirection"></param>
    void ChangeFireDirection(FireDirection fireDirection)
    {
        switch (fireDirection)
        {
            case FireDirection.Up:
                upFire = true; downFire = false; leftFire = false; rightFire = false;
                break;
            case FireDirection.Down:
                upFire = false; downFire = true; leftFire = false; rightFire = false;
                break;
            case FireDirection.Left:
                upFire = false; downFire = false; leftFire = true; rightFire = false;
                break;
            case FireDirection.Right:
                upFire = false; downFire = false; leftFire = false; rightFire = true;
                break;
        }
    }

    /// <summary>
    /// 松开按键时，更新开火方向
    /// </summary>
    void UpdateFireDirection()
    {
        if (Input.GetKey(KeyCode.UpArrow)) upFire = true;
        else if (Input.GetKey(KeyCode.DownArrow)) downFire = true;
        else if (Input.GetKey(KeyCode.LeftArrow)) leftFire = true;
        else if (Input.GetKey(KeyCode.RightArrow)) rightFire = true;
    }

    void Fire()
    {
        if (remainFireInterval <= 0f)
        {
            if (upFire)
            {
                PoolManager.Release(playerProjectileUp, firePoint.position,
                    Quaternion.identity).GetComponent<Projectile>().SetOwner(playerStatus);
                remainFireInterval = fireInterval;
            }
            else if (downFire)
            {
                PoolManager.Release(playerProjectileDown, firePoint.position,
                    Quaternion.identity).GetComponent<Projectile>().SetOwner(playerStatus);
                remainFireInterval = fireInterval;
            }
            else if (leftFire)
            {
                PoolManager.Release(playerProjectileLeft, firePoint.position,
                    Quaternion.identity).GetComponent<Projectile>().SetOwner(playerStatus);
                remainFireInterval = fireInterval;
            }
            else if (rightFire)
            {
                PoolManager.Release(playerProjectileRight, firePoint.position,
                    Quaternion.identity).GetComponent<Projectile>().SetOwner(playerStatus);
                remainFireInterval = fireInterval;
            }
        }
    }
}
