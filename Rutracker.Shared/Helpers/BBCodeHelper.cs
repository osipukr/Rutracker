using CodeKicker.BBCode;

namespace Rutracker.Shared.Helpers
{
    public static class BBCodeHelper
    {
        public static string Parse(string bbCode) => BBCode.ToHtml(bbCode);
    }
}