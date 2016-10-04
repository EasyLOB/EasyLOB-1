using EasyLOB.Library;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyLOB.Data
{
    public abstract class ZDataBase : IZDataBase, IValidatableObject
    {
        #region Properties

        [JsonIgnore] // Newtonsoft.Json
        [NotMapped] // MongoDB
        public virtual string LookupText
        {
            get
            {
                string result = "";

                Type entityType = this.GetType();
                ZDataDictionaryAttribute dictionary = DataHelper.GetDataDictionaryAttribute(entityType);
                if (!String.IsNullOrEmpty(dictionary.Lookup))
                {
                    var value = LibraryHelper.GetPropertyValue(this, dictionary.Lookup);
                    result = value == null ? "" : value.ToString();
                }

                return result;
            }
            set { }
        }

        #endregion Properties

        #region Methods

        public virtual object[] GetId()
        {
            throw new NotImplementedException();
        }

        public virtual void SetId(object[] ids)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }

        #endregion Methods
    }
}