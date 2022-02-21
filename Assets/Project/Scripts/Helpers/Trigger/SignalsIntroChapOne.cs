using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class SignalsIntroChapOne : SerializedMonoBehaviour
    {
        public void ShowChapterView(ChapterData data)
        {
            var view = UI.GetView<ChapterLoaderView>("ChapterLoaderView");
            view.Show();
            view.Refresh(data);
        } 
        
        public void HideChapterView()
        {
            UI.CloseView("ChapterLoaderView");
        }

        public void SetControllers()
        {
            {
                var controller = Player.Robyn.Controller.SetController<ThirdPersonController>(ControllerEnum.PLAYER_CONTROLLER);
                controller.SetCameraToTPSCamera();
            }

            {
                var controller = Player.Abigael.Controller.SetController(ControllerEnum.NAV_MESH_FOLLOWER);
            }
        }

        public void SetPlayerController()
        {
            var controller = Player.Robyn.Controller.SetController<ThirdPersonController>(ControllerEnum.PLAYER_CONTROLLER);
            controller.SetCameraToTPSCamera();
        }
    }
}
