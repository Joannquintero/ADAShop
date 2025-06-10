using System.ComponentModel;

namespace ADAShop.Shared.Emuns
{
    public enum EnumActionSP
    {
        [Description("Insert")]
        Insert,

        [Description("Select")]
        Select,

        [Description("Update")]
        Update,

        [Description("Delete")]
        Delete
    }

    public enum EnumActionDirectionParam
    {
        [Description("Input")]
        Input,

        [Description("Output")]
        Output
    }
}