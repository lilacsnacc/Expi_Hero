using UnityEngine;

public class RoomCounter : MonoBehaviour {
  public GameObject endGamePrefab;
  public GameObject toHubWorldPrefab;
  public AudioSource strangeSound;

  Room[] rooms;
  int finishedRooms;

  void Start() {
    rooms = GetComponentsInChildren<Room>();

    foreach (Room room in rooms)
      room.OnEnd.AddListener(plusOneDone);
  }

  public void plusOneDone(Room finishedRoom) {
    finishedRooms++;

    if (finishedRooms >= rooms.Length) {
      GameObject obj1 = Instantiate(endGamePrefab, finishedRoom.transform.position + Vector3.right * -2.5f, transform.rotation);
      GameObject obj2 = Instantiate(toHubWorldPrefab, finishedRoom.transform.position + Vector3.right * 2.5f, transform.rotation);

      strangeSound.Play();

      LeanTween.moveLocalY(obj1, 2, 2);
      LeanTween.moveLocalY(obj2, 2, 2);
    }
  }
}
