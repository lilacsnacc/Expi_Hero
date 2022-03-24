using UnityEngine;

public class OpenSkillsInteractable : Interactable {
  public Transform playerSittingTargetTransform;
  public Transform menuCameraTargetTransform;

  public override void Interact(CharacterZ character) {
    character.animator.SetTrigger("Sit");

    if (playerSittingTargetTransform) {
      character.transform.position = playerSittingTargetTransform.position + playerSittingTargetTransform.forward * 0.25f;
      character.transform.LookAt(playerSittingTargetTransform.position + playerSittingTargetTransform.forward);
    }

    if (character.cameraController && menuCameraTargetTransform) {
      character.cameraController.SetLerpTarget(menuCameraTargetTransform, 2);
    }

    GameManager.gameManagerInstance.OpenSkillMenu();
  }
}
