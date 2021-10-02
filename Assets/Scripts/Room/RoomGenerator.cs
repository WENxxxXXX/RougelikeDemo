using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Direction { Up, Down, Left, Right }

/// <summary>
/// 
/// </summary>
public class RoomGenerator : MonoBehaviour
{
    [Header("房间信息")]
    [SerializeField] GameObject roomPrefab;
    [SerializeField] int roomNumber;
    [SerializeField] Color startColor, endColor;
    [SerializeField] GameObject[] roomLayouts;

    [Header("位置控制")]
    [SerializeField] Transform generatorPointTF;
    [SerializeField] int xOffset, yOffset;

    List<Room> rooms;
    List<Vector3> existingRoomPos;
    Direction direction;
    GameObject endRoom;
    int maxStep = 0;
    List<GameObject> farRooms;
    List<GameObject> lessFarRooms;
    List<GameObject> oneDoorRooms;

    private void Awake()
    {
        rooms = new List<Room>();
        existingRoomPos = new List<Vector3>() { generatorPointTF.position };//已有房间列表，用于判断重复房间
        farRooms = new List<GameObject>();
        lessFarRooms = new List<GameObject>();
        oneDoorRooms = new List<GameObject>();
    }

    private void Start()
    {
        for (int i = 0; i < roomNumber; i++)
        {
            rooms.Add(Instantiate(roomPrefab, generatorPointTF.position, Quaternion.identity).GetComponent<Room>());

            //改变point位置
            if (i == roomNumber - 1)
                break;//生成最后一个房间后，无须在改变point位置
            ChangePointPos();
        }

        foreach (var room in rooms)
        {
            SetupRoom(room);
        }

        for (int i = 1; i < rooms.Count - 1; i++)
        {
            Instantiate(roomLayouts[Random.Range(0, roomLayouts.Length)],
                rooms[i].transform.position, Quaternion.identity);

            //TODO:待优化
            float temp = Random.Range(0f, 1f);
            if (temp >= 0f && temp <= 0.7f) rooms[i].roomType = RoomType.FightRoom;
            else if (temp > 0.7f && temp <= 0.85f) rooms[i].roomType = RoomType.TreasureRoom;
            else if (temp > 0.85f && temp < 1f) rooms[i].roomType = RoomType.KeyRoom;
        }

        //为首尾房间着色
        rooms[0].roomType = RoomType.DefaultRoom;
        FindEndRoom();
        endRoom.GetComponent<Room>().roomType = RoomType.BossRoom;
    }

    private void Update()
    {
        // if (Input.anyKeyDown)
        // {
        //     SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // }
    }

    void ChangePointPos()
    {
        do
        {
            direction = (Direction)Random.Range(0, 4);

            switch (direction)
            {
                case Direction.Up:
                    generatorPointTF.position += new Vector3(0, yOffset, 0);
                    break;
                case Direction.Down:
                    generatorPointTF.position += new Vector3(0, -yOffset, 0);
                    break;
                case Direction.Left:
                    generatorPointTF.position += new Vector3(-xOffset, 0, 0);
                    break;
                case Direction.Right:
                    generatorPointTF.position += new Vector3(xOffset, 0, 0);
                    break;
            }
        } while (IsExistingRoomPos(generatorPointTF.position));
        existingRoomPos.Add(generatorPointTF.position);
    }

    bool IsExistingRoomPos(Vector3 position)
    {
        foreach (var item in existingRoomPos)
        {
            if (Vector3.SqrMagnitude(item - position) < 1)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 设置房间的门
    /// </summary>
    void SetupRoom(Room newRoom)
    {
        newRoom.roomLeft = IsExistingRoomPos(newRoom.transform.position + new Vector3(-xOffset, 0, 0));
        newRoom.roomRight = IsExistingRoomPos(newRoom.transform.position + new Vector3(xOffset, 0, 0));
        newRoom.roomUp = IsExistingRoomPos(newRoom.transform.position + new Vector3(0, yOffset, 0));
        newRoom.roomDown = IsExistingRoomPos(newRoom.transform.position + new Vector3(0, -yOffset, 0));
        newRoom.SetupDoor(xOffset, yOffset);

        Object obj = Resources.Load(GetWallPrefabPath(newRoom));
        Instantiate(obj, newRoom.transform.position, Quaternion.identity);
    }

    string GetWallPrefabPath(Room newRoom)
    {
        string wallPrefabPath = "Prefabs/Walls/Wall_";
        if (newRoom.roomUp) wallPrefabPath += "U";
        if (newRoom.roomDown) wallPrefabPath += "D";
        if (newRoom.roomLeft) wallPrefabPath += "L";
        if (newRoom.roomRight) wallPrefabPath += "R";

        return wallPrefabPath;
    }

    void FindEndRoom()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].stepToStart > maxStep)
                maxStep = rooms[i].stepToStart;
        }

        foreach (var room in rooms)
        {
            if (room.stepToStart == maxStep)
                farRooms.Add(room.gameObject);
        }
        foreach (var room in rooms)
        {
            if (room.stepToStart == maxStep - 1)
                lessFarRooms.Add(room.gameObject);
        }
        foreach (var room in farRooms)
        {
            if (room.GetComponent<Room>().DoorNumber == 1)
                oneDoorRooms.Add(room);
        }
        foreach (var room in lessFarRooms)
        {
            if (room.GetComponent<Room>().DoorNumber == 1)
                oneDoorRooms.Add(room);
        }

        if (oneDoorRooms.Count != 0)
        {
            endRoom = oneDoorRooms[Random.Range(0, oneDoorRooms.Count)];
        }
        else
        {
            endRoom = farRooms[Random.Range(0, farRooms.Count)];
        }
    }

}
