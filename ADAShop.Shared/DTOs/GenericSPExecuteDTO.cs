using ADAShop.Shared.Emuns;

namespace ADAShop.Shared.DTOs
{
    public class GenericSPExecuteDTO
    {
        public string NameStoreProcedure { get; set; }
        public string NameDataBaseSelection { get; set; }
        public List<ParamGeneric> Params { get; set; }
        public ActionSPEnum ActionSP { get; set; }
    }

    public class ParamGeneric
    {
        public string Key { get; set; }
        public object Value { get; set; }
        public string DataType { get; set; }
        public string? TypeName { get; set; }
        public EnumActionDirectionParam Direction { get; set; }
    }

    public class SqlParameters
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public object Value { get; set; }
        public string? TypeName { get; set; }
        public EnumActionDirectionParam Direction { get; set; }
    }
}