using System;
using System.Configuration;

namespace ProductImmuneSystemScores.ApiClient
{
    public class ProductScoreApiSection : ConfigurationSection, IProductScoresApiConfig
    {
        [ConfigurationProperty("Api")]
        public UrlElement Api
        {
            get
            {
                return (UrlElement)this["Api"];
            }
            set
            {
                this["Api"] = value;
            }
        }

        [ConfigurationProperty("Authentication")]
        public AuthenticationElement Authentication
        {
            get
            {
                return (AuthenticationElement)this["Authentication"];
            }
            set
            {
                this["Authentication"] = value;
            }
        }
    }

    public class AuthenticationElement : ConfigurationElement
    {
        [ConfigurationProperty("url", IsRequired = true)]
//        [StringValidator(MinLength = 3)]
        public string Url
        {
            get
            {
                return (String)this["url"];
            }
            set
            {
                this["url"] = value;
            }
        }

        [ConfigurationProperty("username", IsRequired = true)]
//        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\:;<>,./?`¬", MinLength = 1, MaxLength = 40)]
        public String Username
        {
            get
            {
                return (String)this["username"];
            }
            set
            {
                this["username"] = value;
            }
        }

        [ConfigurationProperty("password", IsRequired = true)]
//        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\:;<>,./?`¬", MinLength = 1, MaxLength = 40)]
        public String Password
        {
            get
            {
                return (String)this["password"];
            }
            set
            {
                this["password"] = value;
            }
        }
    }

    public class UrlElement : ConfigurationElement
    {
        [ConfigurationProperty("url", IsRequired = true)]
//        [StringValidator(MinLength = 1)]
        public String Url
        {
            get
            {
                return (String)this["url"];
            }
            set
            {
                this["url"] = value;
            }
        }
    }
}