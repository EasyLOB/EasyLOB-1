namespace EasyLOB.Data
{
    public interface IZDataBase
    {
        #region Properties

        string LookupText { get; set; } // !!! "LookupText" could be read-only, but OData needs "set"

        #endregion Properties

        #region Methods

        object[] GetId();

        void SetId(object[] ids);

        #endregion Methods
    }
}