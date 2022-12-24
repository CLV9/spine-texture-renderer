using Spine.Unity;
using UnityEngine;

namespace CLVP.Tools.SpineTextureRenderer
{
    public interface ISpineTextureRenderer
    {
        ISpineTextureObject Render(SkeletonAnimation spinePrefab, Vector2Int resolution, Vector4 padding);
    }

    public interface ISpineTextureObject
    {
        RenderTexture Texture { get; }
        SkeletonAnimation SpineAnimationInstance { get; }

        void Dispose();
    }
}