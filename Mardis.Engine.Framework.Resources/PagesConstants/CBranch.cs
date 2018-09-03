namespace Mardis.Engine.Framework.Resources.PagesConstants
{
    public class CBranch
    {
        public const string Controller = "BranchController";
        public const string IdRegister = "IdBranch";
        public const string SequenceCode = "SEQ_BR";
        public const string InDataBase = "BDD";
        public const string IsNew = "NEW";
        public const string IsDelete = "DELETE";
        public const string TableName = "MardisCore.Branch";
        public const string ViewDataLocalizacion = "localizacion";
        public const string Yes = "SI";
        public const string No = "NO";

        public const string ImagesContainer = "branches";
    }

    public enum StateBranch
    {
        New,
        Udpate,
        Delete
    }
}
