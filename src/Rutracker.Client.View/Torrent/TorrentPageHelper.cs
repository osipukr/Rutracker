using System;

namespace Rutracker.Client.View.Torrent
{
    public static class TorrentPageHelper
    {
        private static readonly string[] Sizes;
        private const double MB = 1024;

        static TorrentPageHelper()
        {
            Sizes = new[] { "B", "KB", "MB", "GB", "TB" };
        }

        public const string TorrentMenuId = "Torrent-Action-Menu-Id";

        public static string GetSizeInMemory(long bytes)
        {
            var len = Convert.ToDouble(bytes);
            var order = 0;

            while (len >= MB && order < Sizes.Length - 1)
            {
                order++;
                len /= MB;
            }

            return $"{len:0.##} {Sizes[order]}";
        }

        public static string GetMagnetLink(int? trackerId)
        {
            return !trackerId.HasValue
                ? string.Empty
                : trackerId == 1
                    ? "http://bt.t-ru.org/ann?magnet"
                    : $"http://bt{trackerId}.t-ru.org/ann?magnet";
        }
    }
}