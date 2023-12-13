using EFT;
using UnityEngine;
using Comfort.Common;
using EFT.Interactive;
using System.Reflection;

namespace Power
{
    public class UnlimitedPower : MonoBehaviour
    {
        float timer;
        float PowerTimer;
        float PowerOnTimeRange = Random.Range(60f, 180f);

        Player player
        { get => gameWorld.MainPlayer; }

        GameWorld gameWorld
        { get => Singleton<GameWorld>.Instance; }

        void Update()
        {
            if (!Ready())
            {
                timer = 0f;
                PowerTimer = 0f;
                return;
            }

            timer += Time.deltaTime;
            if (Plugin.EnablePowerChanges.Value) PowerTimer += Time.deltaTime;

            if (PowerTimer >= PowerOnTimeRange)
            {
                PowerOn();
                PowerTimer = 0f;
                PowerOnTimeRange = Random.Range(900f, 1800f);
            }
        }

        void PowerOn()
        {
            foreach (Switch pSwitch in FindObjectsOfType<Switch>())
            {
                typeof(Switch).GetMethod("Open", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(pSwitch, null);
            }
        }
        public bool Ready() => gameWorld != null && gameWorld.AllAlivePlayersList != null && gameWorld.AllAlivePlayersList.Count > 0 && !(player is HideoutPlayer);
    }
}