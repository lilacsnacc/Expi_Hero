using UnityEngine;
using UnityEngine.EventSystems;

public class GamePauseMenu : BaseMenu {
  public RectTransform titleImage;
  public RectTransform emptyButton;
  public RectTransform backButton;
  public RectTransform toggleButton;
  public RectTransform quitButton;
  public CanvasGroup buttonCanvasGroup;

  public override LTDescr Open(EventSystem eventSystem) {
    base.Open(eventSystem);

    LTDescrList.Add(LeanTween.alpha(titleImage, 1, 0).setOnComplete(() => {
      eventSystem.SetSelectedGameObject(emptyButton.gameObject);

      LTDescrList.Add(LeanTween.moveX(emptyButton, 100, 0.5f));
      LTDescrList.Add(LeanTween.moveX(backButton, 100, 0.5f));
      LTDescrList.Add(LeanTween.moveX(toggleButton, 100, 0.6f));
      LTDescrList.Add(LeanTween.moveX(quitButton, 100, 0.7f));

      LTDescrList.Add(LeanTween.alphaCanvas(buttonCanvasGroup, 1, 0.5f));
    }));

    return LTDescrList[0];
  }

  public override LTDescr Close() {
    base.Close();

    LTDescrList.Add(LeanTween.alpha(titleImage, 0, 1f));

    LTDescrList.Add(LeanTween.moveX(emptyButton, 500, 0.5f));
    LTDescrList.Add(LeanTween.moveX(backButton, 500, 0.5f));
    LTDescrList.Add(LeanTween.moveX(toggleButton, 500, 0.4f));
    LTDescrList.Add(LeanTween.moveX(quitButton, 500, 0.3f));

    LTDescrList.Add(LeanTween.alphaCanvas(buttonCanvasGroup, 0, 0.2f));

    return LTDescrList[0];
  }
}
