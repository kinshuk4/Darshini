//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Data;

//namespace WordListW.Utils
//{
//    public class DbUtil2
//    {
//        private static string _connStr = string.Empty;

//        public static IDbConnection GetConnection()
//        {
//            if (_connStr != string.Empty)
//            {
//                return OpenConnection(_connStr);
//            }
//            else
//            {
//                string connStr = DbUtil.GetConnectionString();
//                return OpenConnection(connStr);
//            }
//        }

//        public static IDbConnection OpenConnection(string connectionString)
//        {
//            IDbConnection cn = null;

//            try
//            {
//                cn = new SqlConnection(connectionString);
//                cn.Open();
//            }
//            catch (SqlException se)
//            {
//                throw new Exception("Failed to Open the connection", se);
//            }
//            catch (Exception e)
//            {
//                throw new Exception("Failed to Open the connection", e);
//            }

//            return cn;
//        }


//        public static System.Data.IDbCommand OpenCommand(IDbConnection cn)
//        {
//            IDbCommand cmd = null;

//            try
//            {
//                cmd = new SqlCommand();

//                if (cmd != null)
//                {
//                    cmd.Connection = cn;
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    cmd.CommandTimeout = 300; //seconds
//                }
//            }
//            catch (SqlException se)
//            {
//                throw new Exception("Failed to Open the command", se);
//            }
//            catch (Exception e)
//            {
//                throw new Exception("Failed to Open the command", e);
//            }

//            return cmd;

//        }

//        public static SqlCommand OpenCommand(SqlConnection cn)
//        {
//            SqlCommand cmd = null;

//            try
//            {
//                cmd = new SqlCommand();

//                if (cmd != null)
//                {
//                    cmd.Connection = cn;
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    cmd.CommandTimeout = 300; //seconds
//                }
//            }
//            catch (SqlException se)
//            {
//                throw new Exception("Failed to Open the command", se);
//            }
//            catch (Exception e)
//            {
//                throw new Exception("Failed to Open the command", e);
//            }

//            return cmd;

//        }

//        public static void CloseDataReader(IDataReader dr)
//        {
//            try
//            {
//                if (dr != null)
//                {
//                    if (!dr.IsClosed)
//                        dr.Close();
//                }
//            }
//            catch
//            {
//                dr = null;
//            }
//        }

//        public static void CloseConnection(IDbConnection cn)
//        {
//            try
//            {
//                if (cn != null)
//                {
//                    cn.Close();
//                    cn = null;
//                }
//            }
//            catch
//            {
//                cn = null;
//            }
//        }

//        public static string FormatDateTime(DateTime date)
//        {
//            return date.Year.ToString() + "-"
//                + date.Month.ToString() + "-"
//                + date.Day.ToString() + " "
//                + date.Hour.ToString() + ":"
//                + date.Minute.ToString() + ":"
//                + date.Second.ToString();
//        }

//        public static string FormatDate(DateTime date)
//        {
//            return date.Year.ToString() + "-"
//                + date.Month.ToString().PadLeft(2, '0') + "-"
//                + date.Day.ToString().PadLeft(2, '0');
//        }

//        public static IDbDataParameter AddParameter(IDbCommand cmd, string paramName, object paramValue)
//        {
//            IDbDataParameter param = cmd.CreateParameter();
//            param.ParameterName = paramName;
//            //if (paramValue.GetType() == typeof(DateTime))
//            //{
//            //    param.DbType = DbType.Date;
//            //    param.Value = FormatDate((DateTime)paramValue);
//            //}
//            //else
//            //{
//            param.Value = paramValue;
//            //}
//            cmd.Parameters.Add(param);
//            return param;
//        }

//        public static string GetConnectionString()
//        {
//            MembershipProvider mp = null;
//            System.Configuration.Configuration config = null;
//            string connectionStringName = null;
//            string connectionString = null;
//            MembershipSection configMembership = null;
//            ConnectionStringsSection configConnStrings = null;
//            ProviderSettings provSettings = null;
//            ConnectionStringSettings connSettings = null;

//            mp = Membership.Provider;
//            config = GetConfiguration();
//            configMembership = GetMembershipConfigSection(config);
//            provSettings = GetProviderSettings(mp, configMembership);
//            connectionStringName = provSettings.Parameters["connectionStringName"];

//            configConnStrings = GetConnectionStringsConfigSection(config);
//            connSettings = GetConnectionStringSettings(connectionStringName, configConnStrings);
//            connectionString = connSettings.ConnectionString;

//            return connectionString;
//        }

//        public static System.Configuration.Configuration GetConfiguration()
//        {
//            HttpContext context = HttpContext.Current;

//            string vpApp = context.Request.ApplicationPath;
//            if (string.IsNullOrEmpty(vpApp))
//            {
//                vpApp = "/";
//            }
//            string lpApp = context.Server.MapPath(vpApp);
//            WebConfigurationFileMap fileMap = null;
//            System.Configuration.Configuration config = null;

//            try
//            {
//                config = WebConfigurationManager.OpenWebConfiguration(vpApp);
//            }
//            catch
//            {
//                fileMap = new WebConfigurationFileMap();
//                fileMap.VirtualDirectories.Add(vpApp, new VirtualDirectoryMapping(lpApp, true));
//                config = WebConfigurationManager.OpenMappedWebConfiguration(fileMap, vpApp);
//            }

//            return config;
//        }

//        public static MembershipSection GetMembershipConfigSection(System.Configuration.Configuration config)
//        {
//            MembershipSection section = null;
//            if (config != null)
//            {
//                section = (MembershipSection)config.GetSection("system.web/membership");
//            }
//            return section;
//        }

//        public static ConnectionStringsSection GetConnectionStringsConfigSection(System.Configuration.Configuration config)
//        {
//            ConnectionStringsSection section = null;
//            if (config != null)
//            {
//                section = (ConnectionStringsSection)config.GetSection("connectionStrings");
//            }
//            return section;
//        }

//        public static ProviderSettings GetProviderSettings(MembershipProvider mp, MembershipSection section)
//        {
//            ProviderSettings settings = null;
//            if (mp != null && section != null)
//            {
//                settings = section.Providers[mp.Name];
//            }
//            return settings;
//        }

//        public static ConnectionStringSettings GetConnectionStringSettings(string connectionStringName, ConnectionStringsSection section)
//        {
//            ConnectionStringSettings settings = null;

//            if (!string.IsNullOrEmpty(connectionStringName)  section != null)
//            {
//                settings = section.ConnectionStrings[connectionStringName];
//            }
//            return settings;
//        }
//    }

//}
