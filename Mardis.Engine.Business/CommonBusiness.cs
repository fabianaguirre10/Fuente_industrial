using Mardis.Engine.DataAccess;
using Mardis.Engine.Framework.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Mardis.Engine.Business
{
    public class CommonBusiness : ABusiness
    {

        public CommonBusiness(MardisContext mardisContext) : base(mardisContext)
        {

        }


        public void DeleteId(string nameTable, string[] input)
        {
            string sqlCmd = " UPDATE " + nameTable + " SET StatusRegister = @status"
                          + " WHERE Id in (";

            List<SqlParameter> lstParameter = new List<SqlParameter>
            {
                new SqlParameter("@status", CStatusRegister.Delete)
            };



            for (int index = 0; index < input.Length; index++)
            {
                string idTemp = input[index];

                sqlCmd += "@" + index;

                if (index < (input.Length - 1))
                {
                    sqlCmd += ",";
                }

                lstParameter.Add(new SqlParameter("@" + index, new Guid(idTemp)));
            }

            sqlCmd += ") ";

            Context.Database.ExecuteSqlCommand(sqlCmd, lstParameter.ToArray());

        }

    }
}
