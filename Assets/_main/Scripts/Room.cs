using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Assertions;

public class Room : MonoBehaviour
{
    public Transform Player { private set; get; }

    public GameObject ObstacleContainer;
    public Transform[] RoomObstacles { private set; get; }

    [Header("Enemy Setup")]
    public GameObject[] EnemyPrefabs;
    public List<GameObject> Enemies { private set; get; }
    public GameObject SpawnPointContainer;
    private Transform[] spawnPoints;

    private float width;
    private float height;

    [Header("Events")]
    public UnityEngine.Events.UnityEvent OnStart;
    public UnityEngine.Events.UnityEvent<Room> OnEnd;

    private bool roomBeat;
    private int killCount;

    private void Awake()
    {
        width = GetComponent<BoxCollider>().size.x * transform.localScale.x;
        height = GetComponent<BoxCollider>().size.z * transform.localScale.z;

        spawnPoints = SpawnPointContainer.GetComponentsInChildren<Transform>();
        RoomObstacles = ObstacleContainer.GetComponentsInChildren<Transform>();

        roomBeat = false;
        Enemies = new List<GameObject>();

        AssertionCheck();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (roomBeat) return;

        if (other.transform.CompareTag("Player"))
        {
            Player = other.transform;
            OnStart?.Invoke();
            SpawnInEnemies();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (roomBeat) return;

        if (other.transform.CompareTag("Player"))
        {
            Player = null;
            RemoveAllEnemies();
            OnEnd?.Invoke(this);
        }
    }

    public float GetWidth()
    {
        return width;
    }

    public float GetHeight()
    {
        return height;
    }

    private void SpawnInEnemies()
    {
        for (int i = 0; i < EnemyPrefabs.Length; i++)
        {
            GameObject newEnemy = Instantiate(EnemyPrefabs[i], spawnPoints[i + 1].position, Quaternion.identity);

            if (newEnemy.GetComponent<Enemy>())
              newEnemy.GetComponent<Enemy>().PatrolRoom = this;
            if (newEnemy.GetComponent<Health>())
              newEnemy.GetComponent<Health>().OnDeath += RemoveDeadEnemies;
            if (newEnemy.GetComponent<EnemyBehavior>())
              newEnemy.GetComponent<EnemyBehavior>().onDeath += RemoveDeadEnemies;

            Enemies.Add(newEnemy);

            newEnemy.SetActive(true);
        }
    }

    private void RemoveDeadEnemies()
    {
        for(int i = Enemies.Count - 1; i >= 0; i--)
        {
            if((Enemies[i].GetComponent<Health>() && Enemies[i].GetComponent<Health>().IsDead())
            || (Enemies[i].GetComponent<CharacterZ>() && Enemies[i].GetComponent<CharacterZ>().GetNormalizedHealth() <= 0))
            {
                Enemies.RemoveAt(i);
            }
        }

        if (!roomBeat && Enemies.Count == 0)
        {
            roomBeat = true;
            OnEnd?.Invoke(this);
        }
    }
    
    private void RemoveAllEnemies()
    {
        for(int i = Enemies.Count - 1; i >= 0; i--)
        {
            Enemies.RemoveAt(i);
        }
    }

    private void AssertionCheck()
    {
        Assert.IsTrue(spawnPoints.Length - 1 == EnemyPrefabs.Length, "The number of spawn points must be equal to the number of enemies");
    }
}
