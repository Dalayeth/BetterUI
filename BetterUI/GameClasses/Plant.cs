﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace BetterUI.GameClasses
{
  [HarmonyPatch]
  public static class BetterPlant
  {
    [HarmonyPrefix]
    [HarmonyPatch(typeof(Plant), "GetHoverText")]
    private static bool GetHoverText(Plant __instance, ref string __result)
    {
			switch (__instance.m_status)
			{
				case Plant.Status.Healthy:
					if (Main.timeLeftStylePlant.Value != 0)
          {
						if (Main.timeLeftStylePlant.Value == 1)
            {
							double percentage = __instance.TimeSincePlanted() /__instance.GetGrowTime();
							__result = Localization.instance.Localize($"{__instance.m_name}\n{percentage:P0}"); //$piece_plant_healthy
							return false;
						}
						else
            {
							string time = Patches.Helpers.TimeString(__instance.GetGrowTime(), __instance.TimeSincePlanted());
							__result = Localization.instance.Localize($"{__instance.m_name}\n{time}"); //$piece_plant_healthy
							return false;
						}
          }
					__result = Localization.instance.Localize($"{__instance.m_name} ( $piece_plant_healthy )");
					return true;
				case Plant.Status.NoSun:
					__result = Localization.instance.Localize(__instance.m_name + " ( $piece_plant_nosun )");
					return true;
				case Plant.Status.NoSpace:
					__result = Localization.instance.Localize(__instance.m_name + " ( $piece_plant_nospace )");
					return true;
				case Plant.Status.WrongBiome:
					__result = Localization.instance.Localize(__instance.m_name + " ( $piece_plant_wrongbiome )");
					return true;
				case Plant.Status.NotCultivated:
					__result = Localization.instance.Localize(__instance.m_name + " ( $piece_plant_notcultivated )");
					return true;
				default:
					__result = "";
					return true;
			}
    }
  }
}