namespace pokitdokcsharp.Models
{
    /// <summary>
    /// Class BaseProvider.
    /// </summary>
    public abstract class BaseProvider
    {
        private string _organizationName;
        private string _firstName;
        private string _lastName;

        /// <summary>
        /// Gets the first name, will clear organization name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (value != null)
                {
                    _organizationName = null;
                    _firstName = value;
                }
            }
        }

        /// <summary>
        /// Gets the last name, will clear organization name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (value != null)
                {
                    _organizationName = null;
                    _lastName = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the npi.
        /// </summary>
        /// <value>The npi.</value>
        public string NPI { get; set; }

        /// <summary>
        /// Gets or Sets the organization name. This will clear the first and last name properties.
        /// </summary>
        /// <value>The name of the organization.</value>
        public virtual string OrganizationName
        {
            get { return _organizationName; }
            set
            {
                if (value != null)
                    SetOrganizationName(value);
            }
        }

        /// <summary>
        /// Sets the organization name. This will clear the first and last name properties.
        /// </summary>
        /// <param name="orgName"></param>
        private void SetOrganizationName(string orgName)
        {
            FirstName = null;
            LastName = null;
            _organizationName = orgName;
        }

        /// <summary>
        /// The taxonomy code for the attending provider.
        /// </summary>
        /// <value>The taxonomy code.</value>
        public virtual string TaxonomyCode { get; set; }
    }
}