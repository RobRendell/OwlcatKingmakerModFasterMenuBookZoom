using System;
using System.Reflection;
using Harmony12;
using Kingmaker.UI.MainMenuUI;
using Kingmaker.Visual;
using UnityEngine;
using UnityModManagerNet;

namespace FasterMenuBookZoom {
    
    public static class Main {

        // ReSharper disable once UnusedMember.Local
        private static void Load(UnityModManager.ModEntry modEntry) {
            try {
                HarmonyInstance.Create("kingmaker.fasterMenuBookZoom").PatchAll(Assembly.GetExecutingAssembly());
            } catch (Exception e) {
                modEntry.Logger.Error($"Exception while patching Kingmaker: {e}");
                throw;
            }
        }

        [HarmonyPatch(typeof(MainMenuBoard), "StartIntro")]
        private static class MainMenuBoardStartIntroPatch {
            private static void Postfix(MainMenuBoard __instance) {
                var mCameraController = Traverse.Create(__instance).Field("m_CameraController").GetValue<MainMenuCameraController>();
                var mCameraTransform = Traverse.Create(mCameraController).Field("m_CameraTransform").GetValue<Transform>();
                Traverse.Create(mCameraController).Field("m_CurrentTween").GetValue<CameraTweenAnchor>().TickTween(mCameraTransform, 20f);
                if (MainMenuLogoAnimator.Instance != null) {
                    MainMenuLogoAnimator.Instance.gameObject.SetActive(false);
                }
            }
        }

        [HarmonyPatch(typeof(MainMenuAnimationsController), "Update")]
        private static class MainMenuAnimationsControllerUpdatePatch {
            private static void Postfix(MainMenuAnimationsController __instance) {
                __instance.FadeOutAnimator.Update(20f);
            }
        }
    }
}