using Mardis.Engine.Framework.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace Mardis.Engine.Framework
{
    public class UtilSqlBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="referenceTable"></param>
        /// <param name="field"></param>
        /// <param name="clause"></param>
        /// <param name="value"></param>
        /// <param name="numberParameter"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static string AddCondition(string referenceTable, string field, string clause, string value, int numberParameter, DbCommand command)
        {

            string returnCondition = string.Empty;

            switch (clause)
            {
                case CClause.In:
                    returnCondition = ConditionIn(referenceTable, field, value, numberParameter, command);
                    break;
                case CClause.Equal:
                    returnCondition = ConditionEqual(referenceTable, field, value, numberParameter, command);
                    break;
                default:
                    break;
            }

            return returnCondition;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="referenceTable"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="numberParameter"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static string ConditionIn(string referenceTable, string field, string value, int numberParameter, DbCommand command)
        {

            string returnCondition = string.Empty;

            returnCondition = " AND " + referenceTable + "." + field + " IN (";
            string[] splitValues = value.Split(',');

            for (int indexSplit = 0; indexSplit < splitValues.Length; indexSplit++)
            {
                returnCondition += "@p" + numberParameter.ToString() + indexSplit.ToString();

                var parameter = command.CreateParameter();

                parameter.ParameterName = "p" + numberParameter.ToString() + indexSplit.ToString();
                parameter.Value = splitValues[indexSplit];

                if ((indexSplit + 1) < splitValues.Length)
                {
                    returnCondition += ",";
                }

                command.Parameters.Add(parameter);
            }


            returnCondition += ") ";

            return returnCondition;
        }


        public static string ConditionEqual(string referenceTable, string field, object value, int numberParameter, DbCommand command)
        {
            string returnCondition = string.Empty;

            returnCondition = " AND " + referenceTable + "." + field + " = ";
            returnCondition += "@p" + numberParameter;

            var parameter = command.CreateParameter();

            parameter.ParameterName = "p" + numberParameter.ToString();
            parameter.Value = value;

            command.Parameters.Add(parameter);

            return returnCondition;
        }


        public static void AddParameter(DbCommand cmd,string name,object value) {

            var parameter = cmd.CreateParameter();

            parameter.ParameterName = name;
            parameter.Value = value;

            cmd.Parameters.Add(parameter);
        }
    }
}
