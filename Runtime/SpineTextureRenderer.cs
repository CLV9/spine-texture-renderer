using Spine.Unity;
using UnityEngine;

namespace CLVP.Tools.SpineTextureRenderer
{
    public class SpineTextureRenderer : ISpineTextureRenderer
    {
        public ISpineTextureObject Render(SkeletonAnimation spinePrefab, Vector2Int resolution, Vector4 padding)
        {
            var spineTextureObject = new GameObject("SpineTextureRenderer").AddComponent<SpineTextureObject>();
            
            // Random far point
            spineTextureObject.transform.position = new Vector3(10000f, 10000f, 10000f);
            
            spineTextureObject.Init(spinePrefab, resolution, padding);
            return spineTextureObject;
        }
    }
}

