using MultiplayerARPG.KitBT;
using UnityEngine;
//https://gamedevbeginner.com/how-to-use-debug-log-in-unity-without-affecting-performance/
//While this isn’t necessarily a problem when you’re working on your game,
//it might surprise you to know that debug messages will be included,
//and called, in your finished build by default.

namespace KitBehaviorTree
{
    public enum MessageColor { red, blue, green, magenta, yellow, cyan, grey }
    public class Log : ActionNode
    {
        public string message;
        public MessageColor messageColor;
        public static string Colorize(string text, string color)
        {
            return
            "<color=" + color + ">" +
            text +
            "</color>";
        }

        protected override void OnStart()
        {
            Colorize(message, messageColor.ToString());
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            Debug.Log($"<color=" + messageColor + ">" + message + "</color>");
            return State.Success;
        }
    }
}
