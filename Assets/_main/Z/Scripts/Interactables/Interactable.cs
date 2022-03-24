using UnityEngine;

public class Interactable : MonoBehaviour {
  public string interactMessage = "";

  public virtual void Interact(CharacterZ character) { }

  void OnTriggerEnter(Collider other) {
    other.GetComponent<CharacterZ>()?.SetInteractable(this);
  }

  void OnTriggerExit(Collider other) {
    if (other.GetComponent<CharacterZ>()?.GetInteractable() == this)
      other.GetComponent<CharacterZ>().SetInteractable();
  }
}
