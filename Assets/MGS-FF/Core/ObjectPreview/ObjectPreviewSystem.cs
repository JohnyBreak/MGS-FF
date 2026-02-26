using UnityEngine;

public class ObjectPreviewSystem
{
    //подхожу к предмету, взаимодействую с ним, он говорит системе, что его подобрали
    //выключаю управление персонажем и камерой
    //включаю канвас и объект рендер парент
    //меняю ему слой, перемещаю объект в обджект парент, ставлю в нужную мозицию и поворот, включаю анимацию дотвином
    //показываю текст
    //через время выключаю и возвращаю управление
    
    private readonly PreviewView _view;
    private readonly ObjectRenderParent _objectRenderParent;

    public ObjectPreviewSystem(PreviewView view, ObjectRenderParent objectRenderParent)
    {
        _view = view;
        _objectRenderParent = objectRenderParent;
        
        _view.Toggle(false);
        _objectRenderParent.Toggle(false);
        //GameState.GameState.SetState(GameState.GameState.State.Dialogue);
    }

    public void Preview(GameObject interactable)
    {
        GameState.GameState.SetState(GameState.GameState.State.Dialogue);
        _view.SetText("Item found");
        _objectRenderParent.SetObject(interactable);
        _objectRenderParent.Toggle(true);
        _view.Toggle(true);
        _objectRenderParent.StartAnim(() =>
        {
            Object.Destroy(interactable);
            Stop();
        });
    }

    private void Stop()
    {
        _objectRenderParent.Toggle(false);
        _view.Toggle(false);
        GameState.GameState.SetState(GameState.GameState.State.GamePlay);
    }
}
