using Base.Core;
using Base.Levels;
using Base.UI.VideoPlayback;
using HarmonyLib;
using PhoenixPoint.Common.Game;
using PhoenixPoint.Home.View.ViewStates;
using PhoenixPoint.Tactical.View.ViewStates;
using SkipIntro.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SkipIntro
{
    internal class SkipIntroPatch
    {
        private static readonly SkipIntroConfig Config = SkipIntroMain.Main.Config;

        public static void Apply()
        {

        }

        [HarmonyPatch(typeof(PhoenixGame), "RunGameLevel")]
        public static class SkipIntroLogos
        {
            public static bool Prefix(PhoenixGame __instance, LevelSceneBinding levelSceneBinding, ref IEnumerator<NextUpdate> __result)
            {
                try
                {
                    if (levelSceneBinding == __instance.Def.IntroLevelSceneDef.Binding)
                    {
                        __result = Enumerable.Empty<NextUpdate>().GetEnumerator();
                        return false;
                    }
                    if (Config.SkipIntroLogos == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    FileLogger.Error(e);
                    return true;
                }
            }
        }
        [HarmonyPatch(typeof(UIStateHomeScreenCutscene), "EnterState")]
        public static class SkipIntroMovie
        {
            public static void Postfix(UIStateHomeScreenCutscene __instance, VideoPlaybackSourceDef ____sourcePlaybackDef)
            {
                FileLogger.Always(Config.SkipIntroMovie.ToString());
                if (Config.SkipIntroMovie == true)
                {
                    try
                    {
                        if (____sourcePlaybackDef == null)
                        {
                            return;
                        }

                        if (____sourcePlaybackDef.ResourcePath.Contains("Game_Intro_Cutscene"))
                        {
                            typeof(UIStateHomeScreenCutscene).GetMethod("OnCancel", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(__instance, null);
                        }
                    }
                    catch (Exception e)
                    {
                        FileLogger.Error(e);
                        return;
                    }
                }
            }
        }
        [HarmonyPatch(typeof(UIStateTacticalCutscene), "EnterState")]
        public static class SkipLandingSequences
        {
            public static void Postfix(UIStateTacticalCutscene __instance, VideoPlaybackSourceDef ____sourcePlaybackDef)
            {
                if (Config.SkipLandingSequences == true)
                {
                    try
                    {
                        if (____sourcePlaybackDef == null)
                        {
                            return;
                        }
                        if (____sourcePlaybackDef.ResourcePath.Contains("LandingSequences"))
                        {
                            typeof(UIStateTacticalCutscene).GetMethod("OnCancel", BindingFlags.NonPublic | BindingFlags.Instance)?.Invoke(__instance, null);
                        }
                    }
                    catch (Exception e)
                    {
                        FileLogger.Error(e);
                        return;
                    }
                }
            }
        }
    }
}
