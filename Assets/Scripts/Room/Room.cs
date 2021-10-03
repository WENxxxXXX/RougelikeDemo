using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType { DefaultRoom, FightRoom, TreasureRoom, KeyRoom, BossRoom }
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
    [SerializeField] GameObject treasure, key;

    public bool roomLeft, roomRight, roomUp, roomDown;
    public int stepToStart = 0;
    public RoomType roomType;
    int doorNumber = 0;
    bool isFirstEnter = true;

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
                switch (roomType)
                {
                    case RoomType.FightRoom:
                        EnemyManager.Instance.GenerateRandomEnemy(4, transform.position);
                        break;
                        //TODO:
                }
                isFirstEnter = false;
            }
        }
    }
}
