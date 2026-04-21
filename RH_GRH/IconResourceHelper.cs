using System.Drawing;

namespace RH_GRH.Properties
{
    /// <summary>
    /// Helper pour générer les icônes manquantes dynamiquement
    /// </summary>
    internal static class IconResources
    {
        private static Bitmap _userIconModern;
        private static Bitmap _lockIconModern;

        public static Bitmap user_icon_modern
        {
            get
            {
                if (_userIconModern == null)
                {
                    _userIconModern = ModernIcons.CreateUserIcon(32, Color.FromArgb(148, 163, 184));
                }
                return _userIconModern;
            }
        }

        public static Bitmap lock_icon_modern
        {
            get
            {
                if (_lockIconModern == null)
                {
                    _lockIconModern = ModernIcons.CreateLockIcon(32, Color.FromArgb(148, 163, 184));
                }
                return _lockIconModern;
            }
        }
    }
}
