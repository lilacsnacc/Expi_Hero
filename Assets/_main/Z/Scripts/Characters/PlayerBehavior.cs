using UnityEngine;

public class PlayerBehavior : MonoBehaviour {
  public static PlayerBehavior playerInstance;

  public CharacterZ myCharacter;

  protected int opposingLayer = 0;

  // thanks to zeid87 of StackOverflow for the snippet! <3
  void Awake() {
    if (playerInstance == null)
      playerInstance = this;
    else {
      playerInstance.GetComponent<CharacterZ>().TeleportTo(transform.position);
      playerInstance.transform.rotation = transform.rotation;

      Destroy(gameObject);
    }

    DontDestroyOnLoad(gameObject);
  }

  void Start() {
    myCharacter.AffectExpi(50);

    #if UNITY_EDITOR
      myCharacter.AffectStats(2, 9000, 10);
      myCharacter.AffectExpi(100);
    #endif

    opposingLayer = LayerMask.NameToLayer("Enemy Ability");

    myCharacter.gameManager = GameManager.gameManagerInstance;
  }

  void OnTriggerEnter(Collider other) {
    Ability otherLegacyAbility = other.GetComponent<Ability>();

    if (otherLegacyAbility && other.gameObject.layer == opposingLayer)
      myCharacter.AffectHealth(-otherLegacyAbility.Damage);
  }
}
