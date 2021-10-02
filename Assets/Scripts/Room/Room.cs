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
    [SerializeField] GameObject[] enemy, boss;
    [SerializeField] GameObject treasure, key;

    public bool roomLeft, roomRight, roomUp, roomDown;
    public int stepToStart = 0;
    int doorNumber = 0;
    public RoomType roomType;
    Vector3[] enemyPosition;

    private void Start()
    {
        enemyPosition = new Vector3[]
        {
            transform.position + new Vector3(7.8f, 3.4f, 0f),
            transform.position + new Vector3(7.8f, -3.4f, 0f),
            transform.position + new Vector3(-7.8f, 3.4f, 0f),
            transform.position + new Vector3(-7.8f, -3.4f, 0f),
            transform.position + new Vector3(-3f, 0f, 0f),
            transform.position + new Vector3(3f, 0f, 0f),
            transform.position + new Vector3(1.5f, 3f, 0f),
            transform.position + new Vector3(-1.5f, 3f, 0f),
            transform.position + new Vector3(-3f, -3f, 0f),
            transform.position + new Vector3(3f, -3f, 0f)
        };
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
            switch (roomType)
            {
                case RoomType.FightRoom:
                    for (int i = 0; i < 4; i++)
                    {
                        EnemyManager.Instance.AddToList(
                        PoolManager.Release(enemy[Random.Range(0, enemy.Length)], 
                            enemyPosition[Random.Range(0, enemyPosition.Length)], 
                            Quaternion.identity));
                    }
                    break;
                    //TODO:
            }
        }
    }
}
