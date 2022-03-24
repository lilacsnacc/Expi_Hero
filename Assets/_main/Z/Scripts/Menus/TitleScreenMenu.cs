using UnityEngine;
using UnityEngine.EventSystems;

public class TitleScreenMenu : BaseMenu {
    public RectTransform titleImage;
    public RectTransform emptyButton;
    public RectTransform startButton;
    public RectTransform optionsButton;
    public RectTransform quitButton;
    public RectTransform whiteRect;

    public override LTDescr Open(EventSystem eventSystem) {
        base.Open(eventSystem);
        
        LTDescrList.Add(LeanTween.moveX(titleImage, 100, 1f).setOnComplete(() => {
            eventSystem.SetSelectedGameObject(emptyButton.gameObject);

            LTDescrList.Add(LeanTween.alpha(whiteRect, 1, 0));
            LTDescrList.Add(LeanTween.alpha(whiteRect, 0, 0.2f));
            LTDescrList.Add(LeanTween.moveX(emptyButton, 100, 0.5f));
            LTDescrList.Add(LeanTween.moveX(startButton, 100, 0.5f));
            LTDescrList.Add(LeanTween.moveX(optionsButton, 100, 0.6f));
            LTDescrList.Add(LeanTween.moveX(quitButton, 100, 0.7f));
        }).setDelay(.5f));

        return LTDescrList[0];
    }

    public override LTDescr Close() {
        base.Close();
        
        LTDescrList.Add(LeanTween.moveX(titleImage, -800, 0));
        LTDescrList.Add(LeanTween.alpha(whiteRect, 0, 0));
        LTDescrList.Add(LeanTween.moveX(emptyButton, -400, 0.5f));
        LTDescrList.Add(LeanTween.moveX(startButton, -400, 0.5f));
        LTDescrList.Add(LeanTween.moveX(optionsButton, -400, 0.4f));
        LTDescrList.Add(LeanTween.moveX(quitButton, -400, 0.3f));

        return LTDescrList[0];
    }
}
