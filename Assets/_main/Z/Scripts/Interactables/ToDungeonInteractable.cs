using UnityEngine;

public class ToDungeonInteractable : Interactable {
  public Transform menuCameraTargetTransform1;
  public Transform menuCameraTargetTransform2;
  public GameObject leftDoor;
  public GameObject rightDoor;
  public string sceneName = "";

  public override void Interact(CharacterZ character) {
    bool going = false;

    if (character.cameraController && menuCameraTargetTransform1) {
      character.cameraController.SetLerpTarget(menuCameraTargetTransform1, 1).setOnComplete(() => {
        if (leftDoor) LeanTween.rotateY(leftDoor, -125, 0.25f);
        if (rightDoor) LeanTween.rotateY(rightDoor, 125, 0.25f);
        if (menuCameraTargetTransform2) character.cameraController.SetLerpTarget(menuCameraTargetTransform2, 0.5f);
        if (sceneName.Length > 0) character.gameManager?.GoToScene(sceneName);
      });

      going = true;
    }

    if (!going && sceneName.Length > 0)
      character.gameManager?.GoToScene(sceneName);
  }
}
