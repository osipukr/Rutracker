namespace Rutracker.Shared.Models.Resources
{
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ChangePhoneNumberViewModelResource
    {
        private static global::System.Resources.ResourceManager resourceMan;

        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ChangePhoneNumberViewModelResource()
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
                        "Rutracker.Shared.Models.Resources.ChangePhoneNumberViewModelResource",
                        typeof(ChangePhoneNumberViewModelResource).Assembly);
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
        ///   Looks up a localized string similar to Phone number.
        /// </summary>
        public static string PhoneNumberDisplayName => ResourceManager.GetString("PhoneNumberDisplayName", Culture);

        /// <summary>
        ///   Looks up a localized string similar to The {0} is not valid..
        /// </summary>
        public static string PhoneNumberErrorMessage => ResourceManager.GetString("PhoneNumberErrorMessage", Culture);
    }
}