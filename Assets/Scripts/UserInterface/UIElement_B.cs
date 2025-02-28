using System.Threading.Tasks;
using LionUitlity;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

[UxmlElement]
public abstract partial class UIElement_B : VisualElement {
    public Task InitializeTask { get; private set; }

    public UIElement_B(string path)
    {
        InitializeTask = Initialize(path);
    }

    private async Task Initialize(string path)
    {
        ResourceRequest handle = default;
        if (!string.IsNullOrEmpty(path)) {
            handle = Resources.LoadAsync(path);
        }
        else {
            Debug.LogError($"{name} failed initialize");
            return;
        }
        
        await Uitlity.WaitUntil(() => handle.isDone);

        if (handle.asset != null && handle.asset is VisualTreeAsset visualTreeAsset) {

            #region 親エレメントの初期化

            var container = visualTreeAsset.Instantiate();
            container.style.width = Length.Percent(100);
            container.style.height = Length.Percent(100);
            this.RegisterCallback<KeyDownEvent>(e => e.StopImmediatePropagation());
            pickingMode = PickingMode.Ignore;
            container.RegisterCallback<KeyDownEvent>(e => e.StopImmediatePropagation());
            container.pickingMode = PickingMode.Ignore;
            hierarchy.Add(container);

            #endregion

            // UI要素の取得
            await Initialize_S(container);
            Debug.Log("ウィンドウは正常にロード完了");
        }
        else {
            Debug.LogError($"Failed to load UXML file from Pass:{path}");
        }
    }

    protected abstract Task Initialize_S(TemplateContainer container);
}