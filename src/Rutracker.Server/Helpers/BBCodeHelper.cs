using CodeKicker.BBCode;

namespace Rutracker.Server.Helpers
{
    public static class BBCodeHelper
    {
        public static string ParseToHtml(string bbCode) => BBCode.ToHtml(bbCode);
    }
}