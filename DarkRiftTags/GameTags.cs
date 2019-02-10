namespace DarkRiftTags
{
    public class GameTags
    {
        private const ushort Shift = Tags.Game * Tags.TagsPerPlugin;

        public const ushort InitPlayer = 0 + Shift;
        public const ushort PlayerCommand = 1 + Shift;
        public const ushort MoveToTarget = 2 + Shift;
        public const ushort NearestShips = 3 + Shift;
        public const ushort MessageFailed = 4 + Shift;
        public const ushort PlayerShipData = 5 + Shift;
        public const ushort NearestSpaceObjects = 6 + Shift;
    }
}
