using UnityEngine;

namespace FirstPersonView
{
    public class FPV_FirstPersonCamera : FPV_Camera
    {
        void Awake()
        {
            SetCamera();
            UpdateStaticCamera();
        }

        /// <summary>
        /// Manualy update the static first person view camera variable.
        /// </summary>
        public override void UpdateStaticCamera()
        {
            FPV.firstPersonCamera = this;
        }
        
        /// <summary>
        /// Pre Cull Method.
        /// Signal that the First Person Camera is going to start the rendering process
        /// </summary>
        void OnPreCull()
        {
            FPV_Renderer_Base.isWorldCameraRendering = false;
            FPV_Renderer_Base.isFPVCameraRendering = true;
        }

        /// <summary>
        /// On Post Render.
        /// Signal that the First Person Camera is no longer rendering.
        /// </summary>
        void OnPostRender()
        {
            FPV_Renderer_Base.isFPVCameraRendering = false;
        }
    }
}