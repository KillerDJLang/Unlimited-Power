using EFT;
using System;
using UnityEngine;
using Comfort.Common;
using EFT.Interactive;
using System.Reflection;
using System.Collections;
using EFT.Communications;

namespace Power
{
    public class UnlimitedPower : MonoBehaviour
    {
        private bool _isRunning = false;
        private Switch[] _switchs = null;

        Player player
        { get => gameWorld.MainPlayer; }

        GameWorld gameWorld
        { get => Singleton<GameWorld>.Instance; }

        void Update()
        {
            if (!Ready() || !Plugin.Enablemod.Value || _isRunning)
            {
                return;
            }

            if (_switchs == null)
            {
                _switchs = FindObjectsOfType<Switch>();
            }

            if (_switchs.Length > 0)
            {
                StaticManager.Instance.StartCoroutine(ThrowRandomSwitch());
                _isRunning = true;
            }
        }

        private IEnumerator ThrowRandomSwitch()
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(Plugin.RandomRangeMin.Value, Plugin.RandomRangeMax.Value) * 60f);

            if (gameWorld != null && gameWorld.AllAlivePlayersList != null && gameWorld.AllAlivePlayersList.Count > 0 && !(player is HideoutPlayer))
            {
                PowerOn();
            }

            else
            {
                _switchs = null;
            }

            _isRunning = false;
            yield break;
        }

        private void PowerOn()
        {
            if (_switchs == null || _switchs.Length <= 0)
            {
                return;
            }

            System.Random random = new System.Random();

            int selection = random.Next(_switchs.Length);
            Switch _switch = _switchs[selection];

            typeof(Switch).GetMethod("Open", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(_switch, null);

            NotificationManagerClass.DisplayMessageNotification("Unlimited Power: A random switch has been thrown.", ENotificationDurationType.Default);

            RemoveAt(ref _switchs, selection);
        }

        static void RemoveAt<T>(ref T[] array, int index)
        {
            if (index >= 0 && index < array.Length)
            {
                for (int i = index; i < array.Length - 1; i++)
                {
                    array[i] = array[i + 1];
                }

                Array.Resize(ref array, array.Length - 1);
            }
        }

        private bool Ready() => gameWorld != null && gameWorld.AllAlivePlayersList != null && gameWorld.AllAlivePlayersList.Count > 0 && !(player is HideoutPlayer);
    }
}
