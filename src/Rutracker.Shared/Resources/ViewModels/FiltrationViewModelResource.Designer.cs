namespace Rutracker.Shared.Resources.ViewModels
{
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class FiltrationViewModelResource
    {
        private static global::System.Resources.ResourceManager resourceMan;

        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal FiltrationViewModelResource()
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
                        "Rutracker.Shared.Resources.ViewModels.FiltrationViewModelResource",
                        typeof(FiltrationViewModelResource).Assembly);
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
        ///   Looks up a localized string similar to The string must be less than 50 characters..
        /// </summary>
        public static string SearchError => ResourceManager.GetString("SearchError", Culture);

        /// <summary>
        ///   Looks up a localized string similar to Size From must be greater than 0..
        /// </summary>
        public static string SizeFromError => ResourceManager.GetString("SizeFromError", Culture);

        /// <summary>
        ///   Looks up a localized string similar to Size To must be greater than 0..
        /// </summary>
        public static string SizeToError => ResourceManager.GetString("SizeToError", Culture);
    }
}