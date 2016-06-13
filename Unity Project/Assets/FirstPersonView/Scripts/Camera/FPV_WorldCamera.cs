namespace FirstPersonView
{
    public class FPV_WorldCamera : FPV_Camera
    {
        void Awake()
        {
            SetCamera();
            UpdateStaticCamera();
        }

        /// <summary>
        /// Manualy update the static world camera variable.
        /// </summary>
        public override void UpdateStaticCamera()
        {
            FPV.worldCamera = this;
        }

        /// <summary>
        /// Pre Cull Method.
        /// Signal that the World Camera is going to start the rendering process
        /// </summary>
        void OnPreCull()
        {
            FPV_Renderer_Base.isWorldCameraRendering = true;
        }

        /// <summary>
        /// On Post Render.
        /// Signal that the World Camera is no longer rendering.
        /// </summary>
        //void OnPostRender()
        //{
        //}
    }
}
