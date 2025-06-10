using ADAShop.Shared.DTOs;
using ADAShop.Shared.Emuns;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Dynamic;

namespace ADAShop.Api.Helpers.GenericExecuteSP
{
    public class GenericExecuteSP : IGenericExecuteSP
    {
        private readonly IConfiguration _configuration;

        public GenericExecuteSP(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<T> ExecuteGenericSPAsync<T>(GenericSPExecuteDTO generiRequestDTO)
        {
            SqlConnection con;
            using (con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                con.Open();

                string procedureName = generiRequestDTO.NameStoreProcedure;
                List<SqlParameters> parameters = generiRequestDTO.Params.Select(e => new SqlParameters()
                {
                    DataType = e.DataType,
                    Name = e.Key,
                    Value = e.Value,
                    Direction = e.Direction,
                    TypeName = e.TypeName
                }).ToList();

                SqlCommand cmd = GetStoredProcedureCmd(procedureName, parameters, con);

                switch (generiRequestDTO.ActionSP)
                {
                    case ActionSPEnum.Insert:
                        await cmd.ExecuteNonQueryAsync();

                        GenericResultDTO response = new GenericResultDTO() { IsSuccesfull = true };

                        if (parameters.Any(e => e.Direction == EnumActionDirectionParam.Output))
                            response.Id = cmd.Parameters["@Id"].Value.ToString();

                        return (T)(object)response;

                    case ActionSPEnum.Select:
                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);

                        List<dynamic> resultList = new List<dynamic>();
                        foreach (DataRow dr in dt.Rows)
                        {
                            dynamic objectResult = new ExpandoObject();
                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                var res = dt.Columns[i];
                                AddProperty(objectResult, res.ColumnName, dr[res.ColumnName].ToString());
                            }
                            resultList.Add(objectResult);
                        }

                        string resulJsonList = JsonConvert.SerializeObject(resultList);

                        return (T)(object)(JsonConvert.DeserializeObject<T>(resulJsonList)!);

                    case ActionSPEnum.Update:

                        await cmd.ExecuteNonQueryAsync();

                        return (T)(object)new GenericResultDTO() { IsSuccesfull = true };

                    default:
                        return (T)(object)new GenericResultDTO() { IsSuccesfull = false };
                }
            }
        }

        #region methods private

        private SqlCommand GetStoredProcedureCmd(string nameSP, List<SqlParameters> Parameters, SqlConnection con, SqlTransaction trans = null)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = null;

            if (trans != null)
                cmd = new SqlCommand(nameSP, con, trans);
            else
                cmd = new SqlCommand(nameSP, con);

            cmd.CommandType = CommandType.StoredProcedure;

            if (Parameters != null && Parameters.Count > 0)
            {
                foreach (var para in Parameters)
                {
                    if ((para.Name.Replace("@", "") == "CreadoPor" || para.Name.Replace("@", "") == "FechaCreado") && nameSP.Contains("Update"))
                        continue;

                    SqlDbType type = GetType(para.DataType);

                    if (para.Direction == EnumActionDirectionParam.Output)
                    {
                        cmd.Parameters.AddWithValue(para.Name, type).Value = para.Value;
                        cmd.Parameters[para.Name].Direction = ParameterDirection.Output;
                    }
                    else
                    {
                        if (para.Value != null)
                        {
                            if (para.DataType.ToLower().Contains("datetime"))
                                cmd.Parameters.AddWithValue(para.Name, type).Value = DateTime.Parse(para.Value.ToString());
                            else if (para.DataType.ToLower().Contains("structured"))
                                cmd.Parameters.AddWithValue(para.Name, para.Value);
                            else
                                cmd.Parameters.AddWithValue(para.Name, type).Value = para.Value;
                        }
                        else
                            cmd.Parameters.AddWithValue(para.Name, type).Value = DBNull.Value;
                    }
                }
            }

            return cmd;
        }

        private bool FindDefaultDelete(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                return bool.Parse(dr["IsDeleted"].ToString());
            }

            return false;
        }

        private static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }

        private SqlDbType GetType(string value)
        {
            value = value.Trim();
            value = value.ToLower();

            switch (value.ToString())
            {
                case "smallint":
                    return SqlDbType.SmallInt;

                case "int":
                    return SqlDbType.Int;

                case "int32":
                    return SqlDbType.Int;

                case "int64":
                    return SqlDbType.BigInt;

                case "long":
                    return SqlDbType.BigInt;

                case "short":
                    return SqlDbType.SmallInt;

                case "nvarchar":
                    return SqlDbType.NVarChar;

                case "varchar":
                    return SqlDbType.VarChar;

                case "string":
                    return SqlDbType.VarChar;

                case "datetime":
                    return SqlDbType.DateTime;

                case "bit":
                    return SqlDbType.Bit;

                case "bool":
                    return SqlDbType.Bit;

                case "datetime2":
                    return SqlDbType.DateTime2;

                case "bigint":
                    return SqlDbType.BigInt;

                case "decimal":
                    return SqlDbType.Decimal;

                case "guid":
                    return SqlDbType.UniqueIdentifier;

                case "structured":
                    return SqlDbType.Structured;

                default:
                    throw new Exception(string.Format("Method exception. Date: {0}"));
            }
        }

        public Task<GenericResultDTO> ExceuteSPSelectGeneric(GenericSPExecuteDTO generiRequestDTO)
        {
            throw new NotImplementedException();
        }

        #endregion methods private
    }
}