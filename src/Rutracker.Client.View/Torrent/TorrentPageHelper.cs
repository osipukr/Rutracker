using System;
using Microsoft.AspNetCore.Components.Forms;
using Rutracker.Shared.Models.ViewModels.Torrent;

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

        public static string GetMagnetLink(TorrentView torrent)
        {
            var link = string.Empty;

            if (torrent != null)
            {
                link = $"magnet:?xt=urn:btih:{torrent.Hash}";
            }

            return link;
        }
    }
}