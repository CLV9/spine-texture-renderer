using Spine.Unity;
using UnityEngine;

namespace CLVP.Tools.SpineTextureRenderer
{
    public class SpineRendererCamera : MonoBehaviour
    {
        private SkeletonAnimation _spineAnimationInstance;
        
        public Camera CreatedCamera { get; private set; }
        
        private void OnPreCull()
        {
            _spineAnimationInstance.gameObject.layer = LayerMask.NameToLayer("SpineRendererVisible");
        }

        private void OnPostRender()
        {
            _spineAnimationInstance.gameObject.layer = LayerMask.NameToLayer("SpineRendererInvisible");
        }

        public void Init(RenderTexture texture, SkeletonAnimation spineAnimation)
        {
            _spineAnimationInstance = spineAnimation;
            CreatedCamera = gameObject.AddComponent<Camera>();
            CreatedCamera.orthographic = true;
            CreatedCamera.targetTexture = texture;
            CreatedCamera.cullingMask = 1 << LayerMask.NameToLayer("SpineRendererVisible");
        }
    }
}