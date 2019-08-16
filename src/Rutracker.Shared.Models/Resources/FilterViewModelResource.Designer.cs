namespace Rutracker.Shared.Models.Resources
{
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class FilterViewModelResource
    {
        private static global::System.Resources.ResourceManager resourceMan;

        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal FilterViewModelResource()
        {
        }

        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    resourceMan = new global::System.Resources.ResourceManager(
                        "Rutracker.Shared.Models.Resources.FilterViewModelResource",
                        typeof(FilterViewModelResource).Assembly);
                }

                return resourceMan;
            }
        }

        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture { get; set; }

        /// <summary>
        ///   Looks up a localized string similar to Search.
        /// </summary>
        public static string SearchDisplayName => ResourceManager.GetString("SearchDisplayName", Culture);

        /// <summary>
        ///   Looks up a localized string similar to The {0} must be at  max {1} characters long..
        /// </summary>
        public static string SearchErrorMessage => ResourceManager.GetString("SearchErrorMessage", Culture);

        /// <summary>
        ///   Looks up a localized string similar to Forum titles category.
        /// </summary>
        public static string SelectedForumIdsDisplayName => ResourceManager.GetString("SelectedForumIdsDisplayName", Culture);

        /// <summary>
        ///   Looks up a localized string similar to Size from.
        /// </summary>
        public static string SizeFromDisplayName => ResourceManager.GetString("SizeFromDisplayName", Culture);

        /// <summary>
        ///   Looks up a localized string similar to The {0} must be greater than {1}..
        /// </summary>
        public static string SizeFromErrorMessage => ResourceManager.GetString("SizeFromErrorMessage", Culture);

        /// <summary>
        ///   Looks up a localized string similar to Size to.
        /// </summary>
        public static string SizeToDisplayName => ResourceManager.GetString("SizeToDisplayName", Culture);

        /// <summary>
        ///   Looks up a localized string similar to The {0} must be greater than {1}..
        /// </summary>
        public static string SizeToErrorMessage => ResourceManager.GetString("SizeToErrorMessage", Culture);
    }
}