using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager current;

    [SerializeField]
    private int activeLevel;
    public int Level => activeLevel - 1;
    public LevelData[] LevelDatas;

    private RoomConnectorType lastConnectedType = RoomConnectorType.LR;
    public int height;
    private List<Room> activeRooms;
    public CinemachineVirtualCamera cam;
    PolygonCollider2D polyCollider => GetComponent<PolygonCollider2D>();

	public void Start()
	{
        if (current == null)
            current = this;
        else
        {
            Destroy(this);
        }
    }
	private void Awake()
	{
	}

    public void SetLevel(int level) => activeLevel = level;
    public void IncrementLevel() => activeLevel++;

    public void CreateLevel()
    {
        cam = GameObject.FindWithTag("PlayerCam").GetComponent<CinemachineVirtualCamera>();
        polyCollider.enabled = false;
        activeRooms = new List<Room>();
        for (int i = 0; i < LevelDatas[Level].levelLength; i++)
        {
            if (i == 0)
                SelectRoom(LevelDatas[Level].startRooms);
            else if (i == LevelDatas[Level].levelLength - 1)
                SelectRoom(LevelDatas[Level].endRooms);
            else
                SelectRoom(LevelDatas[Level].rooms);
        }

        EditRooms();
        CreateRooms();


        List<Vector2> points = new List<Vector2>
		{
			new Vector2(-5, -5),
			new Vector2(5, -5),
			new Vector2(5, height),
			new Vector2(-5, height)
		};
		polyCollider.SetPath(0, points);
        cam.GetComponent<CinemachineConfiner>().m_BoundingShape2D = polyCollider;

        DisperseObjects(LevelDatas[Level].enemies, LevelDatas[Level].enemyCount);
        SpawnObjects(LevelDatas[Level].intObjects, LevelDatas[Level].intObjectCount);
    }


	public void SelectRoom(GameObject[] _rooms)
	{
        int tries = 0;
        int r = Random.Range(0, _rooms.Length);
        while (_rooms[r].GetComponent<Room>().entryConnector != lastConnectedType && tries < 100)
        {
            r = Random.Range(0, _rooms.Length);
            tries++;
        }
        if (tries < 100)
        {
            activeRooms.Add(_rooms[r].GetComponent<Room>());
            lastConnectedType = _rooms[r].GetComponent<Room>().exitConnector;
        }
		else
		{
            GameManager.current.ReloadCurrentLevel();
		}
    }

    public void EditRooms()
    {
        int tries = 0;
        for (int i = 0; i < LevelDatas[Level].upgrades; i++)
        {
            int roomToReplace = Random.Range(0, activeRooms.Count);
            while (!activeRooms[roomToReplace].replaceable)
                roomToReplace = Random.Range(0, activeRooms.Count);

            int rUpgrade = Random.Range(0, LevelDatas[Level].upgradeRooms.Length);
            while ((LevelDatas[Level].upgradeRooms[rUpgrade].GetComponent<Room>().entryConnector != activeRooms[roomToReplace - 1].GetComponent<Room>().exitConnector || LevelDatas[Level].upgradeRooms[rUpgrade].GetComponent<Room>().exitConnector != activeRooms[roomToReplace + 1].GetComponent<Room>().entryConnector) && tries < 100)
            {
                rUpgrade = Random.Range(0, LevelDatas[Level].upgradeRooms.Length);
                tries++;
            }
            if (tries < 100)
                activeRooms[roomToReplace] = LevelDatas[Level].upgradeRooms[rUpgrade].GetComponent<Room>();
            else
            {
                GameManager.current.ReloadCurrentLevel();
            }
        }

        tries = 0;
        for (int i = 0; i < LevelDatas[Level].shops; i++)
        {
            int shoproom = Random.Range(0, activeRooms.Count);
            while (!activeRooms[shoproom].replaceable)
                shoproom = Random.Range(0, activeRooms.Count);

            int rShop = Random.Range(0, LevelDatas[Level].shopRooms.Length);
            while ((LevelDatas[Level].shopRooms[rShop].GetComponent<Room>().entryConnector != activeRooms[shoproom - 1].GetComponent<Room>().exitConnector || LevelDatas[Level].shopRooms[rShop].GetComponent<Room>().exitConnector != activeRooms[shoproom + 1].GetComponent<Room>().entryConnector) && tries < 100)
            {
                rShop = Random.Range(0, LevelDatas[Level].shopRooms.Length);
                tries++;
            }
            if (tries < 100)
                activeRooms[shoproom] = LevelDatas[Level].shopRooms[rShop].GetComponent<Room>();
            else
            {
                GameManager.current.ReloadCurrentLevel();
            }
        }

        tries = 0;
        for (int i = 0; i < LevelDatas[Level].forcedFights; i++)
        {
            int fightRoom = Random.Range(0, activeRooms.Count);
            while (!activeRooms[fightRoom].replaceable)
                fightRoom = Random.Range(0, activeRooms.Count);

            int rFight = Random.Range(0, LevelDatas[Level].forcedFightRooms.Length);
            while ((LevelDatas[Level].forcedFightRooms[rFight].GetComponent<Room>().entryConnector != activeRooms[fightRoom - 1].GetComponent<Room>().exitConnector || LevelDatas[Level].forcedFightRooms[rFight].GetComponent<Room>().exitConnector != activeRooms[fightRoom + 1].GetComponent<Room>().entryConnector) && tries < 100)
            { 
                rFight = Random.Range(0, LevelDatas[Level].forcedFightRooms.Length);
                tries++;
            }

            if (tries < 100)
                activeRooms[fightRoom] = LevelDatas[Level].forcedFightRooms[rFight].GetComponent<Room>();
            else
            {
                GameManager.current.ReloadCurrentLevel();
            }
        }

    }

    public void CreateRooms()
	{
        for (int i = 0; i < activeRooms.Count; i++) 
        {
            activeRooms[i] = Instantiate(activeRooms[i], new Vector3(0, height, 0), Quaternion.identity).GetComponent<Room>();
            activeRooms[i].Randomize();
            height += activeRooms[i].height;
            activeRooms[i].Init();
        }
    }
    void SpawnObjects(GameObject[] objects, int objectCount)
	{
        for (int i = 0; i < objectCount; i++)
		{
            int r = Random.Range(0, objects.Length);
            Vector2Int pos = new Vector2Int(Random.Range(-4, 4), Random.Range(10, height - activeRooms[activeRooms.Count-1].height - 5));
            Collider2D[] spawnCheck = Physics2D.OverlapCircleAll(pos, 0.8f);
            while (spawnCheck.Length != 0)
			{
                pos = new Vector2Int(Random.Range(-4, 4), Random.Range(10, height - activeRooms[activeRooms.Count - 1].height - 5));
                spawnCheck = Physics2D.OverlapCircleAll(pos, 0.8f);
            }
            Instantiate(objects[r], new Vector3(pos.x, pos.y), Quaternion.identity);
        }
    }

    void DisperseObjects(GameObject[] objects, int objectCount)
	{
        //get total object count
        //while spawned objects < object count
        //for loop through rooms
        //spawn a small batch of enemies if room is spawnable
        int spawnedObjects = 0;
        int tries = 0;
        while (spawnedObjects < objectCount)
		{
            tries++;
            Debug.Log($" Try Spawn {tries} Times");

            int i = Random.Range(0, activeRooms.Count);
            while (!activeRooms[i].GetComponent<Room>().randomEnemies)
                i = Random.Range(0, activeRooms.Count);

            if (activeRooms[i].randomEnemies)
            {
                int enemyBatch = Random.Range(1, 4);
                if (spawnedObjects + enemyBatch > objectCount)
                    enemyBatch = objectCount - spawnedObjects;
                Debug.Log($"Batch of {enemyBatch} enemies in Room {i}");
                for (int currentEnemyBatch = 0; currentEnemyBatch < enemyBatch; currentEnemyBatch++)
                {
                    int spawnTries = 0;
                    int r = Random.Range(0, objects.Length);
                    Vector2  pos = activeRooms[i].transform.position + new Vector3(Random.Range(-4, 4), Random.Range(-activeRooms[i].height * 0.5f, activeRooms[i].height*0.5f));
                    Collider2D[] spawnCheck = Physics2D.OverlapCircleAll(pos, 0.8f);

                    while (spawnCheck.Length != 0 && spawnTries < 10)
                    {
                        pos = activeRooms[i].transform.position + new Vector3(Random.Range(-4, 4), Random.Range(-activeRooms[i].height * 0.5f, activeRooms[i].height*0.5f));
                        spawnCheck = Physics2D.OverlapCircleAll(pos, 0.8f);
                        spawnTries++;
                    }

                    if (spawnTries < 10)
                    {
                        Instantiate(objects[r], new Vector3(pos.x, pos.y), Quaternion.identity);
                        spawnedObjects++;
                    }
                    //Debug.Log($"Spawned {spawnedObjects} enemies");
                }
            }

        }
	}
    void SelectLoot()
	{

	}

	public void ResetLevel()
	{
        height = 0;
        polyCollider.enabled = false;
        lastConnectedType = RoomConnectorType.LR;
        activeRooms = new List<Room>();
    }
}
