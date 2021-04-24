using System.Collections.Generic;

namespace BBO.BBO.GameData
{
    public static class ControllerData
    {
        public static HashSet<string> NotGameControllers
        {
            get
            {
                if (notGameControllerSet == null)
                {
                    notGameControllerSet = new HashSet<string>(notGameController);
                }

                return notGameControllerSet;
            }
        }

        private static HashSet<string> notGameControllerSet = default;
        private static string[] notGameController = { "Mouse" };
    }
}
