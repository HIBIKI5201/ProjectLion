using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

[UxmlElement]
public abstract partial class UIElement_B : VisualElement {
    public Task InitializeTask { get; private set; }
    protected string _addressablePath = string.Empty;

    public UIElement_B() => InitializeTask = Initialize();

    private async Task Initialize()
    {
        AsyncOperationHandle<VisualTreeAsset> handle = default;
        if (string.IsNullOrEmpty(_addressablePath)) {
            handle = Addressables.LoadAssetAsync<VisualTreeAsset>(_addressablePath);
        }
        else {
            Debug.LogError($"{name} failed initialize");
            return;
        }

        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded && handle.Result != null) {

            #region �e�G�������g�̏�����

            var treeAsset = handle.Result;
            var container = treeAsset.Instantiate();
            container.style.width = Length.Percent(100);
            container.style.height = Length.Percent(100);
            this.RegisterCallback<KeyDownEvent>(e => e.StopImmediatePropagation());
            pickingMode = PickingMode.Ignore;
            container.RegisterCallback<KeyDownEvent>(e => e.StopImmediatePropagation());
            container.pickingMode = PickingMode.Ignore;
            hierarchy.Add(container);

            #endregion

            // UI�v�f�̎擾
            await Initialize_S(container);

            Debug.Log("�E�B���h�E�͐���Ƀ��[�h����");
        }
        else {
            Debug.LogError("Failed to load UXML file from Addressables: UXML/BasicInformation.uxml");
        }

        // �������̉��
        Addressables.Release(handle);
    }

    protected abstract Task Initialize_S(TemplateContainer container);
}