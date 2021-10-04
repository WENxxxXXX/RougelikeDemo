using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType { DefaultRoom, FightRoom, TreasureRoom, BloodRoom, KeyRoom, 
                       BoomRoom,TrapRoom, BossRoom }
/// <summary>
/// 
/// </summary>
public class Room : MonoBehaviour
{
    public int DoorNumber
    {
        get
        {
            return doorNumber;
        }
    }

    [SerializeField] GameObject leftDoor, rightDoor, upDoor, downDoor;
    [SerializeField] GameObject treasure, blood, key, boom, trap;

    public bool roomLeft, roomRight, roomUp, roomDown;
    public int stepToStart = 0;
    public RoomType roomType;
    int doorNumber = 0;
    bool isFirstEnter = true;

    private void OnEnable()
    {
        EnemyManager.Instance.onNoEnemy += SetDoor;
    }

    private void OnDisable()
    {
        EnemyManager.Instance.onNoEnemy -= SetDoor;

    }

    public void SetupDoor(int xOffset, int yOffset)
    {
        leftDoor.SetActive(roomLeft);
        rightDoor.SetActive(roomRight);
        upDoor.SetActive(roomUp);
        downDoor.SetActive(roomDown);

        if (roomLeft) doorNumber++;
        if (roomRight) doorNumber++;
        if (roomUp) doorNumber++;
        if (roomDown) doorNumber++;

        stepToStart = (int)Mathf.Abs(transform.position.x / xOffset) + (int)Mathf.Abs(transform.position.y / yOffset);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CameraController.Instance.ChangeTarget(this.transform);
            if (isFirstEnter)
            {
                SetDoor(true);
                switch (roomType)
                {
                    case RoomType.FightRoom:
                        EnemyManager.Instance.GenerateRandomEnemy(Random.Range(2, 4), transform.position);
                        break;
                    case RoomType.TreasureRoom:
                        Instantiate(treasure, transform.position, Quaternion.identity);
                        break;
                    case RoomType.KeyRoom:
                        Instantiate(key, transform.position, Quaternion.identity);
                        break;
                    case RoomType.BloodRoom:
                        Instantiate(blood, transform.position, Quaternion.identity);
                        break;
                    case RoomType.BoomRoom:
                        Instantiate(boom, transform.position, Quaternion.identity);
                        if (roomUp) upDoor.GetComponent<Collider2D>().enabled = true; 
                        if (roomDown) downDoor.GetComponent<Collider2D>().enabled = true; 
                        if (roomLeft) leftDoor.GetComponent<Collider2D>().enabled = true; 
                        if (roomRight) rightDoor.GetComponent<Collider2D>().enabled = true; 
                        break;
                    case RoomType.TrapRoom:
                        Instantiate(trap, transform.position, Quaternion.identity);
                        break;
                }
                isFirstEnter = false;
            }
        }
    }

    void SetDoor(bool isClose)
    {
        if (roomUp) upDoor.GetComponent<Collider2D>().enabled = isClose;
        if (roomDown) downDoor.GetComponent<Collider2D>().enabled = isClose;
        if (roomLeft) leftDoor.GetComponent<Collider2D>().enabled = isClose;
        if (roomRight) rightDoor.GetComponent<Collider2D>().enabled = isClose;
    }
}
